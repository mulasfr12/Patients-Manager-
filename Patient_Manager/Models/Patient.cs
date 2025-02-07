using System;
using System.Collections.Generic;

namespace Patient_Manager.Models;

public partial class Patient
{
    public int Patientid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateOnly Dateofbirth { get; set; }

    public char? Sex { get; set; }

    public virtual ICollection<Checkup> Checkups { get; set; } = new List<Checkup>();

    public virtual ICollection<Medicalrecord> Medicalrecords { get; set; } = new List<Medicalrecord>();
}
