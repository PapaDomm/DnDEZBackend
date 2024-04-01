using DnDEZBackEnd.Models;

namespace DnDEZBackend.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public virtual List<CharacterDTO> Characters { get; set; } = new List<CharacterDTO>();

        public virtual ImageDTO? Image { get; set; }
    }
}
