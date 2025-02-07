using System;
using System.Collections.Generic;

namespace Patient_Manager.Models;

public partial class Medicalfile
{
    public int Fileid { get; set; }

    public int Checkupid { get; set; }

    public string Filepath { get; set; } = null!;

    public DateTime? Uploadedat { get; set; }

    public virtual Checkup Checkup { get; set; } = null!;
}
