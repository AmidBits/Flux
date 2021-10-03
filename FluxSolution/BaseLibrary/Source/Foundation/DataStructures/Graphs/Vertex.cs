namespace Flux.DataStructures.Graphs
{
  public struct Vertex<TKey, TValue>
    : System.IComparable<Vertex<TKey, TValue>>, System.IEquatable<Vertex<TKey, TValue>>
    where TKey : System.IComparable<TKey>, System.IEquatable<TKey>
    where TValue : System.IComparable<TValue>, System.IEquatable<TValue>
  {
    public Vertex(TKey key, TValue value)
    {
      Key = key;
      Value = value;
    }

    /// <summary>The vertex key (identifier).</summary>
    public TKey Key { get; }

    /// <summary>The user value associated with the vertex.</summary>
    public TValue Value { get; set; }

    #region Overloaded operators
    public static bool operator <(Vertex<TKey, TValue> a, Vertex<TKey, TValue> b)
      => a.CompareTo(b) < 0;
    public static bool operator >(Vertex<TKey, TValue> a, Vertex<TKey, TValue> b)
      => a.CompareTo(b) > 0;
    public static bool operator <=(Vertex<TKey, TValue> a, Vertex<TKey, TValue> b)
      => a.CompareTo(b) <= 0;
    public static bool operator >=(Vertex<TKey, TValue> a, Vertex<TKey, TValue> b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Vertex<TKey, TValue> a, Vertex<TKey, TValue> b)
      => a.Equals(b);
    public static bool operator !=(Vertex<TKey, TValue> a, Vertex<TKey, TValue> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(Vertex<TKey, TValue> other)
      => Key.CompareTo(other.Key) is var cmpKey && cmpKey != 0 ? cmpKey : Value.CompareTo(other.Value) is var cmpValue && cmpValue != 0 ? cmpValue : 0;
    // IEquatable<>
    public bool Equals(Vertex<TKey, TValue> other)
      => Key.Equals(other.Key) && Value.Equals(other.Value);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Vertex<TKey, TValue> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Key, Value);
    public override string ToString()
      => $"<{GetType().Name}: {Key}, Value: {Value}>";
    #endregion Object overrides
  }
}
