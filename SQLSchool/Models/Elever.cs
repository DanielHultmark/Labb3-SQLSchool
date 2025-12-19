using System;
using System.Collections.Generic;

namespace SQLSchool.Models;

public partial class Elever
{
    public int ElevId { get; set; }

    public string Namn { get; set; } = null!;

    public string Personnummer { get; set; } = null!;

    public int? KlassId { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();

    public virtual Klasser? Klass { get; set; }
}
