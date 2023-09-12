using FluentValidation;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.GetAllAnimals;

public sealed class GetAllAnimalsValidator : AbstractValidator<GetAllAnimalsQuery>
{
    public GetAllAnimalsValidator()
    {
        RuleFor(query => query.Page)
            .GreaterThan(0)
            .WithMessage($"{nameof(GetAllAnimalsQuery.Page)} must be greater then 0");
        
        RuleFor(query => query.Limit)
            .GreaterThan(0)
            .WithMessage($"{nameof(GetAllAnimalsQuery.Limit)} must be greater then 0");
    }
}