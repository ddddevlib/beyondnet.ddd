﻿namespace BeyondNet.Ddd.Interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(object entity, CancellationToken cancellationToken = default); 
    }
}
