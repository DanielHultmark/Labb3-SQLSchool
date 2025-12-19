using System;
using System.Collections.Generic;

namespace SQLSchool.Models;

public partial class Befattning
{
    public int BefattningsId { get; set; }

    public string Befattning1 { get; set; } = null!;

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
