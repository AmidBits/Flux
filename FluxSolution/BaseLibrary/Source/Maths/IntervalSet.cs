namespace Flux
{
  /// <summary>Represents a value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</summary>
  public readonly record struct IntervalSet<TValue>
    : System.IComparable<IntervalSet<TValue>>
    where TValue : System.IComparable<TValue>
  {
    public readonly static IntervalSet<TValue> Default;

    private readonly TValue m_max;
    private readonly TValue m_min;

    public IntervalSet(TValue minimum, TValue maximum)
    {
      if (minimum.CompareTo(maximum) >= 0 || maximum.CompareTo(minimum) <= 0) throw new System.ArgumentException($"A {nameof(IntervalSet<TValue>)} must have the property: minimum ({minimum}) <= maximum ({maximum}) and vice versa.");

      m_min = minimum;
      m_max = maximum;
    }

    public TValue Max => m_max;
    public TValue Min => m_min;

    /// <summary>Returns whether the set is the degenerate interval with the default value of <typeparamref name="TValue"/>.</summary>
    public bool IsDefault => Equals(Default);

    /// <summary>Returns whether the set is a degenerate interval.</summary>
    public bool IsDegenerateInterval => m_min.CompareTo(m_max) == 0 && m_max.CompareTo(m_min) == 0;

    /// <summary>Returns whether this is a proper interval, i.e. neither empty nor degenerate.</summary>
    public bool IsProperInterval => m_min.CompareTo(m_max) < 0 && m_max.CompareTo(m_min) > 0;

    /// <summary>Asserts that the value is a member of the interval set (throws an exception if it's not).</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public TValue AssertMember(TValue value, IntervalConstraint constraint, string? paramName = null)
      => constraint switch
      {
        IntervalConstraint.Closed => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than-or-equal-to {m_min} and less-than-or-equal-to {m_max}."),
        IntervalConstraint.Open => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than {m_min} and less-than {m_max}."),
        IntervalConstraint.HalfOpenLeft => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than {m_min} and less-than-or-equal-to {m_max}."),
        IntervalConstraint.HalfOpenRight => IsMember(value, constraint) ? value : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Must be a value greater-than-or-equal-to {m_min} and less-than {m_max}."),
        _ => throw new NotImplementedException(),
      };

    /// <summary>Returns whether the value is a member of the interval set.</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool IsMember(TValue value, IntervalConstraint constraint)
      => constraint switch
      {
        IntervalConstraint.Closed => value.CompareTo(m_min) >= 0 && value.CompareTo(m_max) <= 0,
        IntervalConstraint.Open => value.CompareTo(m_min) > 0 && value.CompareTo(m_max) < 0,
        IntervalConstraint.HalfOpenLeft => value.CompareTo(m_min) >= 0 && value.CompareTo(m_max) < 0,
        IntervalConstraint.HalfOpenRight => value.CompareTo(m_min) > 0 && value.CompareTo(m_max) <= 0,
        _ => throw new NotImplementedException(),
      };

    #region Static methods

    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    public static System.Collections.Generic.IEnumerable<IntervalSet<TValue>> Intersect(IntervalSet<TValue> a, IntervalSet<TValue> b)
    {
      var list = new System.Collections.Generic.List<IntervalSet<TValue>>();

      if (IsOverlapping(a, b)) list.Add(new IntervalSet<TValue>(LargerMinimum(a, b), SmallerMaximum(a, b)));

      return list;
    }

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    public static System.Collections.Generic.IEnumerable<IntervalSet<TValue>> LeftDifference(IntervalSet<TValue> a, IntervalSet<TValue> b)
    {
      var list = new System.Collections.Generic.List<IntervalSet<TValue>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new IntervalSet<TValue>(a.m_min, b.m_min));
        if (a.m_max.CompareTo(b.m_max) > 0) list.Add(new IntervalSet<TValue>(b.m_max, a.m_max));
      }
      else list.Add(a);

      return list;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    public static System.Collections.Generic.IList<IntervalSet<TValue>> RightDifference(IntervalSet<TValue> a, IntervalSet<TValue> b)
    {
      var list = new System.Collections.Generic.List<IntervalSet<TValue>>();

      if (IsOverlapping(a, b))
      {
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new IntervalSet<TValue>(b.m_min, a.m_min));
        if (b.m_max.CompareTo(a.m_max) > 0) list.Add(new IntervalSet<TValue>(a.m_max, b.m_max));
      }
      else list.Add(b);

      return list;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    public static System.Collections.Generic.IList<IntervalSet<TValue>> SymmetricDifference(IntervalSet<TValue> a, IntervalSet<TValue> b)
    {
      var list = new System.Collections.Generic.List<IntervalSet<TValue>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new IntervalSet<TValue>(a.m_min, b.m_min));
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new IntervalSet<TValue>(b.m_min, a.m_min));
        if (a.m_max.CompareTo(b.m_max) < 0) list.Add(new IntervalSet<TValue>(a.m_max, b.m_max));
        if (b.m_max.CompareTo(a.m_max) < 0) list.Add(new IntervalSet<TValue>(b.m_max, a.m_max));
      }
      else
      {
        list.Add(a);
        list.Add(b);
      }

      return list;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    public static System.Collections.Generic.IList<IntervalSet<TValue>> Union(IntervalSet<TValue> a, IntervalSet<TValue> b)
    {
      var list = new System.Collections.Generic.List<IntervalSet<TValue>>();

      if (!IsOverlapping(a, b))
      {
        list.Add(a);
        list.Add(b);
      }
      else list.Add(new IntervalSet<TValue>(SmallerMinimum(a, b), LargerMaximum(a, b)));

      return list;
    }

    /// <summary>Returns whether a and b are overlapping.</summary>
    public static bool IsOverlapping(IntervalSet<TValue> a, IntervalSet<TValue> b) => a.m_min.CompareTo(b.m_max) < 0 && b.m_min.CompareTo(a.m_max) < 0;

    /// <summary>Returns the maximum high value of a and b.</summary>
    public static TValue LargerMaximum(IntervalSet<TValue> a, IntervalSet<TValue> b) => a.m_max.CompareTo(b.m_max) >= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the maximum low value of a and b.</summary>
    public static TValue LargerMinimum(IntervalSet<TValue> a, IntervalSet<TValue> b) => a.m_min.CompareTo(b.m_min) >= 0 ? a.m_min : b.m_min;

    /// <summary>Returns the minimum high value of a and b.</summary>
    public static TValue SmallerMaximum(IntervalSet<TValue> a, IntervalSet<TValue> b) => a.m_max.CompareTo(b.m_max) <= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the minimum low value of a and b.</summary>
    public static TValue SmallerMinimum(IntervalSet<TValue> a, IntervalSet<TValue> b) => a.m_min.CompareTo(b.m_min) <= 0 ? a.m_min : b.m_min;

    #endregion Static methods

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(IntervalSet<TValue> other)
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
