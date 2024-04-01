using DnDEZBackend.Models;
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
            DnDBasicObject result = await DnDClassDAL.getAllClasses()!;
            List<string> result2 = new List<string>();
            foreach(Result r in result.Results)
            {
                result2.Add(r.Index);
            }
            return Ok(result2);
        }
    }
}
