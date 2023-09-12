namespace PandaTech.TestCase1.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException(string entityDescription)
        : base($"Entity '{entityDescription}' already exists")
    {
    }
    
    protected EntityAlreadyExistsException(string entityDescription, string entityName)
        : base($"{entityName} '{entityDescription}' already exists")
    {
    }
}

public class EntityAlreadyExistsException<T> : EntityAlreadyExistsException
{
    public EntityAlreadyExistsException(string entityDescription)
        : base(entityDescription, typeof(T).Name)
    {
    }
}
