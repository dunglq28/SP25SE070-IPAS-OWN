using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Repository.Repository
{
    public class PlantCriteriaRepository : GenericRepository<PlantCriteria>, IPlantCriteriaRepository
    {
        public PlantCriteriaRepository(IpasContext context) : base(context)
        {
        }
    }
}
