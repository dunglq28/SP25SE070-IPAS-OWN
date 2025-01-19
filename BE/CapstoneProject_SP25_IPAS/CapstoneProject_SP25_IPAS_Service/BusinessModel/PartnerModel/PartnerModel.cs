using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.PartnerModel
{
    public class PartnerModel
    {
        public int PartnerId { get; set; }

        public string? PartnerCode { get; set; }

        public string? PartnerName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? National { get; set; }

        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
