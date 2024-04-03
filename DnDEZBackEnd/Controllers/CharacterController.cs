using DnDEZBackend.Models;
using DnDEZBackend.Models.DTOs;
using DnDEZBackend.Models.DTOs.CharacterDTOs;
using DnDEZBackend.Models.DTOs.StatsDTOs;
using DnDEZBackend.Models.Public_Classes;
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
                ProfBonus = c.ProfBonus,
                Initiative = c.Initiative,
                Speed = c.Speed,
                Alignment = c.Alignment,
                Image = convertImageDTO(c.Image),
                CharAbilityScores = c.CharAbilityScores.Select(a => convertAbilityDTO(a)).ToList(),
                SavingThrows = c.SavingThrows.Select(t => converSavingThrowDTO(t)).ToList(),
                CharSkillScores = c.CharSkills.Select(s => convertCharSkillDTO(s)).ToList()
            };
        }

        static SavingThrowDTO converSavingThrowDTO(SavingThrow t)
        {
            return new SavingThrowDTO
            {
                Index = t.Index,
                Value = t.Value,
                Proficient = t.Proficient
            };
        }

        static CharSkillDTO convertCharSkillDTO(CharSkill s)
        {
            return new CharSkillDTO
            {
                Index = s.Index,
                Value = s.Value,
                Proficient = s.Proficient
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

            List<CharacterDTO> result = dbContext.Characters.Include(a => a.CharAbilityScores).Include(s => s.CharSkills).Include(t => t.SavingThrows).Include(i => i.Image).Where(c => c.UserId == userId && c.Active == true).Select(c => convertCharacterDTO(c)).ToList();

            return Ok(result);
        }

        [HttpGet("{CharacterId}")]
        public IActionResult getChar(int CharacterId)
        {
            Character result = dbContext.Characters.Include(i => i.Image).Include(a => a.CharAbilityScores).Include(s => s.CharSkills).Include(t => t.SavingThrows).Where(c => c.Active == true).FirstOrDefault(c => c.CharacterId == CharacterId);
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
            newCharacter.ProfBonus = c.ProfBonus;
            newCharacter.Initiative = c.Initiative;
            newCharacter.Speed = c.Speed;
            newCharacter.Alignment = c.Alignment;
            newCharacter.Active = true;

            string charabilitysfromform = c.CharAbilityScores;
            string charskillsfromform = c.CharSkillScores;
            string charsavingthrowsfromform = c.CharSavingThrows;

            if(c.Image != null)
            {
                Image newImage = uploader.getImage(c.Image, "Characters");
                if(newImage != null)
                {
                    newCharacter.ImageId = newImage.ImageId;
                    newCharacter.Image = dbContext.Images.Find(newCharacter.ImageId);
                }
            }
            else
            {
                newCharacter.ImageId = 102;
                newCharacter.Image = dbContext.Images.Find(newCharacter.ImageId);
            }

            List<CharAbilityScoreDTO> newCharAbilityScores = JsonConvert.DeserializeObject<List<CharAbilityScoreDTO>>(charabilitysfromform).ToList();
            List<CharSkillDTO> newCharSkillScores = JsonConvert.DeserializeObject<List<CharSkillDTO>>(charskillsfromform).ToList();
            List<SavingThrowDTO> newCharSavingThrows = JsonConvert.DeserializeObject<List<SavingThrowDTO>>(charsavingthrowsfromform).ToList();

            dbContext.Characters.Add(newCharacter);
            dbContext.SaveChanges();

            Character? addCharacter = dbContext.Characters.Include(a => a.CharAbilityScores).Include(s => s.CharSkills).Include(t => t.SavingThrows).FirstOrDefault(c => c.CharacterId == newCharacter.CharacterId);

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

                foreach (CharSkillDTO skill in newCharSkillScores)
                {
                    CharSkill newSkill = new CharSkill();
                    newSkill.CharacterId = addCharacter.CharacterId;
                    newSkill.Index = skill.Index;
                    newSkill.Value = skill.Value;
                    newSkill.Proficient = skill.Proficient;

                    dbContext.CharSkills.Add(newSkill);
                    dbContext.SaveChanges();
                }

                foreach(SavingThrowDTO savingthrow in newCharSavingThrows)
                {
                    SavingThrow newSavingThrow = new SavingThrow();
                    newSavingThrow.CharacterId = addCharacter.CharacterId;
                    newSavingThrow.Index = savingthrow.Index;
                    newSavingThrow.Value = savingthrow.Value;
                    newSavingThrow.Proficient = savingthrow.Proficient;

                    dbContext.SavingThrows.Add(newSavingThrow);
                    dbContext.SaveChanges();
                }
            }

            //return CreatedAtAction(nameof(getChar), new {id=newCharacter.CharacterId}, convertCharacterDTO(newCharacter));
            return Ok(convertCharacterDTO(newCharacter));
 
        }

        [HttpPut("{id}")]
        public IActionResult updateCharacter([FromForm] CharacterPutDTO c, int id)
        {
            Character updateCharacter = dbContext.Characters.Include(i => i.Image).FirstOrDefault(c => c.CharacterId == id);

            string updateAbilities = c.CharAbilityScores;
            string updateSkills = c.CharSkillScores;
            string updateSavingThrows = c.CharSavingThrows;

            if(updateCharacter == null || updateCharacter.Active == false)
            {
                return NotFound("User Not Found");
            }

            if(c.Name != null)
            {
                updateCharacter.Name = c.Name;
            }

            if(c.Race != null)
            {
                updateCharacter.Race = c.Race;
            }

            if(c.Class != null)
            {
                updateCharacter.Class = c.Class;
            }

            if(c.Level != null) 
            {
                updateCharacter.Level = c.Level;
            }

            if(c.ProfBonus != null)
            {
                updateCharacter.ProfBonus = c.ProfBonus;
            }

            if(c.Initiative != null)
            {
                updateCharacter.Initiative = c.Initiative;
            }

            if (c.Speed != null)
            {
                updateCharacter.Speed = c.Speed;
            }

            if(c.Alignment != null)
            {
                updateCharacter.Alignment = c.Alignment;
            }

            if (c.Image != null)
            {
                Image newImage = uploader.getImage(c.Image, "Characters");
                if(newImage != null)
                {
                    if(updateCharacter.Image != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), updateCharacter.Image.ImagePath)) && updateCharacter.Image.ImagePath != "Images\\Characters\\defaultCharacterImage.png")
                    {
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), updateCharacter.Image.ImagePath));
                        dbContext.Images.Remove(updateCharacter.Image);
                    }
                    updateCharacter.ImageId = newImage.ImageId;
                    updateCharacter.Image = dbContext.Images.Find(newImage.ImageId);
                }
            }

            List<CharAbilityScoreDTO> updateScores = JsonConvert.DeserializeObject<List<CharAbilityScoreDTO>>(updateAbilities).ToList();
            List<CharSkillDTO> updateSkillScores = JsonConvert.DeserializeObject<List<CharSkillDTO>>(updateSkills).ToList();
            List<SavingThrowDTO> updateSavingThrowScores = JsonConvert.DeserializeObject<List<SavingThrowDTO>>(updateSavingThrows).ToList();

            dbContext.Characters.Update(updateCharacter);
            dbContext.SaveChanges();

            Character? targetCharacter = dbContext.Characters.Include(a => a.CharAbilityScores).Include(s => s.CharSkills).Include(t => t.SavingThrows).FirstOrDefault(c => c.CharacterId == updateCharacter.CharacterId);

            if (targetCharacter != null)
            {
                foreach (CharAbilityScoreDTO abi in updateScores)
                {
                    CharAbilityScore targetAbi = dbContext.CharAbilityScores.FirstOrDefault(a => a.Index == abi.Index && a.CharacterId == targetCharacter.CharacterId);
                    targetAbi.Value = abi.Value;
                    targetAbi.RacialBonus = abi.RacialBonus;

                    dbContext.CharAbilityScores.Update(targetAbi);
                    dbContext.SaveChanges();
                }

                foreach(CharSkillDTO skill in updateSkillScores)
                {
                    CharSkill targetSkill = dbContext.CharSkills.FirstOrDefault(s => s.Index == skill.Index && s.CharacterId == targetCharacter.CharacterId);
                    targetSkill.Value = skill.Value;
                    targetSkill.Proficient = skill.Proficient;

                    dbContext.CharSkills.Update(targetSkill);
                    dbContext.SaveChanges();
                }

                foreach(SavingThrowDTO save in updateSavingThrowScores)
                {
                    SavingThrow targetSave = dbContext.SavingThrows.FirstOrDefault(t => t.Index ==  save.Index && t.CharacterId == targetCharacter.CharacterId);
                    targetSave.Value = save.Value;
                    targetSave.Proficient = save.Proficient;

                    dbContext.SavingThrows.Update(targetSave); 
                    dbContext.SaveChanges();
                }
            }

            return Ok(convertCharacterDTO(updateCharacter));

        }

        [HttpDelete("{id}")]
        public IActionResult removeCharacter(int id)
        {
            Character result = dbContext.Characters.Find(id);

            if(result == null || result.Active == false)
            {
                return NotFound("Character Not Found");
            }

            result.Active = false;

            dbContext.Characters.Update(result);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
