using System;
using System.Collections.Generic;

namespace DnDEZBackEnd.Models;

public partial class CharAbilityScore
{
    public int CharacterId { get; set; }

    public string Index { get; set; } = null!;

    public int Value { get; set; }

    public bool? RacialBonus { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual AbilityScore IndexNavigation { get; set; } = null!;
}
