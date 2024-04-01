namespace DnDEZBackend.Models
{
    public class putUserDTO
    {
        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? UserName { get; set; } = null!;

        public virtual IFormFile? Image { get; set; }
    }
}
