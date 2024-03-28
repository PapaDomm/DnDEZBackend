using System;
using System.Collections.Generic;

namespace DnDEZBackEnd.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
