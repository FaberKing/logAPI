using LogApi.Models.DTOs.User;

namespace LogApi.Interfaces;

public interface UserQueryInterface
{
    public Task<CreateUserResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
    public Task<GetUserResponse?> GetUserByEmail(string email, CancellationToken cancellationToken);
}