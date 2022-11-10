using Calabonga.OperationResults;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace UdvTestTask.Services;

public class AuthService : IAuthService
{
    private readonly ulong _appId;
    private string? _accessToken;
    private readonly IVkApi _vkApi;

    public AuthService(ulong appId, IVkApi vkApi)
    {
        _appId = appId;
        _vkApi = vkApi;
    }

    public async Task<OperationResult<bool>> TryAuthAsync(UserModel user)
    {
        var result = OperationResult.CreateResult<bool>();

        try
        {
            await _vkApi.AuthorizeAsync(new ApiAuthParams()
            {
                ApplicationId = _appId,
                Login = user.Login,
                Password = user.Password,
                Settings = Settings.Wall
            });

            _accessToken = _vkApi.Token;
        }
        catch (Exception e)
        {
            result.AddError(e);
        }

        return result;
    }

    public bool IsAuthorized() => _accessToken is not null;
}