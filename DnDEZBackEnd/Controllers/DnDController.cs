using DnDEZBackend.Models;
using DnDEZBackend.Models.DALs;
using DnDEZBackend.Models.DTOs.CharacterDTOs;
using DnDEZBackend.Models.DTOs.DnDDTOs;
using DnDEZBackend.Models.DTOs.StatsDTOs;
using DnDEZBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DnDEZBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DnDController : ControllerBase
    {
        private DnDezdbContext dbContext = new DnDezdbContext();

        static RaceDTO convertRaceDTO(Race r)
        {
            return new RaceDTO
            {
                index = r.index,
                name = r.name,
                speed = r.speed,
                ability_bonuses = r.ability_bonuses
            };
        }

        static async Task<ClassDTO> convertClassDTO(Class c)
        {
            return new ClassDTO
            {
                index = c.index,
                name = c.name,
                hit_die = c.hit_die,
                proficiency = await convertProficiencyDTO(c.proficiency_choices[0]),
                saving_Throws = c.saving_throws
            };
        }

        static async Task<ProficiencyDTO> convertProficiencyDTO(Proficiency_Choices proficiencies)
        {
            List<SkillDTO> classSkills = new List<SkillDTO>();
            foreach(Option o in proficiencies.from.options)
            {
                classSkills.Add(convertSkillDTO(await DnDStatsDAL.getSkill(o.item.index.Substring(6))));
            }

            return new ProficiencyDTO
            {
                choose = proficiencies.choose,
                choices = classSkills
            };
        }

        static SkillDTO convertSkillDTO(DnDSkill s)
        {
            return new SkillDTO
            {
                index = s.index,
                name = s.name,
                score = convertAbilityDTO(s.ability_score)
            };
        }

        static AbilityScoreDTO convertAbilityDTO(DnDEZBackend.Models.Ability_Score a)
        {
            return new AbilityScoreDTO
            {
                index = a.index,
                name = a.name
            };
        }

        static DBSkillDTO convertDBSkillDTO(Skill s)
        {
            return new DBSkillDTO
            {
                Index = s.Index,
                Name = s.Name,
                AbilityIndex = s.AbilityIndex
            };
        }

        [HttpGet("Race")]
        public async Task<IActionResult> getRaceList()
        {

            DnDBasicObject? response = await DnDRaceDAL.getAllRaces()!;
            List<Result> result = response.Results.ToList();
            List<RaceDTO> result2 = new List<RaceDTO>();
            foreach(Result r in result)
            {
                result2.Add(convertRaceDTO(await DnDRaceDAL.getRace(r.Index)!)); 
            }
            return Ok(result2);
        }

        [HttpGet("Class")]
        public async Task<IActionResult> getClassList()
        {
            DnDBasicObject response = await DnDClassDAL.getAllClasses()!;
            List<Result> result = response.Results.ToList();
            List<ClassDTO> result2 = new List<ClassDTO>();
            foreach(Result r in result)
            {
                result2.Add(await convertClassDTO(await DnDClassDAL.getClass(r.Index)!));
            }
            return Ok(result2);
        }

        [HttpGet("Alignment")]
        public async Task<IActionResult> getAlignments()
        {
            DnDBasicObject response = await DnDStatsDAL.getAlignments()!;
            List<Result> result = response.Results.ToList();
            List<string> result2 = new List<string>();
            foreach (Result r in result)
            {
                result2.Add(r.Name);
            }
            return Ok(result2);
        }

        [HttpGet("Skills")]
        public IActionResult getAllSkills()
        {
            List<DBSkillDTO> result = dbContext.Skills.Select(s => convertDBSkillDTO(s)).ToList();

            return Ok(result);
        }

        [HttpGet("Rules")]
        public async Task<IActionResult> getAllRules()
        {
            DnDBasicObject response = await DnDStatsDAL.getAllRules()!;
            List<Result> result = response.Results.ToList();
            List<DnDRule> result2 = new List<DnDRule>();
            foreach (Result r in result)
            {
                result2.Add(await DnDStatsDAL.getRule(r.Index)!);
            }
            return Ok(result2);
        }
    }
}
