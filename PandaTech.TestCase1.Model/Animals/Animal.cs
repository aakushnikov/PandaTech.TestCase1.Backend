namespace PandaTech.TestCase1.Model.Animals;

public class Animal : IAnimal
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    private Animal(Guid id, string name) =>
        (Id, Name) = (id, name);

    public static IAnimal Create(string name) =>
        new Animal(
            Guid.NewGuid(),
            string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException($"Argument '{nameof(name)}' cannot be null, empty or white-spaced")
                : name
        );

    public int CompareTo(object? obj)
    {
        if (obj is not Animal animal)
            return -1;

        if (animal.Id != Id)
            return -1;

        return string.Compare(animal.Name, Name, StringComparison.OrdinalIgnoreCase);
    }
}