using System;
using System.Collections.Generic;

namespace DnDEZBackend.Models.SQLObjects;

public partial class Image
{
    public int ImageId { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
