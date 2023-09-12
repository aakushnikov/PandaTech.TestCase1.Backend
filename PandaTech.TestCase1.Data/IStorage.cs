using PandaTech.TestCase1.Model.Animals;

namespace PandaTech.TestCase1.Data;

public interface IStorage
{
    IStorageContext<IAnimal> Animals { get; }
}