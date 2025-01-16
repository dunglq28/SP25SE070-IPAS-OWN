using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IpasContext _context;
        private readonly IConfiguration _configuration;
        private IDbContextTransaction _transaction;


        //private PaymentRepository _paymentRepo;
        private UserRepository _userRepo;
        private RoleRepository _roleRepo;
        private RefreshTokenRepository _refreshRepo;
        private FarmRepository _farmRepo;
        public UnitOfWork(IpasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _userRepo = new UserRepository(context);
            _roleRepo = new RoleRepository(context);
            _refreshRepo = new RefreshTokenRepository(context);
            _farmRepo = new FarmRepository(context);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null!;
            }
        }

        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepo == null)
                {
                    this._userRepo = new UserRepository(_context);
                }
                return _userRepo;
            }
        }

        public RoleRepository RoleRepository
        {
            get
            {
                if (_roleRepo == null)
                {
                    this._roleRepo = new RoleRepository(_context);
                }
                return _roleRepo;
            }
        }

        public RefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if(_refreshRepo == null)
                {
                    this._refreshRepo = new RefreshTokenRepository(_context);
                }
                return _refreshRepo;
            }
        }

        public FarmRepository FarmRepository
        {
            get
            {
                if (_farmRepo == null)
                {
                    this._farmRepo = new FarmRepository(_context);
                }
                return _farmRepo;
            }
        }
    }
}
