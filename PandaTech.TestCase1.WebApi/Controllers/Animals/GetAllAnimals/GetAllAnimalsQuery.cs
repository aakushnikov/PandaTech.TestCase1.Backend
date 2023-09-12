using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.GetAllAnimals;

public sealed class GetAllAnimalsQuery : IRequest<Either<Error,GetAllAnimalsOut>>
{
    public int Page { get; init; }
    public int Limit { get; init; }
}