using LogApi.Functions;
using LogApi.Interfaces;
using LogApi.Models.DTOs.Login;
using Microsoft.AspNetCore.Mvc;

namespace LogApi.Controllers;

public class LoginController : ApiControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly UserQueryInterface _userQuery;
    private readonly IConfiguration _configuration;

    public LoginController(ILogger<LoginController> logger, UserQueryInterface userQuery, IConfiguration configuration)
    {
        _logger = logger;
        _userQuery = userQuery;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginUserResponse>> loginUser([FromForm] LoginUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request : try to login the user");
        var validator = new LoginUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        var userData = await _userQuery.GetUserByEmail(request.Email, cancellationToken);
        
        if (userData is null)
        {
            return NotFound($"Üser with email {request.Email} is not found.");
        }

        var decryptedPassword = userData.password.Decrypt();

        if (!decryptedPassword.Equals(request.Password))
        {
            return BadRequest("Wrong Email or Password");
        }

        var token = request.Authenticate(_configuration);

        _logger.LogInformation($"Response : successfully created token for user {request.Email}");
        return Ok(new LoginUserResponse(token));
    }
}