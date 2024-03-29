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
                ability_bonuses = r.ability_bonuses
            };
        }

        [HttpGet("Race")]
        public IActionResult getRaceList()
        {
            List<Result> result = DnDRaceDAL.getAllRaces().Results.ToList();
            List<RaceDTO> result2 = new List<RaceDTO>();
            foreach(Result r in result)
            {
                result2.Add(convertRaceDTO(DnDRaceDAL.getRace(r.Index))); 
            }
            return Ok(result2);
        }

        [HttpGet("Class")]
        public IActionResult getClassList()
        {
            DnDBasicObject result = DnDClassDAL.getAllClasses();
            List<string> result2 = new List<string>();
            foreach(Result r in result.Results)
            {
                result2.Add(r.Index);
            }
            return Ok(result2);
        }
    }
}
