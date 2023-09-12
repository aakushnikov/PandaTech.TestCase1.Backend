using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.AddAnimal;

public sealed class AddAnimalCommand : IRequest<Either<Error,Guid>>
{
    public string Name { get; init; } = string.Empty;
}