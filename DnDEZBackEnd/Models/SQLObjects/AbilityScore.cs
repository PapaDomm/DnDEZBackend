using System;
using System.Collections.Generic;
using DnDEZBackend.Models.SQLObjects;

namespace DnDEZBackend.Models;

public partial class AbilityScore
{
    public string Index { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<CharAbilityScore> CharAbilityScores { get; set; } = new List<CharAbilityScore>();

    public virtual ICollection<SavingThrow> SavingThrows { get; set; } = new List<SavingThrow>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
