using System;
using System.Collections.Generic;

namespace DnDEZBackend.Models;

public partial class SavingThrow
{
    public int CharacterId { get; set; }

    public string Index { get; set; } = null!;

    public int Value { get; set; }

    public bool? Proficient { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual AbilityScore IndexNavigation { get; set; } = null!;
}
