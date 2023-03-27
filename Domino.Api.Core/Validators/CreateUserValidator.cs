using Domino.Api.Core.Dtos;
using FluentValidation;

namespace Domino.Api.Core.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(40)
            .WithMessage("First name must be less than 40 characters");

        RuleFor(x => x.LastName)
            .MaximumLength(40)
            .WithMessage("Last name must be less than 40 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Email must be less than 50 characters")
            .EmailAddress()
            .WithMessage("Email must be a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(60)
            .WithMessage("Password must be less than 60 characters")
            .Matches("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])")
            .WithMessage("Password must contain at least one uppercase letter, one number and one special character");
    }
}
