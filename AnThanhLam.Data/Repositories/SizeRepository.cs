using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Data.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {      
    }
    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }
    }
}
