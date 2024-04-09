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

    public int ProfBonus { get; set; }

    public int Initiative { get; set; }

    public int Speed { get; set; }

    public string Alignment { get; set; } = null!;

    public string? Personality { get; set; }

    public string? Ideals { get; set; }

    public string? Bonds { get; set; }

    public string? Flaws { get; set; }

    public int HitDie { get; set; }

    public int Hp { get; set; }

    public int? ImageId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<CharAbilityScore> CharAbilityScores { get; set; } = new List<CharAbilityScore>();

    public virtual ICollection<CharSkill> CharSkills { get; set; } = new List<CharSkill>();

    public virtual Image? Image { get; set; }

    public virtual ICollection<SavingThrow> SavingThrows { get; set; } = new List<SavingThrow>();

    public virtual User? User { get; set; }
}
