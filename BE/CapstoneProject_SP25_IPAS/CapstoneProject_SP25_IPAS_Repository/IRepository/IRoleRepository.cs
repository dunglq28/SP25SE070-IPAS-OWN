using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.IRepository
{
    public interface IRoleRepository
    {
        public Task<Role?> GetRoleById(int? id);
        public Task<Role?> GetRoleByName(string roleName);
        public Task AddRoleAsync(Role newRole);

    }
}
