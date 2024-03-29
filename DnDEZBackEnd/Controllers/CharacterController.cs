using DnDEZBackEnd.Models;
using DnDEZBackEnd.Models.Public_Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDEZBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private DnDezdbContext dbContext = new DnDezdbContext();

        private StatAdjuster statAdjuster = new StatAdjuster();

        static CharacterDTO convertCharacterDTO(Character c)
        {
            return new CharacterDTO
            {
                Name = c.Name,
                Race = c.Race,
                Class = c.Class,
                Level = c.Level,
                CharAbilityScores = c.CharAbilityScores.Select(a => convertAbilityDTO(a)).ToList()
            };
        }

        static CharAbilityScoreDTO convertAbilityDTO(CharAbilityScore a)
        {
            return new CharAbilityScoreDTO
            {
                index = a.Index,
                value = a.Value,
                racialBonus = a.RacialBonus
            };
        }


        [HttpGet]
        public IActionResult getAllUserCharacters(int userId)
        {
            List<CharacterDTO> result = dbContext.Characters.Include(a => a.CharAbilityScores).Where(c => c.UserId == userId).Select(c => convertCharacterDTO(c)).ToList();

            return Ok(result);
        }

        [HttpGet("Race")]
        public IActionResult getCharactersWRace(string race, int charId)
        {
            Character c = dbContext.Characters.Include(a => a.CharAbilityScores).FirstOrDefault(c => c.CharacterId == charId);

            Race r = DnDRaceDAL.getRace(race);

            if (c.Race == r.index && c.CharAbilityScores.Any(a => a.RacialBonus == true))
            {
                return Ok(convertCharacterDTO(c));
            }

            if (c.Race != r.index && c.CharAbilityScores.Any(a => a.RacialBonus == true))
            {
                c = statAdjuster.abilityRaceDecrease(c);
            }

            if ((c.Race != r.index && !c.CharAbilityScores.Any(a => a.RacialBonus == true)) || (c.Race == r.index && !c.CharAbilityScores.Any(a => a.RacialBonus == true)))
            {
                c = statAdjuster.abilityRaceIncrease(c, r);
            }

            c.Race = race;

            dbContext.Characters.Update(c);
            dbContext.SaveChanges();

            return Ok(convertCharacterDTO(c));
        }

        [HttpPost]
        public IActionResult createCharacter([FromForm] CharacterPostDTO c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Character newCharacter = new Character();

            newCharacter.CharacterId = 0;
            newCharacter.UserId = c.UserId;
            newCharacter.Name = c.Name;
            newCharacter.Race = c.Race;
            newCharacter.Class = c.Class;
            newCharacter.Level = c.Level;

            

            dbContext.Characters.Add(newCharacter);
            dbContext.SaveChanges();

            Character addCharacter = dbContext.Characters.Include(a => a.CharAbilityScores).FirstOrDefault(c => c.CharacterId == newCharacter.CharacterId);

            foreach (CharAbilityScoreDTO abi in c.CharAbilityScores)
            {
                CharAbilityScore newAbi = new CharAbilityScore();
                newAbi.CharacterId = addCharacter.CharacterId;
                newAbi.Index = abi.index;
                newAbi.Value = abi.value;
                newAbi.RacialBonus = abi.racialBonus;

                dbContext.CharAbilityScores.Add(newAbi);
                dbContext.SaveChanges();
            }

            //dbContext.Characters.Update(addCharacter);
            //dbContext.SaveChanges();

            return Ok();
 
        }
    }
}
