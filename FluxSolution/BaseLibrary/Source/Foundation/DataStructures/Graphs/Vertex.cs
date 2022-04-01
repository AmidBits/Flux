namespace Flux
{
  namespace DataStructures.Graphs
  {
    public class Vertex<TKey, TValue>
    : System.IEquatable<Vertex<TKey, TValue>>
    where TKey : System.IEquatable<TKey>
    where TValue : System.IEquatable<TValue>
    {
      TKey Key { get; }
      TValue Value { get; }

      public Vertex(TKey key, TValue value)
      {
        Key = key;
        Value = value;
      }

      public bool Equals(Vertex<TKey, TValue>? other)
        => Key.Equals(other.Key) && Value.Equals(other.Value);

      public override bool Equals(object? obj)
        => obj is Vertex<TKey, TValue> o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(Key, Value);
      public override string ToString()
        => $"{GetType().Name} {{ {Key} = {Value} }}";
    }
  }
}
