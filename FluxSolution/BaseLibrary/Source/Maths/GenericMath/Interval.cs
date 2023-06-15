namespace Flux
{
  /// <summary>
  /// <para>Represents an interval, for various range operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable and IEquatable to operate.</para>
  /// </summary>
  public readonly record struct Interval<T>
    : System.IComparable<Interval<T>>
    where T : System.IComparable<T>, System.IEquatable<T>
  {
    public readonly static Interval<T> Empty;

    private readonly T m_max;
    private readonly T m_min;

    public Interval(T minimum, T maximum)
    {
      if (minimum.CompareTo(maximum) > 0 || maximum.CompareTo(minimum) < 0) throw new System.ArgumentException($"An interval must have the property: minimum ({minimum}) < maximum ({maximum}) and vice versa.");

      m_min = minimum;
      m_max = maximum;
    }

    public T Max => m_max;
    public T Min => m_min;

    public bool IsDegenerate => !IsEmpty && m_min.CompareTo(m_max) == 0 && m_max.CompareTo(m_min) == 0;

    /// <summary>Returns whether the value range is empty.</summary>
    public bool IsEmpty => Equals(Empty);

    /// <summary>Returns whether this is a proper value range, i.e. the range as some extent.</summary>
    public bool IsProper => !IsDegenerate && !IsEmpty;

    //public T Distance => m_max - m_min;

    #region Static methods

    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>

    public static Interval<T> Intersect(Interval<T> a, Interval<T> b)
      => IsOverlapping(a, b) ? new Interval<T>(LargerMinimum(a, b), SmallerMaximum(a, b)) : Empty;

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>

    public static System.Collections.Generic.IEnumerable<Interval<T>> LeftDifference(Interval<T> left, Interval<T> right)
    {
      if (IsOverlapping(left, right))
      {
        if (left.m_min.CompareTo(right.m_min) < 0)
          yield return new Interval<T>(left.m_min, right.m_min);
        if (right.m_max.CompareTo(left.m_max) < 0)
          yield return new Interval<T>(right.m_max, left.m_max);
      }
      else yield return left;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>

    public static System.Collections.Generic.IEnumerable<Interval<T>> RightDifference(Interval<T> left, Interval<T> right)
    {
      if (IsOverlapping(left, right))
      {
        if (right.m_min.CompareTo(left.m_min) < 0)
          yield return new Interval<T>(right.m_min, left.m_min);
        if (left.m_max.CompareTo(right.m_max) < 0)
          yield return new Interval<T>(left.m_max, right.m_max);
      }
      else yield return right;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>

    public static System.Collections.Generic.IEnumerable<Interval<T>> SymmetricDifference(Interval<T> a, Interval<T> b)
    {
      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0)
          yield return new Interval<T>(a.m_min, b.m_min);
        if (b.m_max.CompareTo(a.m_max) < 0)
          yield return new Interval<T>(b.m_max, a.m_max);
        if (a.m_max.CompareTo(b.m_max) < 0)
          yield return new Interval<T>(a.m_max, b.m_max);
      }
      else yield return a;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>

    public static Interval<T> Union(Interval<T> a, Interval<T> b)
      => IsOverlapping(a, b) ? new Interval<T>(SmallerMinimum(a, b), LargerMaximum(a, b)) : Empty;

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A and B, unconditionally.</summary>

    public static Interval<T> UnionAll(Interval<T> a, Interval<T> b)
      => new(SmallerMinimum(a, b), LargerMaximum(a, b));

    /// <summary>Returns whether a and b are overlapping.</summary>

    public static bool IsOverlapping(Interval<T> a, Interval<T> b)
      => a.m_min.CompareTo(b.m_max) < 0 && b.m_min.CompareTo(a.m_max) < 0;

    /// <summary>Returns the maximum high value of a and b.</summary>

    public static T LargerMaximum(Interval<T> a, Interval<T> b)
      => a.m_max.CompareTo(b.m_max) >= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the maximum low value of a and b.</summary>

    public static T LargerMinimum(Interval<T> a, Interval<T> b)
      => a.m_min.CompareTo(b.m_min) >= 0 ? a.m_min : b.m_min;

    /// <summary>Returns the minimum high value of a and b.</summary>

    public static T SmallerMaximum(Interval<T> a, Interval<T> b)
      => a.m_max.CompareTo(b.m_max) <= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the minimum low value of a and b.</summary>

    public static T SmallerMinimum(Interval<T> a, Interval<T> b)
      => a.m_min.CompareTo(b.m_min) <= 0 ? a.m_min : b.m_min;

    #endregion Static methods

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Interval<T> other)
    {
      if (m_min.CompareTo(other.m_min) is var cmpmin && cmpmin != 0)
        return cmpmin;

      if (m_max.CompareTo(other.m_max) is var cmpmax && cmpmax != 0)
        return cmpmax;

      return 0;
    }

    #endregion Implemented interfaces
  }
}
