using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class Crop
{
    public int CropId { get; set; }

    public string? CropCode { get; set; }

    public string? CropName { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public DateOnly? CropExpectedTime { get; set; }

    public DateOnly? CropActualTime { get; set; }

    public string? HarvestSeason { get; set; }

    public double? EstimateYield { get; set; }

    public double? ActualYield { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public double? MarketPrice { get; set; }

    public int? LandPlotId { get; set; }

    public virtual ICollection<HarvestHistory> HarvestHistories { get; set; } = new List<HarvestHistory>();

    public virtual LandPlot? LandPlot { get; set; }

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
}
