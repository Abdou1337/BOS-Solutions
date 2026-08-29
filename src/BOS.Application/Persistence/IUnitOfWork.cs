namespace BOS.Application.Persistence;

/// <summary>
/// Unit of work abstraction for coordinating persistence operations
/// within a single bounded context or use case.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
