using System;
using System.Collections.Generic;

namespace DnDEZBackend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? ImageId { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual Image? Image { get; set; }
}
