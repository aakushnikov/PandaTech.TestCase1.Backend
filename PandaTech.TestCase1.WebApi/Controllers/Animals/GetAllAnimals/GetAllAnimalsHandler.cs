using LanguageExt;
using LanguageExt.Common;
using MediatR;
using PandaTech.TestCase1.Data;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.GetAllAnimals;

public sealed class GetAllAnimalsHandler : IRequestHandler<GetAllAnimalsQuery, Either<Error, GetAllAnimalsOut>>
{
    private readonly IStorage _storage;
    private readonly GetAllAnimalsValidator _getAllAnimalsValidator = new ();

    public GetAllAnimalsHandler(IStorage storage) =>
        _storage = storage;
    
    public async Task<Either<Error, GetAllAnimalsOut>> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _getAllAnimalsValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Error.New(StatusCodes.Status400BadRequest, string.Join(';', validationResult.Errors));

        return new GetAllAnimalsOut
        {
            Animals = _storage.Animals.Skip((request.Page - 1) * request.Limit).Take(request.Limit),
            Page = request.Page,
            Limit = request.Limit,
            Total = _storage.Animals.Length(),
        };
    }
}