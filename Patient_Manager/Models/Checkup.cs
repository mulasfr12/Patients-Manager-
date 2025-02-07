using System;
using System.Collections.Generic;

namespace Patient_Manager.Models;

public partial class Checkup
{
    public int Checkupid { get; set; }

    public int Patientid { get; set; }

    public string? Checkuptype { get; set; }

    public DateOnly Checkupdate { get; set; }

    public TimeOnly Checkuptime { get; set; }

    public virtual ICollection<Medicalfile> Medicalfiles { get; set; } = new List<Medicalfile>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
