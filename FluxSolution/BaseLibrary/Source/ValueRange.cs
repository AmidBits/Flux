namespace Flux
{
  public static partial class Em
  {
    public static (TSelf Minimum, TSelf Maximum) GetExtremumUsingConstraint<TSelf>(this ValueRange<TSelf> source, IntervalNotation constraint)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      return constraint switch
      {
        IntervalNotation.Closed => (source.Min, source.Max),
        IntervalNotation.Open => (source.Min + TSelf.One, source.Max - TSelf.One),
        IntervalNotation.HalfOpenLeft => (source.Min + TSelf.One, source.Max),
        IntervalNotation.HalfOpenRight => (source.Min, source.Max - TSelf.One),
        _ => throw new NotImplementedException(),
      };
    }

    /// <summary>
    /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="ValueRange{TSelf}"/> using the <paramref name="constraint"/> .</para>
    /// </summary>
    /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
    /// <param name="count">The number of iterations to perform.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> IterateRange<TSelf>(this ValueRange<TSelf> source, TSelf step, SortOrder order = SortOrder.Ascending, IntervalNotation constraint = IntervalNotation.Closed, int count = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var (min, max) = source.GetExtremumUsingConstraint(constraint);

      switch (order)
      {
        case SortOrder.Ascending:
          while (count-- > 0)
            for (var i = min; i <= max; i += step)
              yield return i;
          break;
        case SortOrder.Descending:
          while (count-- > 0)
            for (var i = max; i >= min; i -= step)
              yield return i;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(order));
      }
    }

    /// <summary>Creates a sequence of <paramref name="count"/> numbers alternating around <paramref name="mean"/>, controlled by a <paramref name="direction"/> (towards or away from <paramref name="mean"/>) and <paramref name="step"/> size.</summary>
    /// <param name="mean">The mean around which the iteration takes place.</param>
    /// <param name="count">The number of iterations to execute.</param>
    /// <param name="step">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="direction">Specified by <see cref="AlternatingLoopDirection"/>.</param>
    public static System.Collections.Generic.IEnumerable<TSelf> IterateAlternating<TSelf>(this ValueRange<TSelf> source, TSelf step, SortOrder initialOrder, IntervalNotation constraint = IntervalNotation.Closed)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var a = source.IterateRange(step, SortOrder.Ascending, constraint).ToList();
      var d = source.IterateRange(step, SortOrder.Descending, constraint).ToList();

      var list = new System.Collections.Generic.List<TSelf>();

      switch (initialOrder)
      {
        case SortOrder.Ascending:
          if (a.Last() == d.First()) d.RemoveAt(0);
          if (a.First() == d.Last()) d.RemoveAt(d.Count - 1);
          list.AddRange(a);
          list.AddRange(d);
          break;
        case SortOrder.Descending:
          if (d.Last() == a.First()) a.RemoveAt(0);
          if (d.First() == a.Last()) a.RemoveAt(a.Count - 1);
          list.AddRange(d);
          list.AddRange(a);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(initialOrder));
      }

      while (true)
        for (var i = 0; i < list.Count; i++)
          yield return list[i];
    }

    /// <summary>Folds an out-of-bound <paramref name="value"/> (back and forth) over the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the <paramref name="value"/> is within the closed interval.</summary>
    public static TSelf Fold<TSelf>(this ValueRange<TSelf> source, TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => (value > source.Max)
      ? TSelf.IsEvenInteger(Maths.TruncMod(value - source.Max, source.Max - source.Min, out var remHi)) ? source.Max - remHi : source.Min + remHi
      : (value < source.Min)
      ? TSelf.IsEvenInteger(Maths.TruncMod(source.Min - value, source.Max - source.Min, out var remLo)) ? source.Min + remLo : source.Max - remLo
      : value;

    /// <summary>Proportionally rescale the <paramref name="value"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The <paramref name="value"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this ValueRange<TSelf> source, ValueRange<TSelf> target, TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => target.Min + (target.Max - target.Min) * (value - source.Min) / (source.Max - source.Min);

    /// <summary>Wraps an out-of-bound <paramref name="value"/> around the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the <paramref name="value"/> is within the closed interval.</summary>
    public static TSelf Wrap<TSelf>(this ValueRange<TSelf> source, TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value < source.Min
      ? source.Max - (source.Min - value) % (source.Max - source.Min)
      : value > source.Max
      ? source.Min + (value - source.Min) % (source.Max - source.Min)
      : value;
  }

  /// <summary>
  /// <para>Represents a value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public readonly record struct ValueRange<TSelf>
    : System.IComparable<ValueRange<TSelf>>
    where TSelf : System.IComparable<TSelf>
  {
    /// <remarks>The default set is using the default(<typeparamref name="TSelf"/>) and so representing a degenerate set with a value of zero for any standard numerical types (int, long, BigInteger, etc.).</remarks>
    public readonly static ValueRange<TSelf> Default;

    private readonly TSelf m_max;
    private readonly TSelf m_min;

    public ValueRange(TSelf minimum, TSelf maximum)
    {
      if (minimum.CompareTo(maximum) >= 0 || maximum.CompareTo(minimum) <= 0) throw new System.ArgumentException($"A {nameof(ValueRange<TSelf>)} must have the property: minimum ({minimum}) <= maximum ({maximum}) and vice versa.");

      m_min = minimum;
      m_max = maximum;
    }

    public TSelf Max => m_max;
    public TSelf Min => m_min;

    /// <summary>Returns whether the set is the degenerate interval with the default value of <typeparamref name="TSelf"/>.</summary>
    public bool IsDefault => Equals(Default);

    /// <summary>Returns whether the set is a degenerate interval.</summary>
    /// <remarks>The default set is included in this definition.</remarks>
    public bool IsDegenerateInterval => m_min.CompareTo(m_max) == 0 && m_max.CompareTo(m_min) == 0;

    /// <summary>Asserts that the value is a member of the interval set (throws an exception if it's not).</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public TSelf AssertMember(TSelf value, IntervalNotation constraint, string? paramName = null)
      => constraint.AssertMember(value, m_min, m_max, paramName);

    /// <summary>Returns whether the value is a member of the interval set.</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool VerifyMember(TSelf value, IntervalNotation constraint)
      => constraint.VerifyMember(value, m_min, m_max);

    #region Static methods

    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    public static System.Collections.Generic.IEnumerable<ValueRange<TSelf>> Intersect(ValueRange<TSelf> a, ValueRange<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<TSelf>>();

      if (IsOverlapping(a, b)) list.Add(new ValueRange<TSelf>(LargerMinimum(a, b), SmallerMaximum(a, b)));

      return list;
    }

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    public static System.Collections.Generic.IEnumerable<ValueRange<TSelf>> LeftDifference(ValueRange<TSelf> a, ValueRange<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<TSelf>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new ValueRange<TSelf>(a.m_min, b.m_min));
        if (a.m_max.CompareTo(b.m_max) > 0) list.Add(new ValueRange<TSelf>(b.m_max, a.m_max));
      }
      else list.Add(a);

      return list;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    public static System.Collections.Generic.IList<ValueRange<TSelf>> RightDifference(ValueRange<TSelf> a, ValueRange<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<TSelf>>();

      if (IsOverlapping(a, b))
      {
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new ValueRange<TSelf>(b.m_min, a.m_min));
        if (b.m_max.CompareTo(a.m_max) > 0) list.Add(new ValueRange<TSelf>(a.m_max, b.m_max));
      }
      else list.Add(b);

      return list;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    public static System.Collections.Generic.IList<ValueRange<TSelf>> SymmetricDifference(ValueRange<TSelf> a, ValueRange<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<TSelf>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new ValueRange<TSelf>(a.m_min, b.m_min));
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new ValueRange<TSelf>(b.m_min, a.m_min));
        if (a.m_max.CompareTo(b.m_max) < 0) list.Add(new ValueRange<TSelf>(a.m_max, b.m_max));
        if (b.m_max.CompareTo(a.m_max) < 0) list.Add(new ValueRange<TSelf>(b.m_max, a.m_max));
      }
      else
      {
        list.Add(a);
        list.Add(b);
      }

      return list;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    public static System.Collections.Generic.IList<ValueRange<TSelf>> Union(ValueRange<TSelf> a, ValueRange<TSelf> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<TSelf>>();

      if (!IsOverlapping(a, b))
      {
        list.Add(a);
        list.Add(b);
      }
      else list.Add(new ValueRange<TSelf>(SmallerMinimum(a, b), LargerMaximum(a, b)));

      return list;
    }

    /// <summary>Returns whether a and b are overlapping.</summary>
    public static bool IsOverlapping(ValueRange<TSelf> a, ValueRange<TSelf> b) => a.m_min.CompareTo(b.m_max) < 0 && b.m_min.CompareTo(a.m_max) < 0;

    /// <summary>Returns the maximum high value of a and b.</summary>
    public static TSelf LargerMaximum(ValueRange<TSelf> a, ValueRange<TSelf> b) => a.m_max.CompareTo(b.m_max) >= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the maximum low value of a and b.</summary>
    public static TSelf LargerMinimum(ValueRange<TSelf> a, ValueRange<TSelf> b) => a.m_min.CompareTo(b.m_min) >= 0 ? a.m_min : b.m_min;

    /// <summary>Returns the minimum high value of a and b.</summary>
    public static TSelf SmallerMaximum(ValueRange<TSelf> a, ValueRange<TSelf> b) => a.m_max.CompareTo(b.m_max) <= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the minimum low value of a and b.</summary>
    public static TSelf SmallerMinimum(ValueRange<TSelf> a, ValueRange<TSelf> b) => a.m_min.CompareTo(b.m_min) <= 0 ? a.m_min : b.m_min;

    #endregion Static methods

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(ValueRange<TSelf> other)
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
