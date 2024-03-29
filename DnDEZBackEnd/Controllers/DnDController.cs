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
            List<BasicRace> result = DnDRaceDAL.getAllRaces().ToList();
            return Ok(result);
        }
    }
}
