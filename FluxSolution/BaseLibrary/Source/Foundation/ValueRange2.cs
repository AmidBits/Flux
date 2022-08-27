#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Represents a value range of two components, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</summary>
  public readonly struct ValueRange2<TSelf>
    : System.IEquatable<ValueRange2<TSelf>>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public readonly static ValueRange2<TSelf> Empty;

    private readonly TSelf m_lo;
    private readonly TSelf m_hi;

    public ValueRange2(TSelf low, TSelf high)
    {
      m_lo = low;
      m_hi = high;
    }

    [System.Diagnostics.Contracts.Pure] public TSelf Low => m_lo;
    [System.Diagnostics.Contracts.Pure] public TSelf High => m_hi;

    [System.Diagnostics.Contracts.Pure] public bool IsProper => m_lo.CompareTo(m_hi) < 0 && m_hi.CompareTo(m_lo) > 0;

    [System.Diagnostics.Contracts.Pure] public TSelf Distance => m_hi - m_lo;

    #region Static methods
    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRange2<TSelf> Intersect(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRange2<TSelf>(MaxLo(a, b), MinHi(a, b)) : Empty;

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<ValueRange2<TSelf>> LeftDifference(ValueRange2<TSelf> left, ValueRange2<TSelf> right)
    {
      var list = new System.Collections.Generic.List<ValueRange2<TSelf>>();

      if (IsOverlapping(left, right))
      {
        if (left.m_lo.CompareTo(right.m_lo) < 0)
          list.Add(new ValueRange2<TSelf>(left.m_lo, right.m_lo));
        if (right.m_hi.CompareTo(left.m_hi) < 0)
          list.Add(new ValueRange2<TSelf>(right.m_hi, left.m_hi));
      }
      else list.Add(left);

      return list;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<ValueRange2<TSelf>> RightDifference(ValueRange2<TSelf> left, ValueRange2<TSelf> right)
    {
      var list = new System.Collections.Generic.List<ValueRange2<TSelf>>();

      if (IsOverlapping(left, right))
      {
        if (right.m_lo.CompareTo(left.m_lo) < 0)
          list.Add(new ValueRange2<TSelf>(right.m_lo, left.m_lo));
        if (left.m_hi.CompareTo(right.m_hi) < 0)
          list.Add(new ValueRange2<TSelf>(left.m_hi, right.m_hi));
      }
      else list.Add(right);

      return list;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<ValueRange2<TSelf>> SymmetricDifference(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRange2<TSelf>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new ValueRange2<TSelf>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new ValueRange2<TSelf>(b.m_hi, a.m_hi));
        if (a.m_hi.CompareTo(b.m_hi) < 0)
          list.Add(new ValueRange2<TSelf>(a.m_hi, b.m_hi));
      }
      else list.Add(a);

      return list;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRange2<TSelf> Union(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRange2<TSelf>(MinLo(a, b), MaxHi(a, b)) : Empty;

    [System.Diagnostics.Contracts.Pure]
    public static ValueRange2<TSelf> UnionAll(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => new(MinLo(a, b), MaxHi(a, b));

    //public static TSelf Fold(TSelf value, ValueRange2<TSelf> range)
    //{
    //  if (value > range.High)
    //  {
    //    var magnitude = value - range.High;

    //    var count = 0;

    //    while(magnitude > range.Distance)
    //    {
    //      magnitude -= range.Distance;

    //      count++;
    //    }

    //    if (count % 2 == 0)
    //      return range.High - magnitude;
    //    else
    //      return range.Low + magnitude;
    //  }
    //}

    public static TSelf Rescale(TSelf value, ValueRange2<TSelf> source, ValueRange2<TSelf> target)
      => target.Distance * (value - source.Low) / source.Distance + target.Low;

    [System.Diagnostics.Contracts.Pure]
    public static TSelf Wrap(TSelf value, ValueRange2<TSelf> range)
      => value < range.Low
      ? range.High - (range.Low - value) % range.Distance
      : value > range.High
      ? range.Low + (value - range.Low) % range.Distance
      : value;

    [System.Diagnostics.Contracts.Pure]
    public static bool IsOverlapping(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => a.m_lo.CompareTo(b.m_hi) < 0 && b.m_lo.CompareTo(a.m_hi) < 0;

    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxHi(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => a.m_hi.CompareTo(b.m_hi) >= 0 ? a.m_hi : b.m_hi;
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxLo(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => a.m_lo.CompareTo(b.m_lo) >= 0 ? a.m_lo : b.m_lo;
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinHi(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => a.m_hi.CompareTo(b.m_hi) <= 0 ? a.m_hi : b.m_hi;
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinLo(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => a.m_lo.CompareTo(b.m_lo) <= 0 ? a.m_lo : b.m_lo;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(ValueRange2<TSelf> a, ValueRange2<TSelf> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(ValueRange2<TSelf> other)
      => m_lo.Equals(other.m_lo) && m_hi.Equals(other.m_hi);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is ValueRange2<TSelf> o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_lo, m_hi);
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => $"{GetType().Name} {{ Low = {m_lo}, High = {m_hi} }}";
    #endregion Object overrides
  }
}
#endif
