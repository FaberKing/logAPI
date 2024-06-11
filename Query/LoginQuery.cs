using LogApi.Interfaces;
using LogApi.Models.DTOs.Login;

namespace LogApi.Query;

public class LoginQuery : LoginQueryInterface
{
    private readonly ILogger<LoginQuery> _logger;

    public LoginQuery(ILogger<LoginQuery> logger)
    {
        _logger = logger;
    }

    public Task<LoginUserResponse> LoginUser(LoginUserRequest request)
    {
        throw new NotImplementedException();
    }
}