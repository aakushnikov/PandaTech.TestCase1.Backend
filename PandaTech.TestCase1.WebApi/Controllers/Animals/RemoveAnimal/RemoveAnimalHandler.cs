using LanguageExt;
using LanguageExt.Common;
using MediatR;
using PandaTech.TestCase1.Data;
using Unit = MediatR.Unit;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.RemoveAnimal;

public sealed class RemoveAnimalHandler : IRequestHandler<RemoveAnimalCommand, Either<Error, Unit>>
{
    private readonly IStorage _storage;
    private readonly RemoveAnimalValidator _removeAnimalValidator = new ();

    public RemoveAnimalHandler(IStorage storage) =>
        _storage = storage;
    
    public async Task<Either<Error, Unit>> Handle(RemoveAnimalCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _removeAnimalValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Error.New(StatusCodes.Status400BadRequest, string.Join(';', validationResult.Errors));

        try
        {
            var animal = _storage.Animals.First(a =>
                a.Id.Equals(request.Id) || a.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
            _storage.Animals.Remove(animal);
            return Unit.Value;
        }
        catch (InvalidOperationException exception)
        {
            return Error.New(StatusCodes.Status404NotFound, exception.Message);
        }
        catch(Exception exception)
        {
            return Error.New(StatusCodes.Status409Conflict, exception.Message);
        }
    }
}