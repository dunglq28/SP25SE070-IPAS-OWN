using CapstoneProject_SP25_IPAS_Repository.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        void Save();
        Task<int> SaveAsync();
        public UserRepository UserRepository { get; }
        public RoleRepository RoleRepository { get; }
        public RefreshTokenRepository RefreshTokenRepository { get; }

        public FarmRepository FarmRepository { get; }

    }
}
