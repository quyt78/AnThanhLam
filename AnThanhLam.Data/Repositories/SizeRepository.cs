using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Data.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
        Size GetSizeById(string id);
    }
    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory): base(dbFactory) 
        {

        }

        public Size GetSizeById(string id)
        {
            return DbContext.Sizes.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
