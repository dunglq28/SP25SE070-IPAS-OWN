using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_BussinessObject.RequestModel.FarmRequest
{
    public class UpdateFarmCoordinationRequest
    {
        [Required(ErrorMessage = "Farm Id is required")]
        public int FarmId { get; set; }
        public List<FarmCoordinationCreateModel> FarmUpdateModel { get; set; }
    }
}
