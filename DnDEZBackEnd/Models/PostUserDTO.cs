namespace DnDEZBackend.Models
{
    public class PostUserDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public virtual IFormFile? Image { get; set; }
    }
}
