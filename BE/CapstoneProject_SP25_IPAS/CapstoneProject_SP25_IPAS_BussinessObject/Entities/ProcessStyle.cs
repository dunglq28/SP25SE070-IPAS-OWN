using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class ProcessStyle
{
    public int ProcessStyleId { get; set; }

    public string? ProcessStyleCode { get; set; }

    public string? ProcessStyleName { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Process> Processes { get; set; } = new List<Process>();

    public virtual ICollection<SubProcess> SubProcesses { get; set; } = new List<SubProcess>();
}
