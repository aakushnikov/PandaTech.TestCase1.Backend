using LanguageExt;
using LanguageExt.Common;
using MediatR;
using PandaTech.TestCase1.Data;
using PandaTech.TestCase1.Model.Animals;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.AddAnimal;

public sealed class AddAnimalHandler : IRequestHandler<AddAnimalCommand, Either<Error, Guid>>
{
    private readonly IStorage _storage;
    private readonly AddAnimalValidator _addAnimalValidator = new ();

    public AddAnimalHandler(IStorage storage) =>
        _storage = storage;
    
    public async Task<Either<Error, Guid>> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _addAnimalValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Error.New(StatusCodes.Status400BadRequest, string.Join(';', validationResult.Errors));

        try
        {
            var newAnimal = Animal.Create(request.Name);
            _storage.Animals.Add(newAnimal);
            return newAnimal.Id;
        }
        catch(Exception exception)
        {
            return Error.New(StatusCodes.Status409Conflict, exception.Message);
        }
    }
}