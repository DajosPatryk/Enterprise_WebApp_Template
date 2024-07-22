namespace server.Application.Validators;

using FluentValidation;
using Data.Entities;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        // Email address validation happens in Entity already, this is unnecessary.
        RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be valid");
        RuleFor(u => u.Sub).SetValidator(new UserSubValidator());
    }
}

public class UserSubValidator : AbstractValidator<string>
{
    public UserSubValidator()
    {
        RuleFor(sub => sub).NotEmpty().WithMessage("Sub can't be empty");
    }
}