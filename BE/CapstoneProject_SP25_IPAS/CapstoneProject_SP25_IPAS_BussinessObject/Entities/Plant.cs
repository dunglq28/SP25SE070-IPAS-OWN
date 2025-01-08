using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class Plant
{
    public int PlantId { get; set; }

    public string? PlantCode { get; set; }

    public string? PlantName { get; set; }

    public int? PlantIndex { get; set; }

    public string? GrowthStage { get; set; }

    public string? HealthStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? PlantingDate { get; set; }

    public int? PlantLotId { get; set; }

    public int? PlantReferenceId { get; set; }

    public string? Description { get; set; }

    public int? CultivarId { get; set; }

    public string? ImageUrl { get; set; }

    public int? LandRowId { get; set; }

    public virtual Cultivar? Cultivar { get; set; }

    public virtual ICollection<GraftedPlant> GraftedPlants { get; set; } = new List<GraftedPlant>();

    public virtual ICollection<HarvestTypeHistory> HarvestTypeHistories { get; set; } = new List<HarvestTypeHistory>();

    public virtual ICollection<Plant> InversePlantReference { get; set; } = new List<Plant>();

    public virtual LandRow? LandRow { get; set; }

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<PlantCriterion> PlantCriteria { get; set; } = new List<PlantCriterion>();

    public virtual ICollection<PlantGrowthHistory> PlantGrowthHistories { get; set; } = new List<PlantGrowthHistory>();

    public virtual PlantLot? PlantLot { get; set; }

    public virtual Plant? PlantReference { get; set; }
}
