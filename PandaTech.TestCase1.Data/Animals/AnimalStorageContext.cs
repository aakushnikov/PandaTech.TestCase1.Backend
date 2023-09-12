using System.Collections;
using System.Collections.Concurrent;
using PandaTech.TestCase1.Exceptions;
using PandaTech.TestCase1.Model.Animals;

namespace PandaTech.TestCase1.Data.Animals;

public sealed class AnimalStorageContext : IStorageContext<IAnimal>
{
    private readonly ConcurrentBag<IAnimal> _animals = new ();
    
    public IEnumerable<IAnimal> Items => _animals;

    public void Add(IAnimal item)
    {
        if (_animals.Any(a => string.Compare(a.Name, item.Name, StringComparison.CurrentCultureIgnoreCase) == 0))
            throw new EntityAlreadyExistsException<IAnimal>(item.Name);
        
        if (_animals.Any(a => a.CompareTo(item) == 0))
            throw new EntityAlreadyExistsException<IAnimal>(item.Name);
        
        _animals.Add(item);
    }

    public void Remove(IAnimal item)
    {
        var result = _animals.FirstOrDefault(a => a.CompareTo(item) == 0);
        
        if (result is null)
            throw new EntityNotFoundException<IAnimal>(item.Name);

        _animals.TryTake(out result);
    }

    public IEnumerator<IAnimal> GetEnumerator() => _animals.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}