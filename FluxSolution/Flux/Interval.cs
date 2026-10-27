namespace Flux
{
  public static class IntervalExtensions
  {
    extension<TNumber>(Interval<TNumber>)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>The unit interval represents [0, 1].</para>
      /// </summary>
      public static Interval<TNumber> UnitInterval => new(TNumber.Zero, TNumber.One);

      /// <summary>
      /// <para>Creates a new <see cref="Interval{T}"/> with the specified <paramref name="minMargin"/>/<paramref name="maxMargin"/> applied.</para>
      /// </summary>
      /// <param name="minMargin">The amount of margin to add to or remove from (negative) an <see cref="Interval{T}.MinValue"/>.</param>
      /// <param name="maxMargin">The amount of margin to add to or remove from (negative) an <see cref="Interval{T}.MaxValue"/>.</param>
      /// <returns></returns>
      public static (TNumber MinValue, TNumber MaxValue) NewByMargin(TNumber minValue, TNumber maxValue, TNumber minMargin, TNumber maxMargin)
        => (minValue - minMargin, maxValue + maxMargin);

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="minUlps">The number of ULPs to add to or remove from an <see cref="Interval{T}.MinValue"/>.</param>
      /// <param name="maxUlps">The number of ULPs to add to or remove from an <see cref="Interval{T}.MaxValue"/>.</param>
      /// <returns></returns>
      public static (TNumber MinValue, TNumber MaxValue) NewByUlps(TNumber minValue, TNumber maxValue, int minUlps, int maxUlps)
      {
        minValue = (minUlps < 0) ? Number.UlpIncrement(minValue, minUlps) : (minUlps > 0) ? Number.UlpDecrement(minValue, minUlps) : minValue;
        maxValue = (maxUlps < 0) ? Number.UlpDecrement(maxValue, maxUlps) : (maxUlps > 0) ? Number.UlpIncrement(maxValue, maxUlps) : maxValue;

        return (minValue, maxValue);
      }
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
      public Interval<TNumber> RoundToNearestIntegers<TNumber>(NearestRoundingRule minValueRounding, NearestRoundingRule maxValueRounding, out TNumber minValue, out TNumber maxValue)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        minValue = TNumber.CreateChecked(FloatingPoint.RoundToNearestInteger(source.MinValue, minValueRounding));
        maxValue = TNumber.CreateChecked(FloatingPoint.RoundToNearestInteger(source.MaxValue, maxValueRounding));

        return new(minValue, maxValue);
      }
    }

    // https://math.stackexchange.com/a/4894702
    extension<TInteger>(Interval<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
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
      public (int Offset, int Length) GetOffsetAndLength(IntervalNotation intervalNotation = IntervalNotation.Closed)
      {
        var index = int.CreateChecked(source.MinValue);
        var length = int.CreateChecked(source.MaxValue - source.MinValue);

        if (intervalNotation is IntervalNotation.HalfOpenLeft or IntervalNotation.Open)
          index++;

        if (intervalNotation is IntervalNotation.Closed)
          length++;
        else if (intervalNotation is IntervalNotation.Open)
          length--;

        if (length <= 0)
          throw new System.InvalidOperationException();

        return (index, length);
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
      public System.Range ToRange(IntervalNotation intervalNotation = IntervalNotation.Closed)
      {
        var (minValue, maxValue) = source.NewByMargin(TInteger.One, intervalNotation);

        return new(int.CreateChecked(minValue), int.CreateChecked(maxValue) + 1);
      }
    }

    extension<TNumber>(Interval<TNumber> interval)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>(min + max) / 2</para>
      /// </summary>
      public TNumber Center => (interval.MinValue + interval.MaxValue) / TNumber.CreateChecked(2);

      /// <summary>
      /// <para>abs(min - max)</para>
      /// </summary>
      public TNumber Diameter => TNumber.Abs(interval.MinValue - interval.MaxValue);

      /// <summary>
      /// <para>abs(min - max) / 2</para>
      /// </summary>
      public TNumber Radius => TNumber.Abs(interval.MinValue - interval.MaxValue) / TNumber.CreateChecked(2);

      public TNumber CountMultiplesInRange(TNumber multiple)
        => Number.IntegerDivRemFloored(interval.MaxValue, multiple).Quotient - Number.IntegerDivRemCeiling(interval.MinValue, multiple).Quotient + TNumber.One;

      /// <summary>
      /// <para>Creates a new sequence of values by iterating over an <see cref="Interval{TSelf}"/> with the specified <paramref name="stepSize"/> and <paramref name="intervalNotation"/>.</para>
      /// </summary>
      /// <param name="stepSize">The step-size between iterations. A positive number iterates in the direction of min-max. A negative number iterates in the direction of max-min.</param>
      /// <param name="intervalNotation">Indicates how to handle the interval endpoints.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TNumber> Loop(TNumber stepSize)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);

        var (minValue, maxValue) = (interval.MinValue, interval.MaxValue);

        return TNumber.IsNegative(stepSize)
          ? Number.ArithmeticSequence(maxValue, stepSize).TakeWhile(n => n >= minValue)
          : Number.ArithmeticSequence(minValue, stepSize).TakeWhile(n => n <= maxValue);
      }

      ///// <summary>
      ///// <para>Creates a new <see cref="Interval{T}"/> with the specified <paramref name="minMargin"/>/<paramref name="maxMargin"/> applied.</para>
      ///// </summary>
      ///// <param name="minMargin">The amount of margin to add to or remove from (negative) an <see cref="Interval{T}.MinValue"/>.</param>
      ///// <param name="maxMargin">The amount of margin to add to or remove from (negative) an <see cref="Interval{T}.MaxValue"/>.</param>
      ///// <returns></returns>
      //public Interval<TNumber> NewByMargin(TNumber minMargin, TNumber maxMargin)
      //  => new(interval.MinValue - minMargin, interval.MaxValue + maxMargin);

      /// <summary>
      /// <para>Creates a new <see cref="Interval{T}"/> with the specified <paramref name="margin"/> applied according to the <paramref name="intervalNotation"/>.</para>
      /// </summary>
      /// <param name="margin">The margin to add to or remove from an <see cref="Interval{T}"/>.</param>
      /// <param name="intervalNotation">Determines where the margin should be applied.</param>
      /// <returns></returns>
      public Interval<TNumber> NewByMargin(TNumber margin, IntervalNotation intervalNotation = IntervalNotation.Open)
      {
        if (intervalNotation == IntervalNotation.Closed)
          return interval;

        var minMargin = intervalNotation is IntervalNotation.Open or IntervalNotation.HalfOpenLeft ? margin : TNumber.Zero;
        var maxMargin = intervalNotation is IntervalNotation.Open or IntervalNotation.HalfOpenRight ? margin : TNumber.Zero;

        var (minValue, maxValue) = Interval<TNumber>.NewByMargin(interval.MinValue, interval.MaxValue, minMargin, maxMargin);

        return new(minValue, maxValue);
      }

      ///// <summary>
      ///// <para></para>
      ///// </summary>
      ///// <param name="minUlps">The number of ULPs to add to or remove from an <see cref="Interval{T}.MinValue"/>.</param>
      ///// <param name="maxUlps">The number of ULPs to add to or remove from an <see cref="Interval{T}.MaxValue"/>.</param>
      ///// <returns></returns>
      //public Interval<TNumber> NewByUlps(int minUlps, int maxUlps)
      //{
      //  var (minValue, maxValue) = (interval.MinValue, interval.MaxValue);

      //  minValue = (minUlps < 0) ? Number.UlpIncrement(minValue, minUlps) : (minUlps > 0) ? Number.UlpDecrement(minValue, minUlps) : minValue;
      //  maxValue = (maxUlps < 0) ? Number.UlpDecrement(maxValue, maxUlps) : (maxUlps > 0) ? Number.UlpIncrement(maxValue, maxUlps) : maxValue;

      //  return new(minValue, maxValue);
      //}

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="ulps">The number of ULPs to add to or remove from an <see cref="Interval{T}"/>.</param>
      /// <param name="intervalNotation">Determines where the ULPs should be applied.</param>
      /// <returns></returns>
      public Interval<TNumber> NewByUlps(int ulps, IntervalNotation intervalNotation = IntervalNotation.Open)
      {
        if (intervalNotation == IntervalNotation.Closed)
          return interval;

        var minUlps = intervalNotation is IntervalNotation.Open or IntervalNotation.HalfOpenLeft ? ulps : 0;
        var maxUlps = intervalNotation is IntervalNotation.Open or IntervalNotation.HalfOpenRight ? ulps : 0;

        var (minValue, maxValue) = Interval<TNumber>.NewByUlps(interval.MinValue, interval.MaxValue, minUlps, maxUlps);

        return new(minValue, maxValue);
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
        var q = interval.Diameter / TNumber.CreateChecked(count);
        var r = interval.Diameter - (q * TNumber.CreateChecked(count));

        var minValue = interval.MinValue;

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
        var (minValue, maxValue) = (interval.MinValue, interval.MaxValue);

        var cmp = Interval<TNumber>.CompareWith(value, minValue, maxValue, IntervalNotation.Closed);

        var eps = value != minValue && value != maxValue ? TNumber.One : TNumber.Zero;

        if (cmp > 0)
          return minValue + (value - maxValue - eps) % (maxValue - minValue + eps);
        else if (cmp < 0)
          return maxValue - (minValue - value - eps) % (maxValue - minValue + eps);
        else
          return value;
      }
    }
  }

  /// <summary>
  /// <para>Represents a closed interval or continuous value set, for various set operations, e.g. difference, intersect, union, min, max, etc. Uses IComparable to operate.</para>
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
      => (m_minValue, m_maxValue) = Interval<T>.AssertValidInterval(minValue, maxValue, IntervalNotation.Closed);

    public void Deconstruct(out T minValue, out T maxValue)
    {
      (minValue, maxValue) = (m_minValue, m_maxValue);
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

    public bool IsInInterval(T value, IntervalNotation notation = IntervalNotation.Closed) => IsInInterval(value, m_minValue, m_maxValue, notation);

    /// <summary>
    /// <para>Compares a <paramref name="value"/> to the closed <see cref="Interval{T}"/> instance.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns>
    /// <list type="bullet">
    /// <item>1 means that <paramref name="value"/> is greater-than <see cref="Interval{T}.MaxValue"/>, and hence not a member of the interval.</item>
    /// <item>0 means that <paramref name="value"/> is a member of the interval.</item>
    /// <item>-1 means that <paramref name="value"/> is less-than <see cref="Interval{T}.MinValue"/>, and hence not a member of the interval.</item>
    /// </list>
    /// </returns>
    public int Compare(T value) => CompareWith(value, m_minValue, m_maxValue, IntervalNotation.Closed);

    /// <summary>
    /// <para>Compares a <paramref name="value"/> to the <see cref="Interval{T}"/> instance with <paramref name="notation"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="notation"></param>
    /// <returns>
    /// <para>The '(-or-equal)' below depends on the openess of the specified <paramref name="notation"/>.</para>
    /// <list type="bullet">
    /// <item>1 means that <paramref name="value"/> is greater-than(-or-equal) to <see cref="Interval{T}.MaxValue"/>, and hence not a member of the interval.</item>
    /// <item>0 means that <paramref name="value"/> is a member of the interval.</item>
    /// <item>-1 means that <paramref name="value"/> is less-than(-or-equal) to <see cref="Interval{T}.MinValue"/>, and hence not a member of the interval.</item>
    /// </list>
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Compare(T value, IntervalNotation notation = IntervalNotation.Closed) => CompareWith(value, m_minValue, m_maxValue, notation);

    #region Static methods

    /// <summary>
    /// <para>Asserts that a <paramref name="value"/> is a member of an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/>. Throws an exception if it's not a member.</para>
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    /// <param name="intervalNotation">The interval notation to apply when asserting the value.</param>
    /// <param name="value">The value to assert.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <param name="paramName">Optional parameter name for the exception.</param>
    /// <returns>The <paramref name="value"/>, if a member, otherwise an exception is thrown.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static T AssertInInterval(T value, T minValue, T maxValue, IntervalNotation notation = IntervalNotation.Closed, string? paramName = "value")
      => IsInInterval(value, minValue, maxValue, notation)
      ? value
      : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"Not in interval: {{{value}}} is not in {notation.ToIntervalNotationString(minValue, maxValue)}.");

    /// <summary>
    /// <para>Asserts that a <paramref name="notation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/> is valid. Throws an exception if it's invalid.</para>
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="notation"></param>
    /// <param name="paramName"></param>
    /// <exception cref="System.ArgumentException"></exception>
    public static (T minValue, T maxValue) AssertValidInterval(T minValue, T maxValue, IntervalNotation notation = IntervalNotation.Closed, string? paramName = "minValue/maxValue")
      => IsValidInterval(minValue, maxValue, notation)
      ? (minValue, maxValue)
      : throw new System.ArgumentException($"Invalid interval: {notation.ToIntervalNotationString(minValue, maxValue)}.", paramName);

    /// <summary>
    /// <para>Clamp the current instance of a <see cref="System.IComparable{T}"/> to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static T Clamp(T value, T minValue, T maxValue)
      => value.CompareTo(minValue) < 0
      ? minValue
      : value.CompareTo(maxValue) > 0
      ? maxValue
      : value;

    /// <summary>
    /// <para>Compares a value with a <paramref name="notation"/> interval minValue..maxValue.</para>
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="notation"></param>
    /// <returns>
    /// <list type="bullet">
    /// <item>1 when <paramref name="value"/> is above <paramref name="maxValue"/> of the <paramref name="notation"/> interval</item>
    /// <item>-1 when <paramref name="value"/> is below <paramref name="minValue"/> of the <paramref name="notation"/> interval</item>
    /// <item>0 when <paramref name="value"/> is in the <paramref name="notation"/> interval</item>
    /// </list>
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static int CompareWith(T value, T minValue, T maxValue, IntervalNotation notation = IntervalNotation.Closed)
    {
      var vcmpmin = value.CompareTo(minValue);
      var vcmpmax = value.CompareTo(maxValue);

      if (notation == IntervalNotation.Closed) return vcmpmin < 0 ? -1 : vcmpmax > 0 ? +1 : 0;
      else if (notation == IntervalNotation.HalfOpenLeft) return vcmpmin <= 0 ? -1 : vcmpmax > 0 ? +1 : 0;
      else if (notation == IntervalNotation.HalfOpenRight) return vcmpmin < 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
      else if (notation == IntervalNotation.Open) return vcmpmin <= 0 ? -1 : vcmpmax >= 0 ? +1 : 0;
      else throw new System.NotImplementedException(nameof(notation));
    }

    public static Interval<T> Create(T minValue, T maxValue) => new(minValue, maxValue);

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
    /// <para>Determines whether an interval <paramref name="minValue"/>..<paramref name="maxValue"/> with a specified <see cref="IntervalNotation"/> represents the empty set.</para>
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <remarks>
    /// <list type="bullet">
    /// <item>If <paramref name="minValue"/> equals <paramref name="maxValue"/>, all but the closed notation represents the empty set.</item>
    /// <item>If <paramref name="minValue"/> is greater than <paramref name="maxValue"/>, all four notations are usually taken to represent the empty set.</item>
    /// </list>
    /// </remarks>
    public static bool IsEmptySet(T minValue, T maxValue, IntervalNotation notation = IntervalNotation.Closed)
      => (notation != IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0) // If minValue equals maxValue, all but the closed notation represents the empty set.
      || (minValue.CompareTo(maxValue) > 0 || maxValue.CompareTo(minValue) < 0); // If minValue is greater than maxValue, all four notations are usually taken to represent the empty set.

    /// <summary>
    /// <para>Determines whether a value is in the <paramref name="notation"/> <see cref="Interval{T}"/>: [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
    /// </summary>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <returns>Whether the <paramref name="value"/> is a member of the interval.</returns>
    public static bool IsInInterval(T value, T minValue, T maxValue, IntervalNotation notation = IntervalNotation.Closed)
      => CompareWith(value, minValue, maxValue, notation) == 0;

    /// <summary>
    /// <para>Returns whether a and b are overlapping.</para>
    /// </summary>
    public static bool IsOverlapping(Interval<T> a, Interval<T> b)
      => b.m_minValue.CompareTo(a.m_maxValue) <= 0 && b.m_maxValue.CompareTo(a.m_minValue) >= 0;

    /// <summary>
    /// <para>Determines whether an <see cref="IntervalNotation"/> interval <paramref name="minValue"/>..<paramref name="maxValue"/> is a valid interval.</para>
    /// </summary>
    /// <typeparam name="TComparable"></typeparam>
    /// <param name="source"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static bool IsValidInterval(T minValue, T maxValue, IntervalNotation notation = IntervalNotation.Closed)
      => (notation == IntervalNotation.Closed && minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) >= 0)
      || (notation == IntervalNotation.HalfOpenRight && minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) > 0)
      || (notation == IntervalNotation.HalfOpenLeft && minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) >= 0)
      || (notation == IntervalNotation.Open && minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) > 0);

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
