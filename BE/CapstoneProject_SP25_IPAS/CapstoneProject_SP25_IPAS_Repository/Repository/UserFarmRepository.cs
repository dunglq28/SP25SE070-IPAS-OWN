﻿using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.Repository
{
    public class UserFarmRepository : GenericRepository<UserFarm>, IUserFarmRepository
    {
        private readonly IpasContext _context;
        public UserFarmRepository(IpasContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> getRoleOfUserInFarm(int userId, int farmId)
        {
            var getUserFarm = await _context.UserFarms.FirstOrDefaultAsync(x => x.UserId == userId && x.FarmId == farmId);
            if (getUserFarm != null)
            {
                return getUserFarm.RoleId;
            }
            return -1;
        }
    }
}
