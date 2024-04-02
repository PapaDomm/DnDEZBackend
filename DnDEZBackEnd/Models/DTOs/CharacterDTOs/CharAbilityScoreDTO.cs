namespace DnDEZBackend.Models.DTOs.CharacterDTOs
{
    public class CharAbilityScoreDTO
    {
        public string Index { get; set; } = null!;

        public int Value { get; set; }

        public bool? RacialBonus { get; set; }

    }
}
