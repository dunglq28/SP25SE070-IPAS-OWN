using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class CriteriaType
{
    public int CriteriaTypeId { get; set; }

    public string? CriteriaTypeCode { get; set; }

    public string? CriteriaTypeName { get; set; }

    public virtual ICollection<Criterion> Criteria { get; set; } = new List<Criterion>();
}
