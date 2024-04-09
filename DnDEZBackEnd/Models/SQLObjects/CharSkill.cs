using System;
using System.Collections.Generic;
using DnDEZBackend.Models.SQLObjects;

namespace DnDEZBackend.Models;

public partial class CharSkill
{
    public int CharacterId { get; set; }

    public string Index { get; set; } = null!;

    public int Value { get; set; }

    public bool? Proficient { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Skill IndexNavigation { get; set; } = null!;
}
