using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //private Fa24Se1716Prn231G5KoiauctionContext _context;
        //private PaymentRepository _paymentRepo;

        //public UnitOfWork(Fa24Se1716Prn231G5KoiauctionContext context)
        //{
        //    _context = context;
        //}

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
            //GC.SuppressFinalize(this);
        }

        public void Save()
        {
            //_context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            //return await _context.SaveChangesAsync();
            throw new NotImplementedException();
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
    }
}
