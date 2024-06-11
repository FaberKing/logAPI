using LogApi.Models.DTOs.Login;

namespace LogApi.Interfaces;

public interface LoginQueryInterface
{
    public Task<LoginUserResponse> LoginUser(LoginUserRequest request);
}