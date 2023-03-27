using Domino.Api.Core.Dtos.User;
using FluentValidation;

namespace Domino.Api.Core.Validators;

public class UserLogInValidator : AbstractValidator<UserLogInDto>
{
    public UserLogInValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Email must be less than 50 characters")
            .EmailAddress()
            .WithMessage("Email must be a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(60)
            .WithMessage("Password must be less than 60 characters");
    }
}
