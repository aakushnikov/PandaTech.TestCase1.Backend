using FluentValidation;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.RemoveAnimal;

public sealed class RemoveAnimalValidator : AbstractValidator<RemoveAnimalCommand>
{
    public RemoveAnimalValidator()
    {
        When(query => query.Id is null, () =>
            {
                RuleFor(query => query.Name)
                    .Must(name => !string.IsNullOrWhiteSpace(name))
                    .WithMessage(
                        $"{nameof(RemoveAnimalCommand.Name)} cannot be null, empty or white-spaced with null Id");
            })
            .Otherwise(() =>
            {
                RuleFor(query => query.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage($"{nameof(RemoveAnimalCommand.Id)} cannot be empty {nameof(Guid)}");
            });
    }
}