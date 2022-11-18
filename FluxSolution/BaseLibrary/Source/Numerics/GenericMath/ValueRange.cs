namespace Flux
{
  /// <summary>Represents a value range of two components, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</summary>
  public readonly record struct ValueRange<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public readonly static ValueRange<TSelf> Empty;

    private readonly TSelf m_high;
    private readonly TSelf m_low;

    public ValueRange(TSelf low, TSelf high)
    {
      m_low = low;
      m_high = high;
    }

    [System.Diagnostics.Contracts.Pure] public TSelf High => m_high;
    [System.Diagnostics.Contracts.Pure] public TSelf Low => m_low;

    [System.Diagnostics.Contracts.Pure] public bool IsProper => m_low < m_high && m_high > m_low;

    [System.Diagnostics.Contracts.Pure] public TSelf Distance => m_high - m_low;

    #region Static methods

    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRange<TSelf> Intersect(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRange<TSelf>(MaxLow(a, b), MinHigh(a, b)) : Empty;

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<ValueRange<TSelf>> LeftDifference(ValueRange<TSelf> left, ValueRange<TSelf> right)
    {
      if (IsOverlapping(left, right))
      {
        if (left.m_low < right.m_low)
          yield return new ValueRange<TSelf>(left.m_low, right.m_low);
        if (right.m_high < left.m_high)
          yield return new ValueRange<TSelf>(right.m_high, left.m_high);
      }
      else yield return left;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<ValueRange<TSelf>> RightDifference(ValueRange<TSelf> left, ValueRange<TSelf> right)
    {
      if (IsOverlapping(left, right))
      {
        if (right.m_low < left.m_low)
          yield return new ValueRange<TSelf>(right.m_low, left.m_low);
        if (left.m_high < right.m_high)
          yield return new ValueRange<TSelf>(left.m_high, right.m_high);
      }
      else yield return right;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<ValueRange<TSelf>> SymmetricDifference(ValueRange<TSelf> a, ValueRange<TSelf> b)
    {
      if (IsOverlapping(a, b))
      {
        if (a.m_low < b.m_low)
          yield return new ValueRange<TSelf>(a.m_low, b.m_low);
        if (b.m_high < a.m_high)
          yield return new ValueRange<TSelf>(b.m_high, a.m_high);
        if (a.m_high < b.m_high)
          yield return new ValueRange<TSelf>(a.m_high, b.m_high);
      }
      else yield return a;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRange<TSelf> Union(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRange<TSelf>(MinLow(a, b), MaxHigh(a, b)) : Empty;

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A and B, unconditionally.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRange<TSelf> UnionAll(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => new(MinLow(a, b), MaxHigh(a, b));

    /// <summary>Returns whether a and b are overlapping.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsOverlapping(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => a.m_low < b.m_high && b.m_low < a.m_high;

    /// <summary>Returns the maximum high value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxHigh(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => a.m_high >= b.m_high ? a.m_high : b.m_high;

    /// <summary>Returns the maximum low value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxLow(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => a.m_low >= b.m_low ? a.m_low : b.m_low;

    /// <summary>Returns the minimum high value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinHigh(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => a.m_high <= b.m_high ? a.m_high : b.m_high;

    /// <summary>Returns the minimum low value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinLow(ValueRange<TSelf> a, ValueRange<TSelf> b)
      => a.m_low <= b.m_low ? a.m_low : b.m_low;

    #endregion Static methods
  }
}
