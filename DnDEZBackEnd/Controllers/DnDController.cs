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

        [HttpGet]
        public IActionResult getRaceList()
        {
            List<Result> result = DnDRaceDAL.getAllRaces().Results.ToList();
            List<Race> result2 = new List<Race>();
            foreach(Result r in result)
            {
                result2.Add(DnDRaceDAL.getRace(r.Index)); 
            }
            return Ok(result2);
        }
    }
}
