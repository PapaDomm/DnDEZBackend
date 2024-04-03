using DnDEZBackend.Models.DTOs.StatsDTOs;

namespace DnDEZBackend.Models.DTOs.CharacterDTOs
{
    public class CharacterDTO
    {
        public int? UserId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; } = null!;

        public string Race { get; set; } = null!;

        public string Class { get; set; } = null!;

        public int? Level { get; set; }

        public int ProfBonus { get; set; }

        public int Initiative { get; set; }

        public int Speed { get; set; }

        public string Alignment { get; set; } = null!;

        public ImageDTO? Image { get; set; }

        public virtual List<CharAbilityScoreDTO> CharAbilityScores { get; set; } = new List<CharAbilityScoreDTO>();

        public virtual List<SavingThrowDTO> SavingThrows { get; set; } = new List<SavingThrowDTO> ();

        public virtual List<CharSkillDTO> CharSkillScores { get; set; } = new List<CharSkillDTO>();
    }
}
