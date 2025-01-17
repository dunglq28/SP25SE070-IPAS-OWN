using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.IRepository
{
    public interface IRefreshTokenRepository
    {
        public Task AddRefreshToken(RefreshToken refreshToken);
        public Task<RefreshToken> GetRefrshTokenByRefreshTokenValue(string refreshToken);
        public Task<bool> UpdateToken(RefreshToken refreshToken);
        public Task<bool> DeleteToken(string deleteRefreshToken);
    }
}
