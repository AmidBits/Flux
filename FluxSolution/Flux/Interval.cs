namespace Flux
{
  public static class XtensionInterval
  {
    public static Interval<T> Create<T>(T minValue, T maxValue)
      where T : System.IComparable<T>
      => new(minValue, maxValue);

    #region GetExtent

    /// <summary>
    /// <para>Gets a new min/max interval (depending on type <typeparamref name="T"/>) using the specified <see cref="IntervalNotation"/>, <paramref name="minValue"/>, <paramref name="maxValue"/> and <paramref name="magnitude"/>.</para>
    /// <para>If <typeparamref name="T"/> is an integer, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + 1) and (<paramref name="maxValue"/> - 1).</para>
    /// <para>If <typeparamref name="T"/> is a floating point value, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + epsilon) and (<paramref name="maxValue"/> - epsilon). In this context epsilon is a value that makes the original value and the new value not equal.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="intervalNotation"></param>
    /// <param name="magnitude">This is the magnitude of the extent to aim for. It is essentially a multiplier of the minimum possible extent (when any open interval is chosen).</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static Interval<T> GetExtent<T>(this Interval<T> source, IntervalNotation intervalNotation = IntervalNotation.Closed, int magnitude = 1)
      where T : System.Numerics.INumber<T>
    {
      var (minValue, maxValue) = intervalNotation.GetExtentRelative(source.MinValue, source.MaxValue, magnitude);

      return new(minValue, maxValue);
    }

    /// <summary>
    /// <para>Attempts to get a new min/max interval (depending on type <typeparamref name="T"/>) using the specified <see cref="IntervalNotation"/>, <paramref name="minValue"/>, <paramref name="maxValue"/> and <paramref name="magnitude"/>.</para>
    /// <para>If <typeparamref name="T"/> is an integer, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + 1 * 1) and (<paramref name="maxValue"/> - 1 * 1).</para>
    /// <para>If <typeparamref name="T"/> is a floating point value, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + epsilon * 1) and (<paramref name="maxValue"/> - epsilon * 1). In this context epsilon is a value that makes the original value and the new value not equal.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="extent"></param>
    /// <param name="intervalNotation"></param>
    /// <param name="magnitude">This is the magnitude of the extent to aim for. It is essentially a multiplier of the minimum possible extent (when any open interval is chosen).</param>
    /// <returns></returns>
    public static bool TryGetExtent<T>(this Interval<T> source, out Interval<T> extent, IntervalNotation intervalNotation = IntervalNotation.Closed, int magnitude = 1)
      where T : System.Numerics.INumber<T>
    {
      try
      {
        extent = GetExtent(source, intervalNotation, magnitude);
        return true;
      }
      catch { }

      extent = default!;
      return false;
    }

    #endregion // GetExtent

    #region GetMargin

    /// <summary>
    /// <para>Gets a new min/max interval, using the <paramref name="intervalNotation"/> <see cref="IntervalNotation"/> where <paramref name="minMargin"/>/<paramref name="maxMargin"/> are absolute margins applied to <paramref name="minValue"/>/<paramref name="maxValue"/> respectively.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="minMargin"></param>
    /// <param name="maxMargin"></param>
    /// <param name="intervalNotation"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static Interval<T> GetMargin<T>(this Interval<T> source, T margin, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.INumber<T>
    {
      var (minValue, maxValue) = intervalNotation.GetExtentAbsolute(source.MinValue, source.MaxValue, margin);

      return new(minValue, maxValue);
    }

    /// <summary>
    /// <para>Attempts to get a new min/max interval, using the <paramref name="intervalNotation"/> <see cref="IntervalNotation"/> where <paramref name="minMargin"/>/<paramref name="maxMargin"/> are absolute margins applied to <paramref name="minValue"/>/<paramref name="maxValue"/> respectively.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="minMargin"></param>
    /// <param name="maxMargin"></param>
    /// <param name="margin"></param>
    /// <param name="intervalNotation"></param>
    /// <returns></returns>
    public static bool TryGetMargin<T>(this Interval<T> source, T margin, out Interval<T> result, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.INumber<T>
    {
      try
      {
        result = GetMargin(source, margin, intervalNotation);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    #endregion // GetMargin

    /// <summary>
    /// <para>Calculates the offset and length of an <see cref="Interval{T}"/>. The offset is the same as <see cref="Interval{T}.MinValue"/> of <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (int Offset, int Length) GetOffsetAndLength<TInteger>(this Interval<TInteger> source, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => intervalNotation.GetOffsetAndLength(source.MinValue, source.MaxValue);

    /// <summary>
    /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="Interval{TSelf}"/> using the <paramref name="constraint"/> .</para>
    /// </summary>
    /// <param name="source">The <see cref="Interval{TSelf}"/>.</param>
    /// <param name="step">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
    /// <param name="intervalNotation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> Range<TSelf>(this Interval<TSelf> source, TSelf step, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var (minMargin, maxMargin) = source.GetMargin(TSelf.Abs(step), intervalNotation);

      var count = System.Numerics.BigInteger.One;

      if (TSelf.IsNegative(step)) // A negative number yields a descending sequence from maxValue to minValue of the interval.
        for (var number = maxMargin; number >= minMargin; number = maxMargin + step * TSelf.CreateChecked(count), count++)
          yield return number;
      else if (!TSelf.IsZero(step)) // Any positive number but zero yields an ascending sequence from minValue to maxValue of the interval.
        for (var number = minMargin; number <= maxMargin; number = minMargin + step * TSelf.CreateChecked(count), count++)
          yield return number;
      else // The argument "step" is zero and that is an invalid value.
        throw new System.ArgumentOutOfRangeException(nameof(step));
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para>The Start property of Range is inclusive, but the End property is exclusive.</para>
    /// <para>Both the Min and the Max properties on an Interval are inclusive.</para>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Range ToRange<TInteger>(this Interval<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => IntervalNotation.Closed.ToRange(source.MinValue, source.MaxValue);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="intervalNotation"></param>
    /// <returns></returns>
    public static T WrapAround<T>(this Interval<T> source, T value, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.INumber<T>
    {
      var (minValue, maxValue) = source.GetExtent(intervalNotation);

      var cmp = intervalNotation.Compare(value, minValue, maxValue);

      var addon = value != minValue && value != maxValue ? T.One : T.Zero;

      if (cmp > 0)
        return minValue + (value - maxValue - addon) % (maxValue - minValue + addon);
      else if (cmp < 0)
        return maxValue - (minValue - value - addon) % (maxValue - minValue + addon);
      else
        return value;
    }
  }

  /// <summary>
  /// <para>Represents a closed interval or value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</para>
  /// <para>An interval is displayed using standard mathematical interval notation.</para>
  /// <see href="https://en.wikipedia.org/wiki/Interval_(mathematics)"/>
  /// </summary>
  public readonly record struct Interval<T>
    : System.IComparable, System.IComparable<Interval<T>>, System.IFormattable
    where T : System.IComparable<T>
  {
    /// <remarks>The default interval is representing a degenerate interval with the default value of <typeparamref name="T"/>, i.e. zero for standard numerical types, e.g. int, double, BigInteger, etc.</remarks>
    public readonly static Interval<T> Default;

    private readonly T m_maxValue;
    private readonly T m_minValue;

    public Interval(T minValue, T maxValue)
      => (m_minValue, m_maxValue) = IntervalNotation.Closed.AssertValid(minValue, maxValue);

    public void Deconstruct(out T minValue, out T maxValue)
    {
      minValue = m_minValue;
      maxValue = m_maxValue;
    }

    public T MaxValue => m_maxValue;
    public T MinValue => m_minValue;

    #region Static methods

    /// <summary>
    /// <para>The intersection of A and B, denoted by A ∩ B, is the set of all things that are members of both A and B. If A ∩ B = none, then A and B are said to be disjoint.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> Intersect(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
        list.Add(new Interval<T>(LargerMinimum(a, b), SmallerMaximum(a, b)));

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
    public static System.Collections.Generic.List<Interval<T>> LeftDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (a.m_minValue.CompareTo(b.m_minValue) < 0) list.Add(new Interval<T>(a.m_minValue, b.m_minValue));
        if (a.m_maxValue.CompareTo(b.m_maxValue) > 0) list.Add(new Interval<T>(b.m_maxValue, a.m_maxValue));
      }
      else
        list.Add(a);

      return list;
    }

    /// <summary>
    /// <para>Right different is the set of all elements that are members of B, but not members of A.</para>
    /// </summary>
    public static System.Collections.Generic.List<Interval<T>> RightDifference(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (IsOverlapping(a, b))
      {
        if (b.m_minValue.CompareTo(a.m_minValue) < 0) list.Add(new Interval<T>(b.m_minValue, a.m_minValue));
        if (b.m_maxValue.CompareTo(a.m_maxValue) > 0) list.Add(new Interval<T>(a.m_maxValue, b.m_maxValue));
      }
      else
        list.Add(b);

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
    public static System.Collections.Generic.List<Interval<T>> SymmetricDifference(Interval<T> a, Interval<T> b)
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
    public static System.Collections.Generic.List<Interval<T>> Union(Interval<T> a, Interval<T> b)
    {
      var list = new System.Collections.Generic.List<Interval<T>>();

      if (!IsOverlapping(a, b))
      {
        list.Add(a);
        list.Add(b);
      }
      else
        list.Add(new Interval<T>(SmallerMinimum(a, b), LargerMaximum(a, b)));

      return list;
    }

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator Interval<T>(System.ValueTuple<T, T> vt) => new(vt.Item1, vt.Item2);
    public static explicit operator System.ValueTuple<T, T>(Interval<T> vr) => (vr.m_minValue, vr.m_maxValue);

    #endregion // Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? obj)
      => obj is Interval<T> o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Interval<T> other)
      => (m_minValue.CompareTo(other.m_minValue) is var cmin && cmin != 0) ? cmin : (m_maxValue.CompareTo(other.m_maxValue) is var cmax && cmax != 0) ? cmax : 0;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => IntervalNotation.Closed.ToIntervalNotationString(m_minValue, m_maxValue, format, formatProvider);

    #endregion Implemented interfaces

    public override string ToString()
      => ToString(null, null);
  }
}
