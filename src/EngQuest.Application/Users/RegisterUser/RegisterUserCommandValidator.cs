using EngQuest.Domain.Levels;
using FluentValidation;

namespace EngQuest.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithName("Имя");

        RuleFor(c => c.LastName).NotEmpty().WithName("Фамилия");

        RuleFor(c => c.Email).EmailAddress();

        RuleFor(c => c.Level).LessThanOrEqualTo(2).GreaterThan(0);

        RuleFor(c => c.Experience).LessThanOrEqualTo(Level.RequiredXp[2]).GreaterThanOrEqualTo(0);
    }
}
