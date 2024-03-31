namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Creates a new <see cref="Flux.Interval{T}"/> by padding the <paramref name="source"/> interval notation <paramref name="minValue"/>..<paramref name="maxValue"/> with its (potentionally inner) extent (i.e. infimum and supremum).</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static Interval<TSelf> CreateIntervalByExtent<TSelf>(this IntervalNotation source, TSelf minValue, TSelf maxValue)
      where TSelf : System.Numerics.INumber<TSelf>
      => (Interval<TSelf>)source.GetExtentInterval(minValue, maxValue);

    /// <summary>
    /// <para>Creates a new <see cref="Flux.Interval{T}"/> by padding the <paramref name="source"/> interval notation <paramref name="minValue"/>..<paramref name="maxValue"/> with <paramref name="marginSize"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="marginSize"></param>
    /// <returns></returns>
    public static Interval<TSelf> CreateIntervalByMargin<TSelf>(this IntervalNotation source, TSelf minValue, TSelf maxValue, TSelf marginSize)
      where TSelf : System.Numerics.INumber<TSelf>
      => (Interval<TSelf>)source.GetMarginInterval(minValue, maxValue, marginSize);

    /// <summary>
    /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="Interval{TSelf}"/> using the <paramref name="constraint"/> .</para>
    /// </summary>
    /// <param name="source">The <see cref="Interval{TSelf}"/>.</param>
    /// <param name="step">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
    /// <param name="notation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> LoopInterval<TSelf>(this Interval<TSelf> source, TSelf step, SortOrder order, IntervalNotation notation = IntervalNotation.Closed)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (step <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(step));

      var (minMargin, maxMargin) = notation.GetMarginInterval(source.MinValue, source.MaxValue, TSelf.Abs(step));

      var count = System.Numerics.BigInteger.One;

      switch (order)
      {
        case SortOrder.Ascending:
          for (var number = minMargin; number <= maxMargin; number = minMargin + step * TSelf.CreateChecked(count), count++)
            yield return number;
          break;
        case SortOrder.Descending:
          for (var number = maxMargin; number >= minMargin; number = maxMargin + step * TSelf.CreateChecked(count), count++)
            yield return number;
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
    where T : System.IComparable<T>
  {
    /// <remarks>The default interval is representing a degenerate interval with the default value of <typeparamref name="T"/>, i.e. zero for standard numerical types, e.g. int, double, BigInteger, etc..</remarks>
    public readonly static Interval<T> Default;

    private readonly T m_maxValue;
    private readonly T m_minValue;

    public Interval(T minValue, T maxValue)
    {
      IntervalNotation.Closed.AssertValidInterval(minValue, maxValue);

      m_minValue = minValue;
      m_maxValue = maxValue;
    }

    public T MaxValue => m_maxValue;
    public T MinValue => m_minValue;

    /// <summary>
    /// <para>Asserts that the value is a member of the interval set (throws an exception if it's not).</para>
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public T AssertMember(T value, IntervalNotation notation = IntervalNotation.Closed, string? paramName = null) => notation.AssertMember(value, m_minValue, m_maxValue, paramName);

    /// <summary>
    /// <para>Returns whether the value is a member of the interval set.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    /// <exception cref="System.NotImplementedException"/>
    public bool VerifyMember(T value, IntervalNotation notation = IntervalNotation.Closed) => notation.VerifyMember(value, m_minValue, m_maxValue);

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
      => a.m_minValue.CompareTo(b.m_maxValue) < 0 && b.m_minValue.CompareTo(a.m_maxValue) < 0;

    /// <summary>
    /// <para>Returns the maximum high value of a and b.</para>
    /// </summary>
    public static T LargerMaximum(Interval<T> a, Interval<T> b)
      => a.m_maxValue.CompareTo(b.m_maxValue) >= 0 ? a.m_maxValue : b.m_maxValue;

    /// <summary>
    /// <para>Returns the maximum low value of a and b.</para>
    /// </summary>
    public static T LargerMinimum(Interval<T> a, Interval<T> b)
      => a.m_minValue.CompareTo(b.m_minValue) >= 0 ? a.m_minValue : b.m_minValue;

    /// <summary>
    /// <para>The relative complement of B in A (also called the set-theoretic difference of A and B), denoted by A \ B (or A − B), is the set of all elements that are members of A, but not members of B.</para>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<Interval<T>> LeftDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_minValue.CompareTo(b.m_minValue) < 0) list.Add(new Interval<T>(a.m_minValue, b.m_minValue));
        if (a.m_maxValue.CompareTo(b.m_maxValue) > 0) list.Add(new Interval<T>(b.m_maxValue, a.m_maxValue));
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
        if (b.m_minValue.CompareTo(a.m_minValue) < 0) list.Add(new Interval<T>(b.m_minValue, a.m_minValue));
        if (b.m_maxValue.CompareTo(a.m_maxValue) > 0) list.Add(new Interval<T>(a.m_maxValue, b.m_maxValue));
      }
      else list.Add(b);

      return list;
    }

    /// <summary>
    /// <para>Returns the minimum high value of a and b.</para>
    /// </summary>
    public static T SmallerMaximum(Interval<T> a, Interval<T> b)
      => a.m_maxValue.CompareTo(b.m_maxValue) <= 0 ? a.m_maxValue : b.m_maxValue;

    /// <summary>
    /// <para>Returns the minimum low value of a and b.</para>
    /// </summary>
    public static T SmallerMinimum(Interval<T> a, Interval<T> b)
      => a.m_minValue.CompareTo(b.m_minValue) <= 0 ? a.m_minValue : b.m_minValue;

    /// <summary>
    /// <para>The symmetric difference, an extension of the complement, of two sets A and B, denoted by (A \ B) ∪ (B \ A) or (A - B) ∪ (B - A), is the set of all elements that are members from A, but not members of B union all elements that are members of B but not members of A.</para>
    /// </summary>
    public static System.Collections.Generic.IList<Interval<T>> SymmetricDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_minValue.CompareTo(b.m_minValue) < 0) list.Add(new Interval<T>(a.m_minValue, b.m_minValue));
        if (b.m_minValue.CompareTo(a.m_minValue) < 0) list.Add(new Interval<T>(b.m_minValue, a.m_minValue));
        if (a.m_maxValue.CompareTo(b.m_maxValue) < 0) list.Add(new Interval<T>(a.m_maxValue, b.m_maxValue));
        if (b.m_maxValue.CompareTo(a.m_maxValue) < 0) list.Add(new Interval<T>(b.m_maxValue, a.m_maxValue));
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
    public static explicit operator System.ValueTuple<T, T>(Interval<T> vr) => (vr.m_minValue, vr.m_maxValue);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? obj) => obj is Interval<T> o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Interval<T> other) => (m_minValue.CompareTo(other.m_minValue) is var cmin && cmin != 0) ? cmin : (m_maxValue.CompareTo(other.m_maxValue) is var cmax && cmax != 0) ? cmax : 0;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => IntervalNotation.Closed.ToNotationString(m_minValue, m_maxValue, format, formatProvider);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
