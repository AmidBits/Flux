#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Represents a value range of two components, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</summary>
  public readonly record struct ValueRangeEx<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public readonly static ValueRangeEx<TSelf> Empty;

    private readonly TSelf m_high;
    private readonly TSelf m_low;

    public ValueRangeEx(TSelf low, TSelf high)
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
    public static ValueRangeEx<TSelf> Intersect(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRangeEx<TSelf>(MaxLow(a, b), MinHigh(a, b)) : Empty;

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<ValueRangeEx<TSelf>> LeftDifference(ValueRangeEx<TSelf> left, ValueRangeEx<TSelf> right)
    {
      if (IsOverlapping(left, right))
      {
        if (left.m_low < right.m_low)
          yield return new ValueRangeEx<TSelf>(left.m_low, right.m_low);
        if (right.m_high < left.m_high)
          yield return new ValueRangeEx<TSelf>(right.m_high, left.m_high);
      }
      else yield return left;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<ValueRangeEx<TSelf>> RightDifference(ValueRangeEx<TSelf> left, ValueRangeEx<TSelf> right)
    {
      if (IsOverlapping(left, right))
      {
        if (right.m_low < left.m_low)
          yield return new ValueRangeEx<TSelf>(right.m_low, left.m_low);
        if (left.m_high < right.m_high)
          yield return new ValueRangeEx<TSelf>(left.m_high, right.m_high);
      }
      else yield return right;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<ValueRangeEx<TSelf>> SymmetricDifference(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
    {
      if (IsOverlapping(a, b))
      {
        if (a.m_low < b.m_low)
          yield return new ValueRangeEx<TSelf>(a.m_low, b.m_low);
        if (b.m_high < a.m_high)
          yield return new ValueRangeEx<TSelf>(b.m_high, a.m_high);
        if (a.m_high < b.m_high)
          yield return new ValueRangeEx<TSelf>(a.m_high, b.m_high);
      }
      else yield return a;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRangeEx<TSelf> Union(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRangeEx<TSelf>(MinLow(a, b), MaxHigh(a, b)) : Empty;

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A and B, unconditionally.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRangeEx<TSelf> UnionAll(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => new(MinLow(a, b), MaxHigh(a, b));

    /// <summary>Returns whether a and b are overlapping.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsOverlapping(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_low < b.m_high && b.m_low < a.m_high;

    /// <summary>Returns the maximum high value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxHigh(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_high >= b.m_high ? a.m_high : b.m_high;

    /// <summary>Returns the maximum low value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxLow(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_low >= b.m_low ? a.m_low : b.m_low;

    /// <summary>Returns the minimum high value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinHigh(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_high <= b.m_high ? a.m_high : b.m_high;

    /// <summary>Returns the minimum low value of a and b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinLow(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_low <= b.m_low ? a.m_low : b.m_low;

    #endregion Static methods
  }
}
#endif
