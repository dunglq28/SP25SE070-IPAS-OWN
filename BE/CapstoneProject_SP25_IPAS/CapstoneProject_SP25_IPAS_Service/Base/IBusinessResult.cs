using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Base
{
    public interface IBusinessResult
    {
        int StatusCode { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }
}
