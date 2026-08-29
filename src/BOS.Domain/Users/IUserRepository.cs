using BOS.Core.Identity;
using BOS.Core.Persistence;

namespace BOS.Domain.Users;

/// <summary>
/// Repository contract for users.
/// </summary>
public interface IUserRepository : IRepository<User, UserId>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
