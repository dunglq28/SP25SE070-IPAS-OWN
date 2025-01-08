using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class WorkLogResource
{
    public int WorkLogResourceId { get; set; }

    public string? WorkLogResourceCode { get; set; }

    public string? ResourceType { get; set; }

    public string? ResourceUrl { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? WorkLogId { get; set; }

    public virtual WorkLog? WorkLog { get; set; }
}
