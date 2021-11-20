namespace Flux
{
  /// <summary>Provides static methods for creating value ranges.</summary>
  public struct ValueRangeEx
  {
    public static ValueRangeEx<T> Create<T>(T low, T high)
      where T : System.IComparable<T>, System.IEquatable<T>
      => new(low, high);
  }

  /// <summary>Represents a value range of two components, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</summary>
  public record struct ValueRangeEx<T>
    where T : System.IComparable<T>, System.IEquatable<T>
  {
    public readonly static ValueRangeEx<T> Empty;

    private readonly T m_lo;
    private readonly T m_hi;

    public ValueRangeEx(T low, T high)
    {
      if (low.CompareTo(high) < 0)
      {
        m_lo = low;
        m_hi = high;
      }
      else if (high.CompareTo(low) < 0)
      {
        m_lo = high;
        m_hi = low;
      }
      else throw new System.ArgumentException(@"Low and high cannot be equal.");
    }

    public T Low
      => m_lo;
    public T High
      => m_hi;

    //public bool IsValid
    //   => m_lo.CompareTo(m_hi) < 0;

    #region Static methods
    public static System.Collections.Generic.List<ValueRangeEx<T>> Difference(ValueRangeEx<T> a, ValueRangeEx<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRangeEx<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new ValueRangeEx<T>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new ValueRangeEx<T>(b.m_hi, a.m_hi));
      }
      else list.Add(a);

      return list;
    }
    public static ValueRangeEx<T> Intersect(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => IsOverlapping(a, b) ? new ValueRangeEx<T>(MaxLo(a, b), MinHi(a, b)) : Empty;
    public static System.Collections.Generic.List<ValueRangeEx<T>> SymmetricDifference(ValueRangeEx<T> a, ValueRangeEx<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRangeEx<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_lo.CompareTo(b.m_lo) < 0)
          list.Add(new ValueRangeEx<T>(a.m_lo, b.m_lo));
        if (b.m_hi.CompareTo(a.m_hi) < 0)
          list.Add(new ValueRangeEx<T>(b.m_hi, a.m_hi));
        if (a.m_hi.CompareTo(b.m_hi) < 0)
          list.Add(new ValueRangeEx<T>(a.m_hi, b.m_hi));
      }
      else list.Add(a);

      return list;
    }
    public static ValueRangeEx<T> Union(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => IsOverlapping(a, b) ? new ValueRangeEx<T>(MinLo(a, b), MaxHi(a, b)) : Empty;
    public static ValueRangeEx<T> UnionAll(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => new(MinLo(a, b), MaxHi(a, b));

    public static bool IsOverlapping(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => a.m_lo.CompareTo(b.m_hi) < 0 && b.m_lo.CompareTo(a.m_hi) < 0;

    public static T MaxHi(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => a.m_hi is var va && b.m_hi is var vb && va.CompareTo(vb) >= 0 ? va : vb;
    public static T MaxLo(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => a.m_lo is var va && b.m_lo is var vb && va.CompareTo(vb) >= 0 ? va : vb;
    public static ValueRangeEx<T> MaxRange(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => new(MinLo(a, b), MaxHi(a, b));

    public static T MinHi(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => a.m_hi is var va && b.m_hi is var vb && va.CompareTo(vb) <= 0 ? va : vb;
    public static T MinLo(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => a.m_lo is var va && b.m_lo is var vb && va.CompareTo(vb) <= 0 ? va : vb;
    public static ValueRangeEx<T> MinRange(ValueRangeEx<T> a, ValueRangeEx<T> b)
      => new(MaxLo(a, b), MinHi(a, b));
    #endregion Static methods

    //#region Object overrides
    //public override string ToString()
    //  => $"<{GetType().Name}: {m_lo}, {m_hi}>";
    //#endregion Object overrides
  }
}
