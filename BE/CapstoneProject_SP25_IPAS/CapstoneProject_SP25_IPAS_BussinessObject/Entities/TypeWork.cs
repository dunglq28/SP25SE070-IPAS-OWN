using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class TypeWork
{
    public int TypeWorkId { get; set; }

    public string? TypeWorkCode { get; set; }

    public bool? VolumeRequired { get; set; }

    public string? TextColor { get; set; }

    public string? TypeWorkName { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? BackgroundColor { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
}
