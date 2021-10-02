namespace Flux.DataStructures
{
  public interface IOrderedSet<T>
    : System.Collections.Generic.ISet<T>
      where T : notnull
  {
    int GetIndex(T value);

    void Insert(int index, T value);

    T this[int index] { get; set; }
  }
}
