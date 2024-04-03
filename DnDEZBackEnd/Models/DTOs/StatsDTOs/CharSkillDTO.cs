namespace DnDEZBackend.Models.DTOs.StatsDTOs
{
    public class CharSkillDTO
    {
        public string Index { get; set; } = null!;

        public int Value { get; set; }

        public bool? Proficient { get; set; }
    }
}
