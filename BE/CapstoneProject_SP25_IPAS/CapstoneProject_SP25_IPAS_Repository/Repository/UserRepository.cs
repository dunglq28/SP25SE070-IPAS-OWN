using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepostiory
    {
        private readonly IpasContext _context;

        public UserRepository(IpasContext context) : base(context)
        {
            _context = context;
        }

        public Task AddUserAsync(User newUser)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SoftDeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
