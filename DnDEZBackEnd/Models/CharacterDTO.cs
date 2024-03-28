namespace DnDEZBackEnd.Models
{
    public class CharacterDTO
    {
        public string Name { get; set; } = null!;

        public string Race { get; set; } = null!;

        public string Class { get; set; } = null!;

        public int Level { get; set; }

        public virtual List<CharacterAbilityScoreDTO> CharAbilityScores { get; set; }
    }
}
