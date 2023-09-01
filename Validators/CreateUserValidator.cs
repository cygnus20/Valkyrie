using FluentValidation;
using Valkyrie.Models;

namespace Valkyrie.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(p => p.Username).NotEmpty().NotNull();
        RuleFor(p => p.Password).NotEmpty().NotNull();
    }
}
