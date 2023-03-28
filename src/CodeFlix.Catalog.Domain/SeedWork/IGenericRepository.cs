﻿namespace CodeFlix.Catalog.Domain.SeedWork;

public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
    public Task Delete(TAggregate aggregate, CancellationToken cancellationToken);
    public Task<TAggregate> Get(Guid id, CancellationToken cancellationToken);
}

