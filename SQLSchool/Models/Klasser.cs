using System;
using System.Collections.Generic;

namespace SQLSchool.Models;

public partial class Klasser
{
    public int KlassId { get; set; }

    public string Namn { get; set; } = null!;

    public int LärarId { get; set; }

    public virtual ICollection<Elever> Elevers { get; set; } = new List<Elever>();

    public virtual Personal Lärar { get; set; } = null!;
}
