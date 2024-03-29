namespace DnDEZBackEnd.Models
{
    public class CharacterPostDTO
    {
        public int? UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Race { get; set; } = null!;

        public string Class { get; set; } = null!;

        public int Level { get; set; }

        public CharAbilityScoreDTO[] CharAbilityScores { get; set; }

    }
}
