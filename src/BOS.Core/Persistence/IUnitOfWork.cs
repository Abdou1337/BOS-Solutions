namespace BOS.Core.Persistence;

/// <summary>
/// Unit of work abstraction.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
