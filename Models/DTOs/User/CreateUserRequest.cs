using FluentValidation;

namespace LogApi.Models.DTOs.User;

public record CreateUserRequest(string Email, string Password);

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .MinimumLength(8);
    }
}