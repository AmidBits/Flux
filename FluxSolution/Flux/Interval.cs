namespace Flux
{
  public static class XtensionInterval
  {
    public static Interval<T> Create<T>(T minValue, T maxValue)
      where T : System.IComparable<T>
      => new(minValue, maxValue);

    extension<TInteger>(Interval<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Creates a new <see cref="Interval{T}"/> based on a guessed <paramref name="value"/> and how the guess <paramref name="compare"/> (as in <see cref="System.IComparable{T}"/>) to a number which is a member of the <paramref name="source"/> interval.</para>
      /// <para>If <paramref name="compare"/> is less than 0 (guess is too low): <c>[<paramref name="value"/> + 1, <see cref="Interval{T}.MaxValue"/>]</c>.</para>
      /// <para>If <paramref name="compare"/> is greater than 0 (guess is too high): <c>[<see cref="Interval{T}.MinValue"/>, <paramref name="value"/> - 1]</c>.</para>
      /// <para>If <paramref name="compare"/> is equal to 0 (guess is correct): <c>[<paramref name="value"/>, <paramref name="value"/>]</c> and therefor with its property <c><see cref="Interval{T}.IsDegenerate"/> = true</c>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="source"></param>
      /// <param name="value">The value of the guess.</param>
      /// <param name="compare">The sign of the guess compared to the chosen number. -1 = guess is too low, 1 = guess is too high, 0 = guess is the chosen number.</param>
      /// <returns></returns>
      public Interval<TInteger> CreateNewBasedOnGuess(TInteger value, int compare)
        => compare < 0
        ? new(value + TInteger.One, source.MaxValue)
        : compare > 0
        ? new(source.MinValue, value - TInteger.One)
        : new(value, value);

      /// <summary>
      /// <para>Gets a guess of a number which is a member of an <see cref="Interval{T}"/>.</para>
      /// <para>The formula is a simple: <c>guess = (minValue + maxValue) / 2</c>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TInteger GetGuess()
        => (source.MinValue + source.MaxValue) / TInteger.CreateChecked(2);

      /// <summary>
      /// <para>Calculates the offset and length of an <see cref="Interval{T}"/>. The offset is the same as <see cref="Interval{T}.MinValue"/> of <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public (int Offset, int Length) GetOffsetAndLength()
        => IntervalNotation.Closed.GetOffsetAndLength(source.MinValue, source.MaxValue);

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
      public System.Range ToRange()
        => IntervalNotation.Closed.ToRange(source.MinValue, source.MaxValue);
    }

    extension<TFloat>(Interval<TFloat> source)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      /// <summary>
      /// <para>Rounds the values of an <see cref="Interval{T}"/> using the specified rounding and also returns the new values as out parameters.</para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="source"></param>
      /// <param name="minValueRounding"></param>
      /// <param name="maxValueRounding"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public Interval<TInteger> Round<TInteger>(UniversalRounding minValueRounding, UniversalRounding maxValueRounding, out TInteger minValue, out TInteger maxValue)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => new(minValue = TInteger.CreateChecked(source.MinValue.RoundUniversal(minValueRounding)), maxValue = TInteger.CreateChecked(source.MaxValue.RoundUniversal(maxValueRounding)));
    }

    extension<TNumber>(Interval<TNumber> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="Interval{TSelf}"/> using the <paramref name="constraint"/> .</para>
      /// </summary>
      /// <param name="source">The <see cref="Interval{TSelf}"/>.</param>
      /// <param name="step">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
      /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
      /// <param name="intervalNotation">Specified by <see cref="IntervalNotation"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TNumber> IterateRange(TNumber step)
      {
        var (minValue, maxValue) = (source.MinValue, source.MaxValue);

        if (TNumber.IsNegative(step)) // A negative number yields a descending sequence from maxValue to minValue of the interval.
          return Iterations.LoopVerge(maxValue, step).TakeWhile(n => n >= minValue);
        else if (!TNumber.IsZero(step)) // Any positive number but zero yields an ascending sequence from minValue to maxValue of the interval.
          return Iterations.LoopVerge(minValue, step).TakeWhile(n => n <= maxValue);
        else // The argument "step" is zero and that is an invalid value.
          throw new System.ArgumentOutOfRangeException(nameof(step));
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="source"></param>
      /// <param name="value"></param>
      /// <param name="intervalNotation"></param>
      /// <returns></returns>
      public TNumber Wrap(TNumber value)
      {
        var (minValue, maxValue) = (source.MinValue, source.MaxValue);

        var cmp = IntervalNotation.Closed.Compare(value, minValue, maxValue);

        var addon = value != minValue && value != maxValue ? TNumber.One : TNumber.Zero;

        if (cmp > 0)
          return minValue + (value - maxValue - addon) % (maxValue - minValue + addon);
        else if (cmp < 0)
          return maxValue - (minValue - value - addon) % (maxValue - minValue + addon);
        else
          return value;
      }
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

    /// <summary>
    /// <para>Determines whether the interval <paramref name="minValue"/>..<paramref name="maxValue"/> is a degenerate interval, i.e. the interval consists of only a single value.</para>
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public bool IsDegenerate
      => m_minValue.CompareTo(m_maxValue) == 0 && m_maxValue.CompareTo(m_minValue) == 0;

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
