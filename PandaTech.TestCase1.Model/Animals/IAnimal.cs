namespace PandaTech.TestCase1.Model.Animals;

public interface IAnimal : IComparable
{
    string Name { get; }
    Guid Id { get; }
}