using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class CriteriaHarvestType
{
    public int CriteriaId { get; set; }

    public int HarvestTypeId { get; set; }

    public bool? IsChecked { get; set; }

    public virtual Criteria Criteria { get; set; } = null!;

    public virtual HarvestType HarvestType { get; set; } = null!;
}
