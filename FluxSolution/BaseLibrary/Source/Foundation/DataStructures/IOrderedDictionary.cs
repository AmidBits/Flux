namespace Flux.DataStructures
{
  public interface IOrderedDictionary<TKey, TValue>
    : System.Collections.Generic.IDictionary<TKey, TValue>
      where TKey : notnull
  {
    int GetIndex(TKey key);
    int GetIndex(TValue value);

    TKey GetKey(int index);
    TKey GetKey(TValue value);

    void Insert(int index, TKey key, TValue value);

    TValue this[int index] { get; set; }
  }
}
