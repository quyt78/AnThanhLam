namespace AnThanhLam.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        AnThanhLamDbContext dbContext;
        public AnThanhLamDbContext Init()
        {
            return dbContext ?? (dbContext = new AnThanhLamDbContext());
        }

        protected override void DisposeCore()
        {
           if(dbContext !=null)
            {
                dbContext.Dispose();
            }
        }
    }
}
