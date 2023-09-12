using FluentValidation;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.AddAnimal;

public sealed class AddAnimalValidator : AbstractValidator<AddAnimalCommand>
{
    public AddAnimalValidator()
    {
        RuleFor(query => query.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage($"{nameof(AddAnimalCommand.Name)} cannot be null, empty or white-spaced.");
    }
}