using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public string? OrderCode { get; set; }

    public string? OrderName { get; set; }

    public double? TotalPrice { get; set; }

    public string? Notes { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? EnrolledDate { get; set; }

    public DateOnly? ExpiredDate { get; set; }

    public int? PackageId { get; set; }

    public int? FarmId { get; set; }

    public virtual Farm? Farm { get; set; }

    public virtual Package? Package { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
