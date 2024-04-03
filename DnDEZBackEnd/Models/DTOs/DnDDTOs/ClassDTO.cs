namespace DnDEZBackend.Models.DTOs.DnDDTOs
{
    public class ClassDTO
    {
        public string index { get; set; }
        public string name { get; set; }
        public ProficiencyDTO proficiency { get; set; }
        public Saving_Throws[] saving_Throws { get; set; }
    }
}
