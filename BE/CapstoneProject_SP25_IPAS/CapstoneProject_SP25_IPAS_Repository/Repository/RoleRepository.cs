using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IpasContext context) : base(context)
        {
        }

        public async Task AddRoleAsync(Role newRole)
        {
            await context.Roles.AddAsync(newRole);
            await context.SaveChangesAsync();
        }

        public async Task<Role?> GetRoleById(int? id)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
        }
        public async Task<Role?> GetRoleByName(string roleName)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.RoleName.ToLower().Equals(roleName.ToLower()));
        }
    }
}
