using System;
using System.Collections.Generic;

namespace SQLSchool.Models;

public partial class Kurser
{
    public int KursId { get; set; }

    public string Namn { get; set; } = null!;

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
