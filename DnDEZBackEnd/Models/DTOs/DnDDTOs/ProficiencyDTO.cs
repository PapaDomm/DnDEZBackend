namespace DnDEZBackend.Models.DTOs.DnDDTOs
{
    public class ProficiencyDTO
    {
        public int choose { get; set; }
        public List<SkillDTO> choices { get; set; }
    }


    public class SkillDTO
    {
        public string index { get; set; }
        public string name { get; set; }
        public AbilityScoreDTO score { get; set; }
    }
}
