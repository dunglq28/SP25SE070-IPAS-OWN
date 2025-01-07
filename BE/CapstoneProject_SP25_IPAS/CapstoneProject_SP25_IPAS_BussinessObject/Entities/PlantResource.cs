using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class PlantResource
{
    public int PlanResourceId { get; set; }

    public string? PlanResourceCode { get; set; }

    public string? ResourceType { get; set; }

    public string? ResourceUrl { get; set; }

    public string? Description { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? PlantGrowthHistoryId { get; set; }

    public virtual PlantGrowthHistory? PlantGrowthHistory { get; set; }
}
