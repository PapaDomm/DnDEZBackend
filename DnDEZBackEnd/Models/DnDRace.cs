namespace DnDEZBackEnd.Models
{
    public class Race
    {
        public string index { get; set; } = null!;
        public string name { get; set; } = null!;
        public int speed { get; set; } 
        public Ability_Bonuses[] ability_bonuses { get; set; } = null!;
        public string alignment { get; set; } = null!;
        public string age { get; set; } = null!;
        public string size { get; set; } = null!;
        public string size_description { get; set; } = null!;
        public object[] starting_proficiencies { get; set; } = null!;
        public Language[] languages { get; set; } = null!;
        public string language_desc { get; set; } = null!;
        public Trait[] traits { get; set; } = null!;
        public object[] subraces { get; set; } = null!;
        public string url { get; set; } = null!;
    }

    public class Ability_Bonuses
    {
        public Ability_Score ability_score { get; set; } = null!;
        public int bonus { get; set; }
    }

    public class Ability_Score
    {
        public string index { get; set; } = null!;
        public string name { get; set; } = null!;
        public string url { get; set; } = null!;
    }

    public class Language
    {
        public string index { get; set; } = null!;
        public string name { get; set; } = null!;
        public string url { get; set; } = null!;
    }

    public class Trait
    {
        public string index { get; set; } = null!;
        public string name { get; set; } = null!;
        public string url { get; set; } = null!;
    }
}
