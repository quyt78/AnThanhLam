using System;

namespace AnThanhLam.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        AnThanhLamDbContext Init();
    }
}
