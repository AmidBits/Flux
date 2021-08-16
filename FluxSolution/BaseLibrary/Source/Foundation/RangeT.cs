namespace Flux
{
  public struct Range<T>
    : System.IEquatable<Range<T>>
    where T : System.IComparable<T>, System.IEquatable<T>
  {
    public static Range<T> Empty;

    private readonly T m_lo;
    private readonly T m_hi;

    public Range(T low, T high)
    {
      m_lo = low;
      m_hi = high;
    }

    public T Low => m_lo;
    public T High => m_hi;

    public bool IsEmpty
      => Equals(Empty);
    public bool IsValid
       => m_lo.CompareTo(m_hi) < 0;

    #region Static methods
    public static System.Collections.Generic.List<Range<T>> Difference(Range<T> a, Range<T> b)
    {
      var list = new System.Collections.Generic.List<Range<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new Range<T>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new Range<T>(b.m_hi, a.m_hi));
      }
      else list.Add(a);

      return list;
    }
    public static Range<T> Intersect(Range<T> a, Range<T> b)
      => IsOverlapping(a, b) ? new Range<T>(MaxLo(a, b), MinHi(a, b)) : Empty;
    public static System.Collections.Generic.List<Range<T>> SymmetricDifference(Range<T> a, Range<T> b)
    {
      var list = new System.Collections.Generic.List<Range<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new Range<T>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new Range<T>(b.m_hi, a.m_hi));
        if (a.m_hi.CompareTo(b.m_hi) < 0)
          list.Add(new Range<T>(a.m_hi, b.m_hi));
      }
      else list.Add(a);

      return list;
    }
    public static Range<T> Union(Range<T> a, Range<T> b)
      => IsOverlapping(a, b) ? new Range<T>(MinLo(a, b), MaxHi(a, b)) : Empty;
    public static Range<T> UnionAll(Range<T> a, Range<T> b)
      => new Range<T>(MinLo(a, b), MaxHi(a, b));

    public static bool IsOverlapping(Range<T> a, Range<T> b)
      => a.m_lo.CompareTo(b.m_hi) < 0 && b.m_lo.CompareTo(a.m_hi) < 0;

    public static T MaxHi(Range<T> a, Range<T> b)
      => a.m_hi.CompareTo(b.m_hi) >= 0 ? a.m_hi : b.m_hi;
    public static T MaxLo(Range<T> a, Range<T> b)
      => a.m_lo.CompareTo(b.m_lo) >= 0 ? a.m_lo : b.m_lo;
    public static T MinHi(Range<T> a, Range<T> b)
      => a.m_hi.CompareTo(b.m_hi) <= 0 ? a.m_hi : b.m_hi;
    public static T MinLo(Range<T> a, Range<T> b)
      => a.m_lo.CompareTo(b.m_lo) <= 0 ? a.m_lo : b.m_lo;
    #endregion Static methods

    #region Implemented interfaces
    public bool Equals(Range<T> other)
      => m_lo.Equals(other.m_lo) && m_hi.Equals(other.m_hi);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Range<T> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_lo, m_hi);
    public override string ToString()
      => $"<{GetType().Name}: {m_lo}, {m_hi}>";
    #endregion Object overrides
  }
}
