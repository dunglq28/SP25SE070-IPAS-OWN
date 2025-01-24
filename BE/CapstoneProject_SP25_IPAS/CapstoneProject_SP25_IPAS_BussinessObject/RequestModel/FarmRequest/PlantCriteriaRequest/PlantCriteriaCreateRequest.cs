using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_BussinessObject.RequestModel.FarmRequest.PlantCriteriaRequest
{
    public class PlantCriteriaCreateRequest
    {
        public List<int> PlantIds { get; set; }  = new List<int>();
        public List<CriteriaData> CriteriaData { get; set; } = new List<CriteriaData>();
    }
    public class CriteriaData
    {
        public int CriteriaId { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}
