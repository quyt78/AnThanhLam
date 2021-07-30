using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Data.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<Brand> GetByAlias(string alias);
    }

    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(IDbFactory dbFactory):base(dbFactory)
        { 
        }

        public IEnumerable<Brand> GetByAlias(string alias)
        {
            return this.DbContext.Brands.Where(x => x.Alias == alias);
        }
    }
}
