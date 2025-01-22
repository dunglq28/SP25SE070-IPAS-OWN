using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.PartnerModel
{
    public class UpdatePartnerModel
    {
        public int PartnerId { get; set; }

        public string? PartnerName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? National { get; set; }

        public int? RoleId { get; set; }
    }
}
