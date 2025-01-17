using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.PlantLotModel
{
    public class PlantLotModel
    {

        public int PlantLotId { get; set; }

        public string? PlantLotCode { get; set; }

        public string? PlantLotName { get; set; }

        public int? PreviousQuantity { get; set; }

        public string? Unit { get; set; }

        public string? Status { get; set; }

        public int? LastQuantity { get; set; }

        public DateTime? ImportedDate { get; set; }

        public string? Note { get; set; }

        public string? PartnerName { get; set; }
    }
}
