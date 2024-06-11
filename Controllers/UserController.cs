using LogApi.Functions;
using LogApi.Interfaces;
using LogApi.Models.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace LogApi.Controllers;

public class UserController : ApiControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserQueryInterface _userQuery;
    
    public UserController(ILogger<UserController> logger, UserQueryInterface userQuery)
    {
        _logger = logger;
        _userQuery = userQuery;
    }
    
    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<CreateUserResponse>> createUser([FromForm] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0]);
        }

        var isEmailExist = _userQuery.GetUserByEmail(request.Email, cancellationToken);

        if (isEmailExist is not null)
        {
            return Conflict($"Email should be unique");
        }

        var hashedUserRequest = request with { Password = request.Password.Encrypt() };

        var result = await _userQuery.CreateUser(hashedUserRequest, cancellationToken);

        return Ok(new CreateUserResponse(result.userId));
    }
}