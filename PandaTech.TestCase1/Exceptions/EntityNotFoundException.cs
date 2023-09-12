namespace PandaTech.TestCase1.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityDescription)
        : base($"Entity '{entityDescription}' not found")
    {
    }
    
    protected EntityNotFoundException(string entityDescription, string entityName)
        : base($"{entityName} '{entityDescription}' not found")
    {
    }
}

public class EntityNotFoundException<T> : EntityNotFoundException
{
    public EntityNotFoundException(string entityDescription)
        : base(entityDescription, typeof(T).Name)
    {
    }
}
