using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class PackageDetail
{
    public int PackageDetailId { get; set; }

    public string? PackageDetailCode { get; set; }

    public string? FeatureName { get; set; }

    public string? FeatureDescription { get; set; }

    public int? PackageId { get; set; }

    public virtual Package? Package { get; set; }
}
