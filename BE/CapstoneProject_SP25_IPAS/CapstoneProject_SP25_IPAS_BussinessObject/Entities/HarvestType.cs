using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class HarvestType
{
    public int HarvestTypeId { get; set; }

    public string? HarvestTypeCode { get; set; }

    public string? HarvestTypeName { get; set; }

    public string? HarvestTypeDescription { get; set; }

    public virtual ICollection<CriteriaHarvestType> CriteriaHarvestTypes { get; set; } = new List<CriteriaHarvestType>();

    public virtual ICollection<HarvestTypeHistory> HarvestTypeHistories { get; set; } = new List<HarvestTypeHistory>();
}
