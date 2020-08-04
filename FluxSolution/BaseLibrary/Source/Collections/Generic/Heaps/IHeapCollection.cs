namespace Flux.Collections.Generic
{
  public interface IHeapCollection<T>
    : System.Collections.Generic.IEnumerable<T>
  {
    int Count { get; }
    T Extract();
    void Insert(T item);
    bool IsEmpty { get; }
    T Peek();
  }
}
