namespace Flux.DataStructures
{
  public interface IOrderedSet<T>
    : System.Collections.Generic.ISet<T>
      where T : notnull
  {
    T this[int index] { get; set; }

    int GetIndex(T value);

    void Insert(int index, T value);

    void RemoveAt(int index);
  }
}
