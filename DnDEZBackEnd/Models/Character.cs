using System;
using System.Collections.Generic;

namespace DnDEZBackEnd.Models;

public partial class Character
{
    public int CharacterId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Race { get; set; } = null!;

    public string Class { get; set; } = null!;

    public int Level { get; set; }

    public virtual List<CharAbilityScore> CharAbilityScores { get; set; } = new List<CharAbilityScore>();

    public virtual User? User { get; set; }
}
