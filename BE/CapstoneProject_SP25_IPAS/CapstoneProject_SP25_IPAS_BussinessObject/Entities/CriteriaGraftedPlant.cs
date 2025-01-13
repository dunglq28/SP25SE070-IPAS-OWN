using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class CriteriaGraftedPlant
{
    public int GraftedPlantId { get; set; }

    public int CriteriaId { get; set; }

    public bool? IsChecked { get; set; }

    public virtual Criteria Criteria { get; set; } = null!;

    public virtual GraftedPlant GraftedPlant { get; set; } = null!;
}
