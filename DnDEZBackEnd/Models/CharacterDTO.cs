using DnDEZBackend.Models;

namespace DnDEZBackEnd.Models
{
    public class CharacterDTO
    { 
        public int? UserId { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; } = null!;

        public string Race { get; set; } = null!;

        public string Class { get; set; } = null!;

        public int Level { get; set; }

        public ImageDTO? Image { get; set; }

        public virtual List<CharAbilityScoreDTO> CharAbilityScores { get; set; } = new List<CharAbilityScoreDTO>();
    }
}
