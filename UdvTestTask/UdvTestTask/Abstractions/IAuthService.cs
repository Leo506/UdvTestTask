using Calabonga.OperationResults;
using UdvTestTask.Models;

namespace UdvTestTask.Abstractions;

public interface IAuthService
{
    Task<OperationResult<bool>> TryAuthAsync(UserModel user);
}