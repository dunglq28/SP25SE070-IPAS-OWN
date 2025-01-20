using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels
{
    public class CreateCriteriaTypeModel
    {
        public string? CriteriaTypeName { get; set; }

        public int? GrowthStageID { get; set; }

        public List<AddCriteriaModel>? ListCritera {  get; set; } = new List<AddCriteriaModel>();

    }
}
