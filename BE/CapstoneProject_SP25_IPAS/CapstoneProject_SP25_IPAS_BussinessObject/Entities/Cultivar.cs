using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class Cultivar
{
    public int CultivarId { get; set; }

    public string? CultivarCode { get; set; }

    public string? CultivarName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
}
