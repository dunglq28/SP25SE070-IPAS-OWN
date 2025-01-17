using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class Process
{
    public int ProcessId { get; set; }

    public string? ProcessCode { get; set; }

    public string? ProcessName { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? FarmId { get; set; }

    public int? ProcessStyleId { get; set; }

    public virtual Farm? Farm { get; set; }

    public int? GrowthStageID { get; set; }

    public virtual GrowthStage? GrowthStage { get; set; }

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<ProcessData> ProcessData { get; set; } = new List<ProcessData>();

    public virtual ProcessStyle? ProcessStyle { get; set; }

    public virtual ICollection<SubProcess> SubProcesses { get; set; } = new List<SubProcess>();
}
