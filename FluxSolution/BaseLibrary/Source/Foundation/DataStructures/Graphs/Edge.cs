namespace Flux.DataStructures.Graphs
{
  public class Edge<TKey, TValue>
    : System.IEquatable<Edge<TKey, TValue>>
    where TKey : System.IComparable<TKey>, System.IEquatable<TKey>
    where TValue : System.IComparable<TValue>, System.IEquatable<TValue>
  {
    public Edge(TKey sourceKey, TKey targetKey, bool isDirected, params TValue[] values)
    {
      SourceKey = sourceKey;
      TargetKey = targetKey;
      IsDirected = isDirected;
      Values = new System.Collections.Generic.List<TValue>(values);
    }

    public TKey SourceKey { get; }
    public TKey TargetKey { get; }
    public System.Collections.Generic.List<TValue> Values { get; set; }

    /// <summary>If true the edge points only from source to target, otherwise the edge goes both ways.</summary>
    public bool IsDirected { get; set; }

    /// <summary>True if source and target are equal, otherwise false.</summary>
    public bool IsLoop
      => SourceKey.Equals(TargetKey);

    //#region Overloaded operators
    //public static bool operator <(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
    //  => a.CompareTo(b) < 0;
    //public static bool operator >(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
    //  => a.CompareTo(b) > 0;
    //public static bool operator <=(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
    //  => a.CompareTo(b) <= 0;
    //public static bool operator >=(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
    //  => a.CompareTo(b) >= 0;

    //public static bool operator ==(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
    //  => a.Equals(b);
    //public static bool operator !=(Edge<TKey, TValue> a, Edge<TKey, TValue> b)
    //  => !a.Equals(b);
    //#endregion Overloaded operators

    //#region Implemented interfaces
    //// IComparable<>
    //public int CompareTo(Edge<TKey, TValue> other)
    //  => SourceKey.CompareTo(other.SourceKey) is var csk && csk != 0
    //  ? csk
    //  : TargetKey.CompareTo(other.TargetKey) is var ctk && ctk != 0
    //  ? ctk
    //  : Value.CompareTo(other.Value) is var cv && cv != 0
    //  ? cv
    //  : IsDirected && !other.IsDirected
    //  ? -1
    //  : !IsDirected && other.IsDirected
    //  ? 1
    //  : 0;
    //// IEquatable<>
    //public bool Equals(Edge<TKey, TValue> other)
    //  => SourceKey.Equals(other.SourceKey)
    //  && TargetKey.Equals(other.TargetKey)
    //  && Value.EqualsEx(other.Value)
    //  && IsDirected == other.IsDirected;
    public bool Equals(Edge<TKey, TValue>? other)
    {
      throw new System.NotImplementedException();
    }
    //#endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Edge<TKey, TValue> o && Equals(o);
    public override int GetHashCode()
    {
      var hc = new System.HashCode();
      hc.Add(SourceKey);
      hc.Add(TargetKey);
      hc.Add(IsDirected);
      Values.ForEach(v => hc.Add(v));
      return hc.ToHashCode();
    }
    public override string ToString()
      => $"<{nameof(Edge<TKey, TValue>)}: {SourceKey}, {TargetKey} (Directed: {IsDirected}, {Values.Count})>";
    #endregion Object overrides
  }
}
