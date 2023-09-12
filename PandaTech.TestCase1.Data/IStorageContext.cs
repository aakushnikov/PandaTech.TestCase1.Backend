namespace PandaTech.TestCase1.Data;

public interface IStorageContext<T> : IEnumerable<T>
{
    void Add(T item);
    void Remove(T item);
}