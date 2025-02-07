using System;
using System.Collections.Generic;

namespace Patient_Manager.Models;

public partial class Medicalrecord
{
    public int Recordid { get; set; }

    public int Patientid { get; set; }

    public string Diseasename { get; set; } = null!;

    public DateOnly Startdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
