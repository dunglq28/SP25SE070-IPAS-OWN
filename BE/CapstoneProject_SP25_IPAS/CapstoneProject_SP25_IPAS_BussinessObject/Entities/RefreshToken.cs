using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string? RefreshTokenCode { get; set; }

    public string? RefreshTokenValue { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? ExpiredDate { get; set; }

    public bool? IsUsed { get; set; }

    public bool? IsRevoked { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
