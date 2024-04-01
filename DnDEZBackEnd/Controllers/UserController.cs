using DnDEZBackend.Models;
using DnDEZBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDEZBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DnDezdbContext dbContext = new DnDezdbContext();

        static CharacterDTO convertCharacterDTO(Character c)
        {
            return new CharacterDTO
            {
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

        static UserDTO convertUserDTO(User u)
        {
            return new UserDTO
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Characters = u.Characters.Select(c => convertCharacterDTO(c)).ToList(),
                Image = convertImageDTO(u.Image)
            };
        }

        [HttpGet("{userId}")]
        public IActionResult getUser(int userId)
        {
            User result = dbContext.Users.Include(i => i.Image).Include(c => c.Characters).ThenInclude(c => c.CharAbilityScores).FirstOrDefault(u => u.UserId == userId);
            return Ok(convertUserDTO(result));
        }


    }
}
