using FluentValidation;

namespace LogApi.Models.DTOs.Login;

public record LoginUserRequest(string Email, string Password);

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull();
    }
}