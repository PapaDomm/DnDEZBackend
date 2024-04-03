using System;
using System.Collections.Generic;

namespace DnDEZBackend.Models;

public partial class Skill
{
    public string Index { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string AbilityIndex { get; set; } = null!;

    public virtual AbilityScore AbilityIndexNavigation { get; set; } = null!;

    public virtual ICollection<CharSkill> CharSkills { get; set; } = new List<CharSkill>();
}
