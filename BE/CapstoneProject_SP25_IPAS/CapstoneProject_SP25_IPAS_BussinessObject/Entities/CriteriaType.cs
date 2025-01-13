using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class CriteriaType
{
    public int CriteriaTypeId { get; set; }

    public string? CriteriaTypeCode { get; set; }

    public string? CriteriaTypeName { get; set; }

    public int? GrowthStageID { get; set; }

    public virtual GrowthStage? GrowthStage { get; set; }

    public virtual ICollection<Criteria> Criteria { get; set; } = new List<Criteria>();
}
