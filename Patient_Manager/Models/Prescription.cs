using System;
using System.Collections.Generic;

namespace Patient_Manager.Models;

public partial class Prescription
{
    public int Prescriptionid { get; set; }

    public int Checkupid { get; set; }

    public string Medication { get; set; } = null!;

    public string Dosage { get; set; } = null!;

    public virtual Checkup Checkup { get; set; } = null!;
}
