namespace DnDEZBackend.Models
{

    public class DnDSkill
    {
        public string index { get; set; }
        public string name { get; set; }
        public string[] desc { get; set; }
        public Ability_Score ability_score { get; set; }
        public string url { get; set; }
    }

}
