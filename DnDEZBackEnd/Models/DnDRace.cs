namespace DnDEZBackEnd.Models
{
    public class Race
    {
        public string index { get; set; }
        public string name { get; set; }
        public int speed { get; set; }
        public Ability_Bonuses[] ability_bonuses { get; set; }
        public string alignment { get; set; }
        public string age { get; set; }
        public string size { get; set; }
        public string size_description { get; set; }
        public object[] starting_proficiencies { get; set; }
        public Language[] languages { get; set; }
        public string language_desc { get; set; }
        public Trait[] traits { get; set; }
        public object[] subraces { get; set; }
        public string url { get; set; }
    }

    public class Ability_Bonuses
    {
        public Ability_Score ability_score { get; set; }
        public int bonus { get; set; }
    }

    public class Ability_Score
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Language
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Trait
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
}
