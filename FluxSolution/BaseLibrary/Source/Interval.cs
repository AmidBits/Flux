namespace Flux
{
  public static partial class Em
  {
    public static Interval<TSelf> CreateInterval<TSelf>(this IntervalNotation notation, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (Interval<TSelf>)notation.GetInfimumAndSupremum(min, max);

    public static System.Collections.Generic.IEnumerable<TSelf> IterateRange<TSelf>(this Interval<TSelf> source, TSelf stepSize, IntervalNotation notation = IntervalNotation.Closed)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var (min, max) = notation.GetInfimumAndSupremum(source.Min, source.Max);

      for (var n = min; n <= max; n += stepSize)
        yield return n;
    }

    public static System.Collections.Generic.IEnumerable<TSelf> IterateInterval<TSelf>(this Interval<TSelf> source, TSelf stepSize, IntervalNotation notation = IntervalNotation.Closed)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var counter = TSelf.Zero;

      var value = source.Min + counter * stepSize;

      while (value <= source.Max)
      {
        yield return value;

        counter++;

        value = source.Min + counter * stepSize;
      }
    }

    /// <summary>Creates a sequence of numbers alternating back and forth over the <see cref="Interval{TSelf}"/>, controlled by a <paramref name="stepSize"/>, <paramref name="initialOrder"/>, <paramref name="notation"/> and loop <paramref name="count"/>.</summary>
    /// <param name="source">The <see cref="Interval{TSelf}"/>.</param>
    /// <param name="stepSize">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="initialOrder">Starting in the specified <see cref="SortOrder"/>.</param>
    /// <param name="notation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <param name="count">The number of iterations to execute.</param>
    public static System.Collections.Generic.IEnumerable<TSelf> IterateBackAndForth<TSelf>(this Interval<TSelf> source, TSelf stepSize, SortOrder initialOrder, IntervalNotation notation = IntervalNotation.Closed, int count = 1)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (stepSize <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(stepSize));
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var a = source.IterateRange(stepSize, SortOrder.Ascending, notation);
      var d = source.IterateRange(stepSize, SortOrder.Descending, notation);

      var list = new System.Collections.Generic.List<TSelf>();

      switch (initialOrder)
      {
        case SortOrder.Ascending:
          list.AddRange(a);
          list.AddRange(d);
          break;
        case SortOrder.Descending:
          list.AddRange(d);
          list.AddRange(a);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(initialOrder));
      }

      if (list.First() == list.Last()) list.RemoveAt(list.Count - 1);
      if (list.Count / 2 is var mindex && list[mindex] == list[mindex + 1]) list.RemoveAt(mindex + 1);

      while (count-- > 0)
        for (var i = 0; i < list.Count; i++)
          yield return list[i];
    }

    /// <summary>
    /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="Interval{TSelf}"/> using the <paramref name="constraint"/> .</para>
    /// </summary>
    /// <param name="source">The <see cref="Interval{TSelf}"/>.</param>
    /// <param name="stepSize">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
    /// <param name="notation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <param name="count">The number of iterations to perform.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> IterateRange<TSelf>(this Interval<TSelf> source, TSelf stepSize, SortOrder order = SortOrder.Ascending, IntervalNotation notation = IntervalNotation.Closed, int count = 1)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (stepSize <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(stepSize));
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var (min, max) = notation.GetInfimumAndSupremum(source.Min, source.Max);

      switch (order)
      {
        case SortOrder.Ascending:
          while (count-- > 0)
            for (var i = min; i <= max; i += stepSize)
              yield return i;
          break;
        case SortOrder.Descending:
          while (count-- > 0)
            for (var i = max; i >= min; i -= stepSize)
              yield return i;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(order));
      }
    }
  }

  /// <summary>
  /// <para>Represents a closed interval or value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public readonly record struct Interval<T>
    : System.IComparable, System.IComparable<Interval<T>>, System.IFormattable
    where T : System.IComparable<T>, System.IFormattable
  {
    /// <remarks>The default interval is representing a degenerate interval with the default value of <typeparamref name="T"/>, i.e. zero for standard numerical types, e.g. int, double, BigInteger, etc..</remarks>
    public readonly static Interval<T> Default;

    private readonly T m_max;
    private readonly T m_min;

    public Interval(T minValue, T maxValue)
    {
      IntervalNotation.Closed.AssertValidInterval(minValue, maxValue);

      m_min = minValue;
      m_max = maxValue;
    }

    public T Max => m_max;
    public T Min => m_min;

    /// <summary>
    /// <para>Asserts that the value is a member of the interval set (throws an exception if it's not).</para>
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public T AssertMember(T value, IntervalNotation notation = IntervalNotation.Closed, string? paramName = null) => notation.AssertMember(value, m_min, m_max, paramName);

    /// <summary>
    /// <para>Returns whether the value is a member of the interval set.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    /// <exception cref="System.NotImplementedException"/>
    public bool VerifyMember(T value, IntervalNotation notation = IntervalNotation.Closed) => notation.VerifyMember(value, m_min, m_max);

    #region Static methods

    /// <summary>
    /// <para>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</para>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<Interval<T>> Intersect(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b)) list.Add(new Interval<T>(LargerMinimum(a, b), SmallerMaximum(a, b)));

      return list;
    }

    /// <summary>
    /// <para>Returns whether a and b are overlapping.</para>
    /// </summary>
    public static bool IsOverlapping(Interval<T> a, Interval<T> b)
      => a.m_min.CompareTo(b.m_max) < 0 && b.m_min.CompareTo(a.m_max) < 0;

    /// <summary>
    /// <para>Returns the maximum high value of a and b.</para>
    /// </summary>
    public static T LargerMaximum(Interval<T> a, Interval<T> b)
      => a.m_max.CompareTo(b.m_max) >= 0 ? a.m_max : b.m_max;

    /// <summary>
    /// <para>Returns the maximum low value of a and b.</para>
    /// </summary>
    public static T LargerMinimum(Interval<T> a, Interval<T> b)
      => a.m_min.CompareTo(b.m_min) >= 0 ? a.m_min : b.m_min;

    /// <summary>
    /// <para>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</para>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<Interval<T>> LeftDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new Interval<T>(a.m_min, b.m_min));
        if (a.m_max.CompareTo(b.m_max) > 0) list.Add(new Interval<T>(b.m_max, a.m_max));
      }
      else list.Add(a);

      return list;
    }

    /// <summary>
    /// <para>Right different is the set of all elements that are members of B, but not members of A.</para>
    /// </summary>
    public static System.Collections.Generic.IList<Interval<T>> RightDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new Interval<T>(b.m_min, a.m_min));
        if (b.m_max.CompareTo(a.m_max) > 0) list.Add(new Interval<T>(a.m_max, b.m_max));
      }
      else list.Add(b);

      return list;
    }

    /// <summary>
    /// <para>Returns the minimum high value of a and b.</para>
    /// </summary>
    public static T SmallerMaximum(Interval<T> a, Interval<T> b)
      => a.m_max.CompareTo(b.m_max) <= 0 ? a.m_max : b.m_max;

    /// <summary>
    /// <para>Returns the minimum low value of a and b.</para>
    /// </summary>
    public static T SmallerMinimum(Interval<T> a, Interval<T> b)
      => a.m_min.CompareTo(b.m_min) <= 0 ? a.m_min : b.m_min;

    /// <summary>
    /// <para>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</para>
    /// </summary>
    public static System.Collections.Generic.IList<Interval<T>> SymmetricDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new Interval<T>(a.m_min, b.m_min));
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new Interval<T>(b.m_min, a.m_min));
        if (a.m_max.CompareTo(b.m_max) < 0) list.Add(new Interval<T>(a.m_max, b.m_max));
        if (b.m_max.CompareTo(a.m_max) < 0) list.Add(new Interval<T>(b.m_max, a.m_max));
      }
      else
      {
        list.Add(a);
        list.Add(b);
      }

      return list;
    }

    /// <summary>
    /// <para>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</para>
    /// </summary>
    public static System.Collections.Generic.IList<Interval<T>> Union(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (!IsOverlapping(a, b))
      {
        list.Add(a);
        list.Add(b);
      }
      else list.Add(new Interval<T>(SmallerMinimum(a, b), LargerMaximum(a, b)));

      return list;
    }

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator Interval<T>(System.ValueTuple<T, T> vt) => new(vt.Item1, vt.Item2);
    public static explicit operator System.ValueTuple<T, T>(Interval<T> vr) => (vr.m_min, vr.m_max);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? obj)
      => obj is Interval<T> o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Interval<T> other)
      => (m_min.CompareTo(other.m_min) is var cmpmin && cmpmin != 0) ? cmpmin : (m_max.CompareTo(other.m_max) is var cmpmax && cmpmax != 0) ? cmpmax : 0;

    // IFormattable
    public string ToString(string? format, IFormatProvider? formatProvider)
      => $"<{m_min.ToString(format, formatProvider)}, {m_max.ToString(format, formatProvider)}>";

    #endregion Implemented interfaces
  }
}
