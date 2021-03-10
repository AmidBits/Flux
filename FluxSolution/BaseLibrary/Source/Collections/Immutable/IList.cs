namespace Flux.Collections.Immutable
{
  public interface IList<T>
    : System.Collections.Generic.IEnumerable<T>
  {
    bool IsEmpty { get; }
    T Value { get; }
    IList<T> Add(T value);
    bool Contains(T value);
    IList<T> Remove(T value);
  }
}
