using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Data.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        IEnumerable<Partner> GetByAlias(string alias);
    }

    public class PartnerRepository : RepositoryBase<Partner>, IPartnerRepository
    {
        public PartnerRepository(IDbFactory dbFactory):base(dbFactory)
        { 
        }

        public IEnumerable<Partner> GetByAlias(string alias)
        {
            return this.DbContext.Partners.Where(x => x.Alias == alias);
        }
    }
}
