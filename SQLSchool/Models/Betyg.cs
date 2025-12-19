using System;
using System.Collections.Generic;

namespace SQLSchool.Models;

public partial class Betyg
{
    public int BetygId { get; set; }

    public int ElevId { get; set; }

    public int KursId { get; set; }

    public int LärareId { get; set; }

    public string Betyg1 { get; set; } = null!;

    public DateOnly Datum { get; set; }

    public virtual Elever Elev { get; set; } = null!;

    public virtual Kurser Kurs { get; set; } = null!;

    public virtual Personal Lärare { get; set; } = null!;
}
