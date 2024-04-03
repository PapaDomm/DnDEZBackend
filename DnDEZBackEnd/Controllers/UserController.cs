using DnDEZBackend.Models;
using DnDEZBackend.Models.DTOs;
using DnDEZBackend.Models.DTOs.CharacterDTOs;
using DnDEZBackend.Models.DTOs.UserDTOs;
using DnDEZBackend.Models.Public_Classes;
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

        private UploadHandler uploader = new UploadHandler();

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
                Image = convertImageDTO(u.Image)
            };
        }

        [HttpGet("{userId}")]
        public IActionResult getUser(int userId)
        {
            User result = dbContext.Users.Include(i => i.Image).Include(c => c.Characters).ThenInclude(i => i.Image).Where(u => u.Active == true).FirstOrDefault(u => u.UserId == userId);

            if (result == null || result.Active == false)
            {
                return NotFound("User Not Found");
            }

            return Ok(convertUserDTO(result));
        }

        [HttpGet("Login")]
        public IActionResult Login(string username, string password)
        {
            User result = dbContext.Users.Include(i => i.Image).Where(u => u.Active == true).FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (result == null || result.Active == false)
            {
                return NotFound();
            }
            return Ok(convertUserDTO(result));
        }

        [HttpPost]
        public IActionResult createUser([FromForm] PostUserDTO u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (dbContext.Users.Any(o => o.UserName == u.UserName && o.Active == true))
            {
                return BadRequest(u.UserName + "is already in use");
            }

            User newUser = new User();

            newUser.FirstName = u.FirstName;
            newUser.LastName = u.LastName;
            newUser.UserName = u.UserName;
            newUser.Password = u.Password;
            newUser.Active = true;

            if (u.Image != null)
            {
                Image newImage = uploader.getImage(u.Image, "Users");
                if (newImage != null)
                {
                    newUser.ImageId = newImage.ImageId;
                    newUser.Image = dbContext.Images.Find(newUser.ImageId);
                }
            }
            else
            {
                newUser.ImageId = 101;
                newUser.Image = dbContext.Images.Find(newUser.ImageId);
            }

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            //return CreatedAtAction(nameof(getUser), new
            //{
            //    id = newUser.UserId
            //}, convertUserDTO(newUser));   

            return Ok(convertUserDTO(newUser));

        }

        [HttpPut("{id}")]
        public IActionResult updateUser([FromForm]putUserDTO u, int id)
        {
            User updateUser = dbContext.Users.Include(i => i.Image).FirstOrDefault(u => u.UserId == id);

            if(updateUser == null || updateUser.Active == false)
            {
                return NotFound("User Not Found");
            }
            if (u.FirstName != null)
            {
                updateUser.FirstName = u.FirstName;
            }
            if (u.LastName != null)
            {
                updateUser.LastName = u.LastName;
            }
            if (u.UserName != null)
            {
                if(dbContext.Users.Any(o => o.UserName == u.UserName && u.UserName != updateUser.UserName))
                {
                    return BadRequest();
                }
                updateUser.UserName = u.UserName;
                
            }
            if (u.Image != null)
            {
                Image newImage = uploader.getImage(u.Image, "Users");
                if (newImage != null)
                {
                    if (updateUser.Image != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), updateUser.Image.ImagePath)) && updateUser.Image.ImagePath != "Images\\Users\\defaultProfilePic.png")
                    {
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), updateUser.Image.ImagePath));
                        dbContext.Images.Remove(updateUser.Image);
                    }
                    updateUser.ImageId = newImage.ImageId;
                    updateUser.Image = dbContext.Images.Find(updateUser.ImageId);
                }
            }
        
            dbContext.Users.Update(updateUser);
            dbContext.SaveChanges();

            return Ok(convertUserDTO(updateUser));
        }

        [HttpDelete("{id}")]
        public IActionResult removeUser(int id)
        {
            User result = dbContext.Users.Find(id);

            if(result == null || result.Active == false)
            {
                return NotFound("User Not Found");
            }

            result.Active = false;

            dbContext.Users.Update(result);
            dbContext.SaveChanges();

            return NoContent();
        }

    }
}
