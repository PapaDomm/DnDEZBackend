namespace DnDEZBackEnd.Models
{
    public class RaceDTO
    {
        public string index { get; set; } = null!;
        public string name { get; set; } = null!;
        public Ability_Bonuses[] ability_bonuses { get; set; } = null!;

    }
}
