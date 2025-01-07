using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task<int> SaveAsync();
        //public PaymentRepository PaymentRepository { get; }

    }
}
