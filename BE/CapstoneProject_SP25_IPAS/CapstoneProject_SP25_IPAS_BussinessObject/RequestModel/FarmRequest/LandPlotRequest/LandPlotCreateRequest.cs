using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_BussinessObject.RequestModel.FarmRequest.LandPlotRequest
{
    public class LandPlotCreateRequest
    {
        public string? LandPlotName { get; set; }

        public double? Area { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        public string? SoilType { get; set; }

        public string? Description { get; set; }

        public int? FarmId { get; set; }

        public string? TargetMarket { get; set; }

        public virtual ICollection<CoordinationCreateRequest> LandPlotCoordinations { get; set; } = new List<CoordinationCreateRequest>();
    }
}
