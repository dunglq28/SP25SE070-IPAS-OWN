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
        private ChatRoomRepository _chatRoomRepo;
        private FarmRepository _farmRepo;
        private TaskFeedbackRepository _taskFeedbackRepo;
        private UserWorkLogRepository _userWorkLogRepo;
        private PlanRepository _planRepo;
        private NotificationRepository _notificationRepo;
        private UserFarmRepository _userFarmRepo;
        private PlantLotRepository _plantLotRepo;
        private PlantRepository _plantRepo;

        public UnitOfWork(IpasContext context, IConfiguration configuration)
        {
            _context = context;
            _userRepo = new UserRepository(context);
            _roleRepo = new RoleRepository(context);
            _refreshRepo = new RefreshTokenRepository(context);
            _chatRoomRepo = new ChatRoomRepository(context);
            _farmRepo = new FarmRepository(context);
            _taskFeedbackRepo = new TaskFeedbackRepository(context);
            _userWorkLogRepo = new UserWorkLogRepository(context);
            _planRepo = new PlanRepository(context);
            _notificationRepo = new NotificationRepository(context);
            _userFarmRepo = new UserFarmRepository(context);
            _plantLotRepo = new PlantLotRepository(context);
            _plantRepo = new PlantRepository(context);
            _configuration = configuration;
        }


        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}
        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
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

        //public PaymentRepository PaymentRepository
        //{
        //    get
        //    {
        //        if (_paymentRepo == null)
        //        {
        //            this._paymentRepo = new PaymentRepository(_context);
        //        }
        //        return _paymentRepo;
        //    }
        //}
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

        public ChatRoomRepository ChatRoomRepository
        {
            get
            {
                if (_chatRoomRepo == null)
                {
                    this._chatRoomRepo = new ChatRoomRepository(_context);
                }
                return _chatRoomRepo;
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

        public TaskFeedbackRepository TaskFeedbackRepository
        {
            get
            {
                if (_taskFeedbackRepo == null)
                {
                    this._taskFeedbackRepo = new TaskFeedbackRepository(_context);
                }
                return _taskFeedbackRepo;
            }
        }

        public UserWorkLogRepository UserWorkLogRepository
        {
            get
            {
                if (_userWorkLogRepo == null)
                {
                    this._userWorkLogRepo = new UserWorkLogRepository(_context);
                }
                return _userWorkLogRepo;
            }
        }

        public PlanRepository PlanRepository
        {
            get
            {
                if(_planRepo == null)
                {
                    this._planRepo = new PlanRepository(_context);
                }
                return _planRepo;
            }
        }

        public NotificationRepository NotificationRepository
        {
            get
            {
                if (_notificationRepo == null)
                {
                    this._notificationRepo = new NotificationRepository(_context);
                }
                return _notificationRepo;
            }
        }

        public UserFarmRepository UserFarmRepository
        {
            get
            {
                if (_userFarmRepo == null)
                {
                    this._userFarmRepo = new UserFarmRepository(_context);
                }
                return _userFarmRepo;
            }
        }

        public PlantLotRepository PlantLotRepository
        {
            get
            {
                if (_plantLotRepo == null)
                {
                    this._plantLotRepo = new PlantLotRepository(_context);
                }
                return _plantLotRepo;
            }
        }

        public PlantRepository PlantRepository
        {
            get
            {
                if (_plantRepo == null)
                {
                    this._plantRepo = new PlantRepository(_context);
                }
                return _plantRepo;
            }
        }
    }
}
