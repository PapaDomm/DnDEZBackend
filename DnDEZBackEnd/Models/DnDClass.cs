namespace DnDEZBackend.Models
{

    public class Class
    {
        public string index { get; set; }
        public string name { get; set; }
        public int hit_die { get; set; }
        public Proficiency_Choices[] proficiency_choices { get; set; }
        public Proficiency1[] proficiencies { get; set; }
        public Saving_Throws[] saving_throws { get; set; }
        public Starting_Equipment[] starting_equipment { get; set; }
        public Starting_Equipment_Options[] starting_equipment_options { get; set; }
        public string class_levels { get; set; }
        public Multi_Classing multi_classing { get; set; }
        public Subclass[] subclasses { get; set; }
        public string url { get; set; }
    }

    public class Multi_Classing
    {
        public Prerequisite[] prerequisites { get; set; }
        public Proficiency[] proficiencies { get; set; }
    }

    public class Prerequisite
    {
        public Ability_Score ability_score { get; set; }
        public int minimum_score { get; set; }
    }

    public class Ability_Score
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Proficiency
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Proficiency_Choices
    {
        public string desc { get; set; }
        public int choose { get; set; }
        public string type { get; set; }
        public From from { get; set; }
    }

    public class From
    {
        public string option_set_type { get; set; }
        public Option[] options { get; set; }
    }

    public class Option
    {
        public string option_type { get; set; }
        public Item item { get; set; }
    }

    public class Item
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Proficiency1
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Saving_Throws
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Starting_Equipment
    {
        public Equipment equipment { get; set; }
        public int quantity { get; set; }
    }

    public class Equipment
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Starting_Equipment_Options
    {
        public string desc { get; set; }
        public int choose { get; set; }
        public string type { get; set; }
        public From1 from { get; set; }
    }

    public class From1
    {
        public string option_set_type { get; set; }
        public Option1[] options { get; set; }
    }

    public class Option1
    {
        public string option_type { get; set; }
        public int count { get; set; }
        public Of of { get; set; }
        public Choice choice { get; set; }
    }

    public class Of
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Choice
    {
        public string desc { get; set; }
        public int choose { get; set; }
        public string type { get; set; }
        public From2 from { get; set; }
    }

    public class From2
    {
        public string option_set_type { get; set; }
        public Equipment_Category equipment_category { get; set; }
    }

    public class Equipment_Category
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Subclass
    {
        public string index { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

}
