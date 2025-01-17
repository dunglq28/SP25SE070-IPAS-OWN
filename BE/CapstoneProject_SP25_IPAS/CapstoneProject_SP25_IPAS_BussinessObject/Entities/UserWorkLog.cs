using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class UserWorkLog
{
    public int WorkLogId { get; set; }

    public int UserId { get; set; }

    public int? IsReporter { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual WorkLog WorkLog { get; set; } = null!;
}
