using System.Diagnostics.CodeAnalysis;
using PandaTech.TestCase1.Model.Animals;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals.GetAllAnimals;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class GetAllAnimalsOut
{
    public IEnumerable<IAnimal> Animals { get; init; } = Array.Empty<IAnimal>();
    public int Page { get; init; }
    public int Limit { get; init; }
    public int Total { get; init; }
}