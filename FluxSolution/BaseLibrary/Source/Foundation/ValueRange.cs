namespace Flux
{
  /// <summary>Represents a value range of two components, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</summary>
  public struct ValueRange<T>
    : System.IEquatable<ValueRange<T>>
    where T : System.IComparable<T>, System.IEquatable<T>
  {
    public readonly static ValueRange<T> Empty;

    private readonly T m_lo;
    private readonly T m_hi;

    public ValueRange(T low, T high)
    {
      m_lo = low;
      m_hi = high;
    }

    public T Low
      => m_lo;
    public T High
      => m_hi;

    public bool IsValid
      => m_lo.CompareTo(m_hi) < 0 && m_hi.CompareTo(m_lo) > 0;

    #region Static methods
    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    public static System.Collections.Generic.List<ValueRange<T>> Difference(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new ValueRange<T>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new ValueRange<T>(b.m_hi, a.m_hi));
      }
      else list.Add(a);

      return list;
    }
    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    public static ValueRange<T> Intersect(ValueRange<T> a, ValueRange<T> b)
      => IsOverlapping(a, b) ? new ValueRange<T>(MaxLo(a, b), MinHi(a, b)) : Empty;
    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    public static System.Collections.Generic.List<ValueRange<T>> SymmetricDifference(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new ValueRange<T>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new ValueRange<T>(b.m_hi, a.m_hi));
        if (a.m_hi.CompareTo(b.m_hi) < 0)
          list.Add(new ValueRange<T>(a.m_hi, b.m_hi));
      }
      else list.Add(a);

      return list;
    }
    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    public static ValueRange<T> Union(ValueRange<T> a, ValueRange<T> b)
      => IsOverlapping(a, b) ? new ValueRange<T>(MinLo(a, b), MaxHi(a, b)) : Empty;
    public static ValueRange<T> UnionAll(ValueRange<T> a, ValueRange<T> b)
      => new(MinLo(a, b), MaxHi(a, b));

    public static bool IsOverlapping(ValueRange<T> a, ValueRange<T> b)
      => a.m_lo.CompareTo(b.m_hi) < 0 && b.m_lo.CompareTo(a.m_hi) < 0;

    public static T MaxHi(ValueRange<T> a, ValueRange<T> b)
      => a.m_hi.CompareTo(b.m_hi) >= 0 ? a.m_hi : b.m_hi;
    public static T MaxLo(ValueRange<T> a, ValueRange<T> b)
      => a.m_lo.CompareTo(b.m_lo) >= 0 ? a.m_lo : b.m_lo;
    public static T MinHi(ValueRange<T> a, ValueRange<T> b)
      => a.m_hi.CompareTo(b.m_hi) <= 0 ? a.m_hi : b.m_hi;
    public static T MinLo(ValueRange<T> a, ValueRange<T> b)
      => a.m_lo.CompareTo(b.m_lo) <= 0 ? a.m_lo : b.m_lo;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(ValueRange<T> a, ValueRange<T> b)
      => a.Equals(b);
    public static bool operator !=(ValueRange<T> a, ValueRange<T> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    public bool Equals(ValueRange<T> other)
      => m_lo.Equals(other.m_lo) && m_hi.Equals(other.m_hi);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ValueRange<T> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_lo, m_hi);
    public override string ToString()
      => $"{GetType().Name} {{ Low = {m_lo}, High = {m_hi} }}";
    #endregion Object overrides
  }
}
