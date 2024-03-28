using System;
using System.Collections.Generic;

namespace DnDEZBackEnd.Models;

public partial class AbilityScore
{
    public string Index { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<CharAbilityScore> CharAbilityScores { get; set; } = new List<CharAbilityScore>();
}
