using PandaTech.TestCase1.Data.Animals;
using PandaTech.TestCase1.Model.Animals;

namespace PandaTech.TestCase1.Data;

public sealed class MemoryStorage : IStorage
{
    private readonly AnimalStorageContext _animalStorageContext = new ();
    public IStorageContext<IAnimal> Animals => _animalStorageContext;

    public MemoryStorage()
    {
        #if DEBUG
        Parallel.For(1, 100, i => _animalStorageContext.Add(Animal.Create($"{nameof(Animal)}-{i}")));
        #endif
    }
}