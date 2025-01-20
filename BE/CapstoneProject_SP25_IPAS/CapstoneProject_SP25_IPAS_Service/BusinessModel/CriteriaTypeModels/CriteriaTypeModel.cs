using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels
{
    public class CriteriaTypeModel
    {
        public int CriteriaTypeId { get; set; }

        public string? CriteriaTypeCode { get; set; }

        public string? CriteriaTypeName { get; set; }

        public int? GrowthStageId { get; set; }
        public string? GrowthStageName { get; set; }
        public List<Criteria>? ListCriteria { get; set; }
    }
}
