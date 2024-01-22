namespace Flux
{
  public static partial class Em
  {
    public static ValueRange<TSelf> Create<TSelf>(this IntervalNotation notation, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (ValueRange<TSelf>)notation.GetExtremum(min, max);

    /// <summary>Creates a sequence of numbers alternating back and forth over the <see cref="ValueRange{TSelf}"/>, controlled by a <paramref name="stepSize"/>, <paramref name="initialOrder"/>, <paramref name="notation"/> and loop <paramref name="count"/>.</summary>
    /// <param name="source">The <see cref="ValueRange{TSelf}"/>.</param>
    /// <param name="stepSize">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="initialOrder">Starting in the specified <see cref="SortOrder"/>.</param>
    /// <param name="notation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <param name="count">The number of iterations to execute.</param>
    public static System.Collections.Generic.IEnumerable<TSelf> IterateBackAndForth<TSelf>(this ValueRange<TSelf> source, TSelf stepSize, SortOrder initialOrder, IntervalNotation notation = IntervalNotation.Closed, int count = 1)
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
    /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="ValueRange{TSelf}"/> using the <paramref name="constraint"/> .</para>
    /// </summary>
    /// <param name="source">The <see cref="ValueRange{TSelf}"/>.</param>
    /// <param name="stepSize">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
    /// <param name="notation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <param name="count">The number of iterations to perform.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> IterateRange<TSelf>(this ValueRange<TSelf> source, TSelf stepSize, SortOrder order = SortOrder.Ascending, IntervalNotation notation = IntervalNotation.Closed, int count = 1)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (stepSize <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(stepSize));
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var (min, max) = notation.GetExtremum(source.Min, source.Max);

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
  /// <para>Represents a value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public readonly record struct ValueRange<T>
    : System.IComparable<ValueRange<T>>
    where T : System.IComparable<T>
  {
    /// <remarks>The default set is using the default(<typeparamref name="T"/>) and so representing a degenerate set with a value of zero for any standard numerical types (int, long, BigInteger, etc.).</remarks>
    public readonly static ValueRange<T> Default;

    private readonly T m_max;
    private readonly T m_min;

    public ValueRange(T minValue, T maxValue)
    {
      if (minValue.CompareTo(maxValue) >= 0 || maxValue.CompareTo(minValue) <= 0) throw new System.ArgumentException($"A {nameof(ValueRange<T>)} must have the property: minimum ({minValue}) <= maximum ({maxValue}) and vice versa.");

      m_min = minValue;
      m_max = maxValue;
    }

    public T Max => m_max;
    public T Min => m_min;

    /// <summary>Returns whether the set is the degenerate interval with the default value of <typeparamref name="T"/>.</summary>
    public bool IsDefault => Equals(Default);

    /// <summary>Returns whether the set is a degenerate interval.</summary>
    /// <remarks>The default set is included in this definition.</remarks>
    public bool IsDegenerateInterval => m_min.CompareTo(m_max) == 0 && m_max.CompareTo(m_min) == 0;

    /// <summary>Asserts that the value is a member of the interval set (throws an exception if it's not).</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public T AssertMember(T value, IntervalNotation constraint, string? paramName = null) => constraint.AssertMember(value, m_min, m_max, paramName);

    /// <summary>Returns whether the value is a member of the interval set.</summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool VerifyMember(T value, IntervalNotation constraint) => constraint.VerifyMember(value, m_min, m_max);

    #region Static methods

    /// <summary>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</summary>
    public static System.Collections.Generic.IEnumerable<ValueRange<T>> Intersect(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (IsOverlapping(a, b)) list.Add(new ValueRange<T>(LargerMinimum(a, b), SmallerMaximum(a, b)));

      return list;
    }

    /// <summary>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</summary>
    public static System.Collections.Generic.IEnumerable<ValueRange<T>> LeftDifference(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new ValueRange<T>(a.m_min, b.m_min));
        if (a.m_max.CompareTo(b.m_max) > 0) list.Add(new ValueRange<T>(b.m_max, a.m_max));
      }
      else list.Add(a);

      return list;
    }

    /// <summary>Right different is the set of all elements that are members of B, but not members of A.</summary>
    public static System.Collections.Generic.IList<ValueRange<T>> RightDifference(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (IsOverlapping(a, b))
      {
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new ValueRange<T>(b.m_min, a.m_min));
        if (b.m_max.CompareTo(a.m_max) > 0) list.Add(new ValueRange<T>(a.m_max, b.m_max));
      }
      else list.Add(b);

      return list;
    }

    /// <summary>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</summary>
    public static System.Collections.Generic.IList<ValueRange<T>> SymmetricDifference(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_min.CompareTo(b.m_min) < 0) list.Add(new ValueRange<T>(a.m_min, b.m_min));
        if (b.m_min.CompareTo(a.m_min) < 0) list.Add(new ValueRange<T>(b.m_min, a.m_min));
        if (a.m_max.CompareTo(b.m_max) < 0) list.Add(new ValueRange<T>(a.m_max, b.m_max));
        if (b.m_max.CompareTo(a.m_max) < 0) list.Add(new ValueRange<T>(b.m_max, a.m_max));
      }
      else
      {
        list.Add(a);
        list.Add(b);
      }

      return list;
    }

    /// <summary>The union of A and B, denoted by A ∪ B, is the set of all things that are members of A or of B or of both.</summary>
    public static System.Collections.Generic.IList<ValueRange<T>> Union(ValueRange<T> a, ValueRange<T> b)
    {
      var list = new System.Collections.Generic.List<ValueRange<T>>();

      if (!IsOverlapping(a, b))
      {
        list.Add(a);
        list.Add(b);
      }
      else list.Add(new ValueRange<T>(SmallerMinimum(a, b), LargerMaximum(a, b)));

      return list;
    }

    /// <summary>Returns whether a and b are overlapping.</summary>
    public static bool IsOverlapping(ValueRange<T> a, ValueRange<T> b) => a.m_min.CompareTo(b.m_max) < 0 && b.m_min.CompareTo(a.m_max) < 0;

    /// <summary>Returns the maximum high value of a and b.</summary>
    public static T LargerMaximum(ValueRange<T> a, ValueRange<T> b) => a.m_max.CompareTo(b.m_max) >= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the maximum low value of a and b.</summary>
    public static T LargerMinimum(ValueRange<T> a, ValueRange<T> b) => a.m_min.CompareTo(b.m_min) >= 0 ? a.m_min : b.m_min;

    /// <summary>Returns the minimum high value of a and b.</summary>
    public static T SmallerMaximum(ValueRange<T> a, ValueRange<T> b) => a.m_max.CompareTo(b.m_max) <= 0 ? a.m_max : b.m_max;

    /// <summary>Returns the minimum low value of a and b.</summary>
    public static T SmallerMinimum(ValueRange<T> a, ValueRange<T> b) => a.m_min.CompareTo(b.m_min) <= 0 ? a.m_min : b.m_min;

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator ValueRange<T>(System.ValueTuple<T, T> vt) => new(vt.Item1, vt.Item2);
    public static explicit operator System.ValueTuple<T, T>(ValueRange<T> vr) => (vr.m_min, vr.m_max);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(ValueRange<T> other)
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
