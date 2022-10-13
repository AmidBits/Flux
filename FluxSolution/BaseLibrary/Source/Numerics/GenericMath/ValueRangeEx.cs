#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Represents a value range of two components, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</summary>
  public readonly struct ValueRangeEx<TSelf>
    : System.IEquatable<ValueRangeEx<TSelf>>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public readonly static ValueRangeEx<TSelf> Empty;

    private readonly TSelf m_lo;
    private readonly TSelf m_hi;

    public ValueRangeEx(TSelf low, TSelf high)
    {
      m_lo = low;
      m_hi = high;
    }

    [System.Diagnostics.Contracts.Pure] public TSelf Low => m_lo;
    [System.Diagnostics.Contracts.Pure] public TSelf High => m_hi;

    [System.Diagnostics.Contracts.Pure] public bool IsProper => m_lo.CompareTo(m_hi) < 0 && m_hi.CompareTo(m_lo) > 0;

    [System.Diagnostics.Contracts.Pure] public TSelf Distance => m_hi - m_lo;

    [System.Diagnostics.Contracts.Pure] public TSelf Fold(TSelf value) => value.Fold(m_lo, m_hi);

    [System.Diagnostics.Contracts.Pure] public TSelf Rescale(TSelf value, ValueRangeEx<TSelf> targetRange) => value.Rescale(m_lo, m_hi, targetRange.m_lo, targetRange.m_hi);

    [System.Diagnostics.Contracts.Pure] public TSelf Wrap(TSelf value) => value.Wrap(m_lo, m_hi);

    [System.Diagnostics.Contracts.Pure]
    public TMu InterpolateCosine<TMu>(TMu mu)
      where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TSelf, TMu>, System.Numerics.ITrigonometricFunctions<TMu>
      => new InterpolationCosine<TSelf, TMu>().Interpolate2Node(m_lo, m_hi, mu);

    [System.Diagnostics.Contracts.Pure]
    public TMu InterpolateLinear<TMu>(TMu mu)
      where TMu : System.Numerics.IFloatingPoint<TMu>, System.Numerics.IMultiplyOperators<TMu, TSelf, TMu>
      => new InterpolationLinear<TSelf, TMu>().Interpolate2Node(m_lo, m_hi, mu);

    #region Static methods

    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRangeEx<TSelf> Intersect(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRangeEx<TSelf>(MaxLo(a, b), MinHi(a, b)) : Empty;

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<ValueRangeEx<TSelf>> LeftDifference(ValueRangeEx<TSelf> left, ValueRangeEx<TSelf> right)
    {
      var list = new System.Collections.Generic.List<ValueRangeEx<TSelf>>();

      if (IsOverlapping(left, right))
      {
        if (left.m_lo.CompareTo(right.m_lo) < 0)
          list.Add(new ValueRangeEx<TSelf>(left.m_lo, right.m_lo));
        if (right.m_hi.CompareTo(left.m_hi) < 0)
          list.Add(new ValueRangeEx<TSelf>(right.m_hi, left.m_hi));
      }
      else list.Add(left);

      return list;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<ValueRangeEx<TSelf>> RightDifference(ValueRangeEx<TSelf> left, ValueRangeEx<TSelf> right)
    {
      var list = new System.Collections.Generic.List<ValueRangeEx<TSelf>>();

      if (IsOverlapping(left, right))
      {
        if (right.m_lo.CompareTo(left.m_lo) < 0)
          list.Add(new ValueRangeEx<TSelf>(right.m_lo, left.m_lo));
        if (left.m_hi.CompareTo(right.m_hi) < 0)
          list.Add(new ValueRangeEx<TSelf>(left.m_hi, right.m_hi));
      }
      else list.Add(right);

      return list;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<ValueRangeEx<TSelf>> SymmetricDifference(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRangeEx<TSelf>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new ValueRangeEx<TSelf>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new ValueRangeEx<TSelf>(b.m_hi, a.m_hi));
        if (a.m_hi.CompareTo(b.m_hi) < 0)
          list.Add(new ValueRangeEx<TSelf>(a.m_hi, b.m_hi));
      }
      else list.Add(a);

      return list;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ValueRangeEx<TSelf> Union(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => IsOverlapping(a, b) ? new ValueRangeEx<TSelf>(MinLo(a, b), MaxHi(a, b)) : Empty;

    [System.Diagnostics.Contracts.Pure]
    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A and B, unconditionally.</summary>
    public static ValueRangeEx<TSelf> UnionAll(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => new(MinLo(a, b), MaxHi(a, b));

    [System.Diagnostics.Contracts.Pure]
    public static TSelf Rescale(TSelf value, ValueRangeEx<TSelf> source, ValueRangeEx<TSelf> target)
      => value.Rescale(source.m_lo, source.m_hi, target.m_lo, target.m_hi);

    [System.Diagnostics.Contracts.Pure]
    public static bool IsOverlapping(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_lo.CompareTo(b.m_hi) < 0 && b.m_lo.CompareTo(a.m_hi) < 0;

    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxHi(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_hi.CompareTo(b.m_hi) >= 0 ? a.m_hi : b.m_hi;

    [System.Diagnostics.Contracts.Pure]
    public static TSelf MaxLo(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_lo.CompareTo(b.m_lo) >= 0 ? a.m_lo : b.m_lo;

    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinHi(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_hi.CompareTo(b.m_hi) <= 0 ? a.m_hi : b.m_hi;

    [System.Diagnostics.Contracts.Pure]
    public static TSelf MinLo(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.m_lo.CompareTo(b.m_lo) <= 0 ? a.m_lo : b.m_lo;

    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(ValueRangeEx<TSelf> a, ValueRangeEx<TSelf> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(ValueRangeEx<TSelf> other)
      => m_lo.Equals(other.m_lo) && m_hi.Equals(other.m_hi);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is ValueRange<TSelf> o && Equals(o);
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
