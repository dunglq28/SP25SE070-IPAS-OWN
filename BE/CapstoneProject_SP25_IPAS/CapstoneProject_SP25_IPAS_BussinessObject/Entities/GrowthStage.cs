using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities
{
    public partial class GrowthStage
    {
        public int GrowthStageId { get; set; }

        public string? GrowthStageCode { get; set; }

        public string? GrowthStageName { get; set; }

        public DateTime? MonthAgeStart { get; set; }
        public DateTime? MonthAgeEnd { get; set; }

        public virtual ICollection<CriteriaType> CriteriaTypes { get; set; } = new List<CriteriaType>();

        public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

        public virtual ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
