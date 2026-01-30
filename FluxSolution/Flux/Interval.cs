namespace Flux
{
  public static class IntervalExtensions
  {
    public static Interval<T> Create<T>(T minValue, T maxValue)
      where T : System.IComparable<T>
      => new(minValue, maxValue);

    extension<TNumber>(Interval<TNumber>)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>The unit interval represents [0, 1].</para>
      /// </summary>
      public static Interval<TNumber> UnitInterval => new(TNumber.Zero, TNumber.One);
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
      public Interval<TNumber> Round<TNumber>(HalfRounding minValueRounding, HalfRounding maxValueRounding, out TNumber minValue, out TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
        => new(minValue = TNumber.CreateChecked(IFloatingPoint.RoundHalf(source.MinValue, minValueRounding)), maxValue = TNumber.CreateChecked(IFloatingPoint.RoundHalf(source.MaxValue, maxValueRounding)));
    }

    //// https://math.stackexchange.com/a/4894702
    extension<TInteger>(Interval<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      public TInteger CountMultiplesInRange(TInteger k, IntervalNotation intervalNotation = IntervalNotation.Closed)
      {
        var (minValue, maxValue) = intervalNotation.GetExtentRelative(source.MinValue, source.MaxValue, 1);

        return IBinaryInteger.FlooredDivRem(maxValue, k).Quotient - IBinaryInteger.CeilingDivRem(minValue, k).Quotient + TInteger.One;
      }

      /// <summary>
      /// <para>Creates a new <see cref="Interval{T}"/> based on a <paramref name="guess"/> and how the guess <paramref name="compare"/> (as in <see cref="System.IComparable{T}"/>) to a number which is a member of the <paramref name="source"/> interval.</para>
      /// <para>If <paramref name="compare"/> is less than 0 (guess is too low): <c>[<paramref name="guess"/> + 1, <see cref="Interval{T}.MaxValue"/>]</c>.</para>
      /// <para>If <paramref name="compare"/> is greater than 0 (guess is too high): <c>[<see cref="Interval{T}.MinValue"/>, <paramref name="guess"/> - 1]</c>.</para>
      /// <para>If <paramref name="compare"/> is equal to 0 (guess is correct): <c>[<paramref name="guess"/>, <paramref name="guess"/>]</c> and therefor with its property <c><see cref="Interval{T}.IsDegenerate"/> = true</c>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="guess">The value of the guess.</param>
      /// <param name="compare">The sign of the guess compared to the chosen number. -1 = guess is too low, 1 = guess is too high, 0 = guess is the chosen number.</param>
      /// <returns></returns>
      public Interval<TInteger> CreateNewBasedOnGuess(TInteger guess, int compare)
        => compare < 0
        ? new(guess + TInteger.One, source.MaxValue)
        : compare > 0
        ? new(source.MinValue, guess - TInteger.One)
        : new(guess, guess);

      /// <summary>
      /// <para>Gets a guess of a number which is a member of an <see cref="Interval{T}"/>.</para>
      /// <para>The formula is a simple: <c>guess = (minValue + maxValue) / 2</c>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TInteger GetGuess()
        => source.Center;

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

    extension<TNumber>(Interval<TNumber> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>(min + max) / 2</para>
      /// </summary>
      public TNumber Center
        => (source.MinValue + source.MaxValue) / TNumber.CreateChecked(2);

      /// <summary>
      /// <para>abs(min - max)</para>
      /// </summary>
      public TNumber Diameter
        => TNumber.Abs(source.MinValue - source.MaxValue);

      /// <summary>
      /// <para>abs(min - max) / 2</para>
      /// </summary>
      public TNumber Radius
        => TNumber.Abs(source.MinValue - source.MaxValue) / TNumber.CreateChecked(2);

      public TNumber CountMultiplesInRanger(TNumber k, IntervalNotation intervalNotation = IntervalNotation.Closed)
      {
        var (minValue, maxValue) = intervalNotation.GetExtentRelative(source.MinValue, source.MaxValue, 1);

        //return ((maxValue - minValue) - TNumber.One) / k;

        //return (maxValue / k) - (minValue / k) + (minValue % k > TNumber.Zero ? TNumber.One : TNumber.Zero) + TNumber.One;

        return (maxValue - (maxValue % k) - minValue - ((-minValue) % k)) / k + TNumber.One;
      }

      /// <summary>
      /// <para>Creates a new sequence of values by iterating over an <see cref="Interval{TSelf}"/> with the specified <paramref name="stepSize"/> and <paramref name="intervalNotation"/>.</para>
      /// </summary>
      /// <param name="stepSize">The step-size between iterations. A positive number iterates in the direction of min-max. A negative number iterates in the direction of max-min.</param>
      /// <param name="intervalNotation">Indicates how to handle the interval endpoints.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TNumber> Iterate(TNumber stepSize, IntervalNotation intervalNotation = IntervalNotation.Closed)
      {
        var (minValue, maxValue) = intervalNotation.GetExtentAbsolute(source.MinValue, source.MaxValue, TNumber.Abs(stepSize));

        if (TNumber.IsNegative(stepSize)) return INumber.LoopVerge(maxValue, stepSize).TakeWhile(n => n >= minValue);
        else if (!TNumber.IsZero(stepSize)) return INumber.LoopVerge(minValue, stepSize).TakeWhile(n => n <= maxValue);
        else throw new System.ArgumentOutOfRangeException(nameof(stepSize));
      }

      /// <summary>
      /// <para>Sub-divides an interval into sub-intervals.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<Interval<TNumber>> Subdivide<TInteger>(TInteger count)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        var q = source.Diameter / TNumber.CreateChecked(count);
        var r = source.Diameter - (q * TNumber.CreateChecked(count));

        var minValue = source.MinValue;

        for (var index = TInteger.Zero; index < count; index++)
        {
          var maxValue = minValue + q + (TNumber.CreateChecked(index) < r ? TNumber.One : TNumber.Zero);

          yield return new Interval<TNumber>(minValue, maxValue);

          minValue = maxValue;
        }
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
    public static Interval<T> Default { get; } = new(default!, default!);

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
      => b.m_minValue.CompareTo(a.m_maxValue) <= 0 && b.m_maxValue.CompareTo(a.m_minValue) >= 0;

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
