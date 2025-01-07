﻿using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? PaymentCode { get; set; }

    public string? TransactionId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
