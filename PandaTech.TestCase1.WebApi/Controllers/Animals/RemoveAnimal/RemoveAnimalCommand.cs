using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Unit = MediatR.Unit;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.RemoveAnimal;

public sealed class RemoveAnimalCommand : IRequest<Either<Error, Unit>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}