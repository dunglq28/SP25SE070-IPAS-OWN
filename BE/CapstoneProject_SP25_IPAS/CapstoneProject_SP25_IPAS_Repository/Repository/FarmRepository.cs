using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CapstoneProject_SP25_IPAS_Repository.Repository
{
    public class FarmRepository : GenericRepository<Farm>, IFarmRepository
    {
        private readonly IpasContext _context;
        public FarmRepository(IpasContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Farm>> Get(Expression<Func<Farm, bool>> filter = null, Func<IQueryable<Farm>, IOrderedQueryable<Farm>> orderBy = null, int? pageIndex = null, int? pageSize = null)
        {
            IQueryable<Farm> query = _context.Farms.AsQueryable(); // Khởi tạo truy vấn

            // Áp dụng bộ lọc (nếu có)
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Sắp xếp (nếu có)
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Phân trang (nếu có cả pageIndex và pageSize)
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                // Ensure the pageIndex and pageSize are valid
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }
            query.Include(x => x.UserFarms)
                .ThenInclude(x => x.User);
            // Thực hiện truy vấn và trả về danh sách
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Farm> GetFarmById(int farmId)
        {
            var farm = await _context.Farms
                .Where(x => x.FarmId == farmId && x.IsDelete == false)
                .Include(x => x.UserFarms)
                .ThenInclude(x => x.User)
                .Include( x => x.LandPlots)
                //.Include(x => x.Processes)
                .Include(x => x.FarmCoordinations)
                .FirstOrDefaultAsync();
            return farm;
        }
    }
}
