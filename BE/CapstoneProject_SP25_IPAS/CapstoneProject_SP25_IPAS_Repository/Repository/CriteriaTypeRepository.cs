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
    public class CriteriaTypeRepository : GenericRepository<CriteriaType>, ICriteriaTypeRepository
    {
        private readonly IpasContext _context;
        public CriteriaTypeRepository(IpasContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CriteriaType>> GetCriteriaTypeByName(string criteriaTypeName)
        {
            var result = await _context.CriteriaTypes.Where(x => x.CriteriaTypeName.ToLower().Equals(criteriaTypeName.ToLower()))
                                                    .Include(x => x.GrowthStage).Include(x => x.Criteria).ToListAsync();
            return result;
        }

    }
}
