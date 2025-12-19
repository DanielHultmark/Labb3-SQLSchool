using System;
using System.Collections.Generic;

namespace SQLSchool.Models;

public partial class Personal
{
    public int PersonalId { get; set; }

    public string Namn { get; set; } = null!;

    public int BefattningId { get; set; }

    public virtual Befattning Befattning { get; set; } = null!;

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();

    public virtual ICollection<Klasser> Klassers { get; set; } = new List<Klasser>();
}
