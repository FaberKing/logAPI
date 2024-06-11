using LogApi.Interfaces;
using LogApi.Models;
using LogApi.Models.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace LogApi.Query;

public class UserQuery : UserQueryInterface
{
    private readonly SqlServerDbContextInterface _context;
    private readonly ILogger<UserQuery> _logger;
    
    public UserQuery(SqlServerDbContextInterface context, ILogger<UserQuery> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating User");
        var newUser = new UserModel
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = request.Password,
            CreatedAt = DateTimeOffset.Now,
        };

        _context.Users.Add(newUser);

        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("User Created");
        return new CreateUserResponse(Guid.NewGuid());
    }

    public async Task<GetUserResponse?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Find User with Email {email}");

        var user = await _context.Users
            .AsNoTracking()
            .Where(x => x.Email.Equals(email))
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogInformation($"User not found for ID {email}");
            return null;
        }
        
        _logger.LogInformation($"Found user for ID {email}");
        return new GetUserResponse(user.Id, user.Email, user.Password, user.CreatedAt);
    }
}