﻿using CapstoneProject_SP25_IPAS_Repository.IRepository;
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
        //public PaymentRepository PaymentRepository { get; }
        public UserRepository UserRepository { get; }
        public RoleRepository RoleRepository { get; }
        public RefreshTokenRepository RefreshTokenRepository { get; }
        public ChatRoomRepository ChatRoomRepository { get; }
        public FarmRepository FarmRepository { get; }
        public TaskFeedbackRepository TaskFeedbackRepository { get; }
        public UserWorkLogRepository UserWorkLogRepository { get; }
        public PlanRepository PlanRepository { get; }
        public NotificationRepository NotificationRepository { get; }
        public UserFarmRepository UserFarmRepository { get; }
    }
}
