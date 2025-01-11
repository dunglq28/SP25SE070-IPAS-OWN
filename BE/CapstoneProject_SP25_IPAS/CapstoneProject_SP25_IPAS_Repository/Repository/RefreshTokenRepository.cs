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
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly IpasContext _context;

        public RefreshTokenRepository(IpasContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
           await _context.RefreshTokens.AddAsync(refreshToken);    
           await _context.SaveChangesAsync();   
        }

        public async Task<bool> DeleteToken(string deleteRefreshToken)
        {
            var deleteToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.RefreshTokenValue.Equals(deleteRefreshToken));
            if (deleteToken != null)
            {
                _context.RefreshTokens.Remove(deleteToken);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<RefreshToken> GetRefrshTokenByRefreshTokenValue(string refreshToken)
        {
            var result = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.RefreshTokenValue.Equals(refreshToken));
            return result;
        }

        public async Task<bool> UpdateToken(RefreshToken refreshToken)
        {
            var oldRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.RefreshTokenValue.Equals(refreshToken));
            if(oldRefreshToken != null)
            {
                oldRefreshToken.RefreshTokenValue = refreshToken.RefreshTokenValue;
                oldRefreshToken.ExpiredDate = refreshToken.ExpiredDate;
                oldRefreshToken.IsRevoked = refreshToken.IsRevoked;
                oldRefreshToken.IsUsed = refreshToken.IsUsed;
                oldRefreshToken.UserId = refreshToken.UserId;
            }
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
