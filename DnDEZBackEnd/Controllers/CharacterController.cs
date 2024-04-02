using DnDEZBackend.Models;
using DnDEZBackend.Models.Public_Classes;
using DnDEZBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DnDEZBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private DnDezdbContext dbContext = new DnDezdbContext();

        private UploadHandler uploader = new UploadHandler();

        //private StatAdjuster statAdjuster = new StatAdjuster();

        static CharacterDTO convertCharacterDTO(Character c)
        {
            return new CharacterDTO
            {
                UserId = c.UserId,
                CharacterId = c.CharacterId,
                Name = c.Name,
                Race = c.Race,
                Class = c.Class,
                Level = c.Level,
                Image = convertImageDTO(c.Image),
                CharAbilityScores = c.CharAbilityScores.Select(a => convertAbilityDTO(a)).ToList()
            };
        }

        static ImageDTO convertImageDTO(Image i)
        {
            if (i == null)
            {
                return null;
            }

            return new ImageDTO
            {
                ImageId = i.ImageId,
                ImagePath = i.ImagePath
            };
        }

        static CharAbilityScoreDTO convertAbilityDTO(CharAbilityScore a)
        {
            return new CharAbilityScoreDTO
            {
                Index = a.Index,
                Value = a.Value,
                RacialBonus = a.RacialBonus
            };
        }

        [HttpGet]
        public IActionResult getAllUserCharacters(int userId)
        {

            List<CharacterDTO> result = dbContext.Characters.Include(a => a.CharAbilityScores).Include(i => i.Image).Where(c => c.UserId == userId).Select(c => convertCharacterDTO(c)).ToList();

            return Ok(result);
        }

        [HttpGet("{CharacterId}")]
        public IActionResult getChar(int CharacterId)
        {
            Character result = dbContext.Characters.Include(i => i.Image).Include(a => a.CharAbilityScores).FirstOrDefault(c => c.CharacterId == CharacterId);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(convertCharacterDTO(result));
        }

        [HttpPost]
        public IActionResult createCharacter([FromForm] CharacterPostDTO c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Character newCharacter = new Character();

            newCharacter.UserId = c.UserId;
            newCharacter.CharacterId = 0;
            newCharacter.UserId = c.UserId;
            newCharacter.Name = c.Name;
            newCharacter.Race = c.Race;
            newCharacter.Class = c.Class;
            newCharacter.Level = c.Level;

            string charabilitysfromform = c.CharAbilityScores;

            if(c.Image != null)
            {
                Image newImage = uploader.getImage(c.Image, "Characters");
                if(newImage != null)
                {
                    newCharacter.ImageId = newImage.ImageId;
                    newCharacter.Image = dbContext.Images.Find(newCharacter.ImageId);
                }
            }

            List<CharAbilityScoreDTO> newCharAbilityScores = JsonConvert.DeserializeObject<List<CharAbilityScoreDTO>>(charabilitysfromform).ToList();

            dbContext.Characters.Add(newCharacter);
            dbContext.SaveChanges();

            Character? addCharacter = dbContext.Characters.Include(a => a.CharAbilityScores).FirstOrDefault(c => c.CharacterId == newCharacter.CharacterId);

            if (addCharacter != null)
            {
                foreach (CharAbilityScoreDTO abi in newCharAbilityScores)
                {
                    CharAbilityScore newAbi = new CharAbilityScore();
                    newAbi.CharacterId = addCharacter.CharacterId;
                    newAbi.Index = abi.Index;
                    newAbi.Value = abi.Value;
                    newAbi.RacialBonus = abi.RacialBonus;

                    dbContext.CharAbilityScores.Add(newAbi);
                    dbContext.SaveChanges();
                }
            }

            //return CreatedAtAction(nameof(getChar), new {id=newCharacter.CharacterId}, convertCharacterDTO(newCharacter));
            return Ok(convertCharacterDTO(newCharacter));
 
        }
    }
}
