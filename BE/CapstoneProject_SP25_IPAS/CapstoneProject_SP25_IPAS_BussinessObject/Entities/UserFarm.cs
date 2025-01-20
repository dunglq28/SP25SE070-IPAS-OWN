using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities
{
    public partial class UserFarm
    {
        public int FarmId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public virtual Farm Farm { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
