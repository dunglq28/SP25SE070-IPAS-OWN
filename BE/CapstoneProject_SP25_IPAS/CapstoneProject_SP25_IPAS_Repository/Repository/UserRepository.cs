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
    public class UserRepository : GenericRepository<User>, IUserRepostiory
    {
        private readonly IpasContext _context;

        public UserRepository(IpasContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User newUser)
        {
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersByRole(string roleName)
        {
            var result = await _context.Users.Include(x => x.Role)
                                                .Where(x => x.Role.RoleName.ToLower().Equals(roleName))
                                                .ToListAsync();
            return result;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));  
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if(getUser != null)
            {
                return getUser;
            }
            return null;
        }

        public async Task<int> SoftDeleteUserAsync(int userId)
        {
            var checkUser = await GetUserByIdAsync(userId);
            if (checkUser != null)
            {
                checkUser.IsDelete = true;
                checkUser.DeleteDate = DateTime.Now;
                var result = await _context.SaveChangesAsync();
                return result;
            }
            return 0;
            
        }

        public async Task<bool> AddOtpToUser(string email, string otpCode, DateTime expiredOtpTime)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if(checkUser != null)
            {
                checkUser.Otp = otpCode;
                checkUser.ExpiredOtpTime = expiredOtpTime;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
        }
    }
}
