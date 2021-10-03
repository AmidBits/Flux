namespace Flux.DataStructures.Graphs
{
  public class Edge<TKey, TValue>
    : System.IEquatable<Edge<TKey, TValue>>
    where TKey : System.IComparable<TKey>, System.IEquatable<TKey>
    where TValue : System.IComparable<TValue>, System.IEquatable<TValue>
  {
    public Edge(TKey sourceKey, TKey targetKey, bool isDirected, TValue value)
    {
      SourceKey = sourceKey;
      TargetKey = targetKey;
      IsDirected = isDirected;
      Value = value;
    }

    /// <summary>If true the edge points only from source to target, i.e. it is directed, otherwise the edge goes both ways, i.e. it is undirected.</summary>
    public bool IsDirected { get; set; }

    /// <summary>True if source and target are equal, otherwise false.</summary>
    public bool IsLoop
      => SourceKey.Equals(TargetKey);

    /// <summary>The source endpoint of the edge.</summary>
    public TKey SourceKey { get; }
    /// <summary>The target endpoint of the edge.</summary>
    public TKey TargetKey { get; }

    /// <summary>The user value associated with the edge.</summary>
    public TValue Value { get; set; }

    #region Overloaded operators
    public static bool operator <(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
      => a.CompareTo(b) < 0;
    public static bool operator >(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
      => a.CompareTo(b) > 0;
    public static bool operator <=(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
      => a.CompareTo(b) <= 0;
    public static bool operator >=(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
      => a.Equals(b);
    public static bool operator !=(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(Edge<TKey, TValue> other)
      => SourceKey.CompareTo(other.SourceKey) is var csk && csk != 0
      ? csk
      : TargetKey.CompareTo(other.TargetKey) is var ctk && ctk != 0
      ? ctk
      : Value.CompareTo(other.Value) is var cv && cv != 0
      ? cv
      : IsDirected && !other.IsDirected
      ? -1
      : !IsDirected && other.IsDirected
      ? 1
      : 0;
    // IEquatable<>
    public bool Equals(Edge<TKey, TValue>? other)
      => other is not null
      && SourceKey.Equals(other.SourceKey)
      && TargetKey.Equals(other.TargetKey)
      && Value.EqualsEx(other.Value)
      && IsDirected == other.IsDirected;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Edge<TKey, TValue> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(SourceKey, TargetKey, IsDirected, Value);
    public override string ToString()
      => $"<{nameof(Edge<TKey, TValue>)}: {SourceKey}, {TargetKey} ({(IsDirected ? @"Directed" : @"Undirected")}, {Value})>";
    #endregion Object overrides
  }
}
