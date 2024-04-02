using System;
using System.Collections.Generic;

namespace DnDEZBackend.Models;

public partial class Character
{
    public int CharacterId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Race { get; set; } = null!;

    public string Class { get; set; } = null!;

    public int? Level { get; set; }

    public int? ImageId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<CharAbilityScore> CharAbilityScores { get; set; } = new List<CharAbilityScore>();

    public virtual Image? Image { get; set; }

    public virtual User? User { get; set; }
}
