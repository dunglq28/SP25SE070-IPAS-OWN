using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class ProcessDatum
{
    public int ProcessDataId { get; set; }

    public string? ProcessDataCode { get; set; }

    public string? Input { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? ResourceUrl { get; set; }

    public int? ProcessId { get; set; }

    public int? SubProcessId { get; set; }

    public virtual Process? Process { get; set; }

    public virtual SubProcess? SubProcess { get; set; }
}
