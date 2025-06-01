namespace Flux
{
  public static partial class Em
  {
    ///// <summary>
    ///// <para>Asserts whether the interval is valid, i.e. throws an exception if it is not.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <exception cref="System.ArgumentException"></exception>
    //public static (T minValue, T maxValue) AssertValidInterval<T>(this IntervalNotation source, T minValue, T maxValue, string? paramName = "minValue/maxValue")
    //  where T : System.IComparable<T>
    //  => Interval<T>.AssertValid(minValue, maxValue, source);
    //=> source.IsValidInterval(minValue, maxValue)
    //? (minValue, maxValue)
    //: throw new System.ArgumentException($"Invalid interval: {source.ToIntervalNotationString(minValue, maxValue)}.", paramName);

    /// <summary>
    /// <para>Asserts that the <paramref name="value"/> is a member of the interval <paramref name="minValue"/>..<paramref name="maxValue"/> according to the specified <see cref="IntervalNotation"/>, and throws an exception if it's not.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The interval notation to apply when asserting the value.</param>
    /// <param name="value">The value to assert.</param>
    /// <param name="minValue">The lower bound of the interval set.</param>
    /// <param name="maxValue">The upper bound of the interval set.</param>
    /// <param name="paramName">Optional parameter name for the exception.</param>
    /// <returns>The <paramref name="value"/>, if a member, otherwise an exception is thrown.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static T AssertIntervalMember<T>(this IntervalNotation source, T value, T minValue, T maxValue, string? paramName = "value")
      where T : System.IComparable<T>
      => Interval<T>.AssertMember(value, minValue, maxValue, source);
    //=> source.IsIntervalMember(value, minValue, maxValue)
    //? value
    //: throw new System.ArgumentOutOfRangeException(paramName ?? nameof(value), $"The value '{value}' is not a member of the interval: {source.ToIntervalNotationString(minValue, maxValue)}.");

    ///// <summary>
    ///// <para>Compares <paramref name="value"/> to the interval <paramref name="minValue"/>..<paramref name="maxValue"/> using the specified <see cref="IntervalNotation"/>.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source">The interval notation to apply when comparing to the interval set.</param>
    ///// <param name="value">The value to compare.</param>
    ///// <param name="minValue">The lower bound of the interval set.</param>
    ///// <param name="maxValue">The upper bound of the interval set.</param>
    ///// <returns>
    ///// <para>The '(-or-equal)' below depends on the <see cref="IntervalNotation"/> specified in <paramref name="source"/>.</para>
    ///// <para>0 if <paramref name="value"/> is a member of the interval.</para>
    ///// <para>-1 if <paramref name="value"/> is not a member, and is less-than(-or-equal) to <paramref name="minValue"/></para>
    ///// <para>+1 if <paramref name="value"/> is not a member, and is greater-than(-or-equal) to <paramref name="maxValue"/>.</para>
    ///// </returns>
    ///// <exception cref="NotImplementedException"></exception>
    //public static int CompareTo<T>(this IntervalNotation source, T value, T minValue, T maxValue)
    //  where T : System.IComparable<T>
    //{
    //  source.AssertValidInterval(minValue, maxValue);

    //  var vctmin = value.CompareTo(minValue);
    //  var vctmax = value.CompareTo(maxValue);

    //  if (source == IntervalNotation.Closed)
    //    return vctmin < 0 ? -1 : vctmax > 0 ? +1 : 0;
    //  else if (source == IntervalNotation.HalfRightOpen)
    //    return vctmin < 0 ? -1 : vctmax >= 0 ? +1 : 0;
    //  else if (source == IntervalNotation.HalfLeftOpen)
    //    return vctmin <= 0 ? -1 : vctmax > 0 ? +1 : 0;
    //  else if (source == IntervalNotation.Open)
    //    return vctmin <= 0 ? -1 : vctmax >= 0 ? +1 : 0;
    //  else
    //    throw new NotImplementedException(source.ToString());

    //  //return source switch
    //  //{
    //  //  IntervalNotation.Closed => vctmin < 0 ? -1 : vctmax > 0 ? +1 : 0,
    //  //  IntervalNotation.Open => vctmin <= 0 ? -1 : vctmax >= 0 ? +1 : 0,
    //  //  IntervalNotation.HalfOpenMinimum => vctmin <= 0 ? -1 : vctmax > 0 ? +1 : 0,
    //  //  IntervalNotation.HalfOpenMaximum => vctmin < 0 ? -1 : vctmax >= 0 ? +1 : 0,
    //  //  _ => throw new NotImplementedException(),
    //  //};
    //}

    //public static TNumber IntervalMaximumRemainder<TNumber>(this IntervalNotation source, TNumber value, TNumber minValue, TNumber maxValue)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  switch (source)
    //  {
    //    case IntervalNotation.Closed:
    //    case IntervalNotation.HalfOpenMinimum:
    //      return (value > maxValue) ? minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One)) : value;
    //    case IntervalNotation.Open:
    //    case IntervalNotation.HalfOpenMaximum:
    //      return (value >= maxValue) ? minValue + ((value - minValue) % (maxValue - minValue)) : value;
    //    default:
    //      throw new System.NotImplementedException();
    //  }
    //}

    //public static TNumber IntervalMinimumRemainder<TNumber>(this IntervalNotation source, TNumber value, TNumber minValue, TNumber maxValue)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  switch (source)
    //  {
    //    case IntervalNotation.Closed:
    //    case IntervalNotation.HalfOpenMaximum:
    //      return (value < minValue) ? maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One)) : value;
    //    case IntervalNotation.Open:
    //    case IntervalNotation.HalfOpenMinimum:
    //      return (value <= minValue) ? minValue + TNumber.Abs((minValue - value).ReverseRemainderNonZero(maxValue - minValue, out TNumber _)) : value;
    //    default:
    //      throw new System.NotImplementedException();
    //  }
    //}

    ///// <summary>
    ///// <para>Determines whether the interval <paramref name="minValue"/>..<paramref name="maxValue"/> is a degenerate interval, i.e. the interval consists of only a single value.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <returns></returns>
    ///// <remarks>If <paramref name="source"/> is <see cref="IntervalNotation.Closed"/> and <paramref name="minValue"/> equals <paramref name="maxValue"/>, then it is a degenerate interval.</remarks>
    //public static bool IsDegenerateInterval<T>(this IntervalNotation source, T minValue, T maxValue)
    //  where T : System.IComparable<T>
    //{
    //  source.AssertValidInterval(minValue, maxValue);

    //  return source == IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0;
    //}

    ///// <summary>
    ///// <para>Determines whether the interval <paramref name="minValue"/>..<paramref name="maxValue"/> represents the empty set.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <returns></returns>
    ///// <remarks>If <paramref name="minValue"/> equals <paramref name="maxValue"/> and <paramref name="source"/> notation is not closed, or if <paramref name="minValue"/> is greater than <paramref name="maxValue"/>, then the interval is the empty set.</remarks>
    //public static bool IsEmptySet<T>(this IntervalNotation source, T minValue, T maxValue)
    //  where T : System.IComparable<T>
    //{
    //  source.AssertValidInterval(minValue, maxValue);

    //  return (source != IntervalNotation.Closed && minValue.CompareTo(maxValue) == 0 && maxValue.CompareTo(minValue) == 0) // If minValue equals maxValue, all but the closed notation represents the empty set.
    //  || (minValue.CompareTo(maxValue) > 0 || maxValue.CompareTo(minValue) < 0); // If minValue is greater than maxValue, all four notations are usually taken to represent the empty set.
    //}

    ///// <summary>
    ///// <para>Determines whether the interval is valid.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <returns></returns>
    ///// <exception cref="NotImplementedException"></exception>
    //public static bool IsValidInterval<T>(this IntervalNotation source, T minValue, T maxValue)
    //  where T : System.IComparable<T>
    //  => Interval<T>.IsValid(source, minValue, maxValue);
    //{
    //  if (source == IntervalNotation.Closed)
    //    return minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) >= 0;
    //  else if (source == IntervalNotation.HalfRightOpen)
    //    return minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) > 0;
    //  else if (source == IntervalNotation.HalfLeftOpen)
    //    return minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) >= 0;
    //  else if (source == IntervalNotation.Open)
    //    return minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) > 0;
    //  else
    //    throw new NotImplementedException(source.ToString());
    //}

    //=> source switch
    //{
    //  IntervalNotation.Closed => minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) >= 0,
    //  IntervalNotation.Open => minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) > 0,
    //  IntervalNotation.HalfOpenMinimum => minValue.CompareTo(maxValue) < 0 && maxValue.CompareTo(minValue) >= 0,
    //  IntervalNotation.HalfOpenMaximum => minValue.CompareTo(maxValue) <= 0 && maxValue.CompareTo(minValue) > 0,
    //  _ => throw new NotImplementedException(),
    //};

    ///// <summary>
    ///// <para>Determines whether the <paramref name="value"/> is a member of the interval <paramref name="minValue"/>..<paramref name="maxValue"/> based on the <paramref name="source"/> <see cref="IntervalNotation"/>.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source">The interval notation to apply when determining membership of the interval set.</param>
    ///// <param name="value">The value to verify.</param>
    ///// <param name="minValue">The lower bound of the interval set.</param>
    ///// <param name="maxValue">The upper bound of the interval set.</param>
    ///// <returns>Whether the <paramref name="value"/> is a member of the interval set <paramref name="minValue"/>..<paramref name="maxValue"/> using the source <see cref="IntervalNotation"/>.</returns>
    /////// <exception cref="System.ArgumentOutOfRangeException"/>
    /////// <exception cref="System.NotImplementedException"/>
    //public static bool IsIntervalMember<T>(this IntervalNotation source, T value, T minValue, T maxValue)
    //  where T : System.IComparable<T>
    //  => Interval<T>.IsMember(value, minValue, maxValue, source);
    //{
    //  try
    //  {
    //    return Interval<T>.CompareMemberToInterval(value, minValue, maxValue, source) == 0;
    //  }
    //  catch { }

    //  return false;
    //}

    #region Extent Interval

    ///// <summary>
    ///// <para>Gets a new min/max interval (depending on type <typeparamref name="T"/>) using the specified <see cref="IntervalNotation"/>, <paramref name="minValue"/>, <paramref name="maxValue"/> and <paramref name="magnitude"/>.</para>
    ///// <para>If <typeparamref name="T"/> is an integer, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + 1) and (<paramref name="maxValue"/> - 1).</para>
    ///// <para>If <typeparamref name="T"/> is a floating point value, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + epsilon) and (<paramref name="maxValue"/> - epsilon). In this context epsilon is a value that makes the original value and the new value not equal.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <param name="magnitude">This is the magnitude of the extent to aim for. It is essentially a multiplier of the minimum possible extent (when any open interval is chosen).</param>
    ///// <returns></returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    ///// <exception cref="NotImplementedException"></exception>
    //public static (T Minimum, T Maximum) GetExtentInterval<T>(this IntervalNotation source, T minValue, T maxValue, int magnitude = 1)
    //  where T : System.Numerics.INumber<T>
    //{
    //  magnitude.AssertNonNegativeNumber(nameof(magnitude));

    //  source.AssertValidInterval(minValue, maxValue);

    //  while (--magnitude >= 0)
    //  {
    //    if (source == IntervalNotation.Closed)
    //      return (minValue, maxValue); // Exit early since this will not change regardless of magnitude.
    //    else if (source == IntervalNotation.HalfRightOpen)
    //      maxValue = maxValue.GetInfimum();
    //    else if (source == IntervalNotation.HalfLeftOpen)
    //      minValue = minValue.GetSupremum();
    //    else if (source == IntervalNotation.Open)
    //      (minValue, maxValue) = (minValue.GetSupremum(), maxValue.GetInfimum());
    //    else
    //      throw new NotImplementedException(source.ToString());
    //  }

    //  //while (--magnitude >= 0)
    //  //  (minValue, maxValue) = source switch
    //  //  {
    //  //    IntervalNotation.Closed => (minValue, maxValue),
    //  //    IntervalNotation.Open => (minValue.GetSupremum(), maxValue.GetInfimum()),
    //  //    IntervalNotation.HalfOpenMinimum => (minValue.GetSupremum(), maxValue),
    //  //    IntervalNotation.HalfOpenMaximum => (minValue, maxValue.GetInfimum()),
    //  //    _ => throw new NotImplementedException(),
    //  //  };

    //  return IntervalNotation.Closed.AssertValidInterval(minValue, maxValue, "minExtent/maxExtent");
    //}

    ///// <summary>
    ///// <para>Attempts to get a new min/max interval (depending on type <typeparamref name="T"/>) using the specified <see cref="IntervalNotation"/>, <paramref name="minValue"/>, <paramref name="maxValue"/> and <paramref name="magnitude"/>.</para>
    ///// <para>If <typeparamref name="T"/> is an integer, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + 1 * 1) and (<paramref name="maxValue"/> - 1 * 1).</para>
    ///// <para>If <typeparamref name="T"/> is a floating point value, magnitude = 1 and notation = <see cref="IntervalNotation.Open"/>, the new values are (<paramref name="minValue"/> + epsilon * 1) and (<paramref name="maxValue"/> - epsilon * 1). In this context epsilon is a value that makes the original value and the new value not equal.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="intervalNotation"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <param name="magnitude">This is the magnitude of the extent to aim for. It is essentially a multiplier of the minimum possible extent (when any open interval is chosen).</param>
    ///// <param name="minExtent"></param>
    ///// <param name="maxExtent"></param>
    ///// <returns></returns>
    //public static bool TryGetExtentInterval<T>(this IntervalNotation source, T minValue, T maxValue, int magnitude, out T minExtent, out T maxExtent)
    //  where T : System.Numerics.INumber<T>
    //{
    //  try
    //  {
    //    (minExtent, maxExtent) = source.GetExtentInterval(minValue, maxValue, magnitude);
    //    return true;
    //  }
    //  catch { }

    //  minExtent = default!;
    //  maxExtent = default!;
    //  return false;
    //}

    #endregion // Extent Interval

    ///// <summary>
    ///// <para>Gets a new min/max interval, using the <paramref name="intervalNotation"/> <see cref="IntervalNotation"/> where <paramref name="minMargin"/>/<paramref name="maxMargin"/> are absolute margins applied to <paramref name="minValue"/>/<paramref name="maxValue"/> respectively.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="intervalNotation"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <param name="minMargin"></param>
    ///// <param name="maxMargin"></param>
    ///// <returns></returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    ///// <exception cref="NotImplementedException"></exception>
    //public static (T Minimum, T Maximum) GetMarginInterval<T>(this IntervalNotation source, T minValue, T maxValue, T minMargin, T maxMargin)
    //  where T : System.Numerics.INumber<T>
    //{
    //  if (T.IsNegative(minMargin)) throw new System.ArgumentOutOfRangeException(nameof(minMargin));
    //  if (T.IsNegative(maxMargin)) throw new System.ArgumentOutOfRangeException(nameof(maxMargin));

    //  intervalNotation.AssertValidInterval(minValue, maxValue);

    //  var paramName = "minMargin/maxMargin";

    //  if (intervalNotation == IntervalNotation.Closed)
    //    return (minValue, maxValue); // No need to re-assert.
    //  else if (intervalNotation == IntervalNotation.Open)
    //    return IntervalNotation.Closed.AssertValidInterval(minValue + minMargin, maxValue - maxMargin, paramName);
    //  else if (intervalNotation == IntervalNotation.HalfLeftOpen)
    //    return IntervalNotation.Closed.AssertValidInterval(minValue + minMargin, maxValue, paramName);
    //  else if (intervalNotation == IntervalNotation.HalfRightOpen)
    //    return IntervalNotation.Closed.AssertValidInterval(minValue, maxValue - maxMargin, paramName);
    //  else
    //    throw new NotImplementedException(intervalNotation.ToString());

    //  //var (min, max) = source switch
    //  //{
    //  //  IntervalNotation.Closed => (minValue, maxValue),
    //  //  IntervalNotation.Open => (minValue + minMargin, maxValue - maxMargin),
    //  //  IntervalNotation.HalfOpenMinimum => (minValue + minMargin, maxValue),
    //  //  IntervalNotation.HalfOpenMaximum => (minValue, maxValue - maxMargin),
    //  //  _ => throw new NotImplementedException(),
    //  //};

    //  //IntervalNotation.Closed.AssertValidInterval(min, max, "minMargin/maxMargin");

    //  //return (min, max);
    //}

    ///// <summary>
    ///// <para>Attempts to get a new min/max interval, using the <paramref name="source"/> <see cref="IntervalNotation"/> where <paramref name="minMargin"/>/<paramref name="maxMargin"/> are absolute margins applied to <paramref name="minValue"/>/<paramref name="maxValue"/> respectively.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <param name="minMargin"></param>
    ///// <param name="maxMargin"></param>
    ///// <param name="minimum"></param>
    ///// <param name="maximum"></param>
    ///// <returns></returns>
    //public static bool TryGetMarginInterval<T>(this IntervalNotation source, T minValue, T maxValue, T minMargin, T maxMargin, out T minimum, out T maximum)
    //  where T : System.Numerics.INumber<T>
    //{
    //  try
    //  {
    //    (minimum, maximum) = source.GetMarginInterval(minValue, maxValue, minMargin, maxMargin);
    //    return true;
    //  }
    //  catch { }

    //  minimum = default!;
    //  maximum = default!;
    //  return false;
    //}

    public static IntervalNotation InvertNotation(this IntervalNotation source)
    {
      if (source == IntervalNotation.Closed)
        return IntervalNotation.Open;
      else if (source == IntervalNotation.Open)
        return IntervalNotation.Closed;
      else if (source == IntervalNotation.HalfLeftOpen)
        return IntervalNotation.HalfRightOpen;
      else if (source == IntervalNotation.HalfRightOpen)
        return IntervalNotation.HalfLeftOpen;
      else
        throw new NotImplementedException(source.ToString());
    }

    /// <summary>
    /// <para>Loop back-and-forth between <paramref name="start"/> and <paramref name="stop"/> in <paramref name="step"/>s for a number of <paramref name="iterations"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The interval-notation to apply for the first and last number in the loop.</param>
    /// <param name="start">The first number in the loop (which may not appear in the output depending on the interval-notation).</param>
    /// <param name="stop">The last number in the loop (which may not appear in the output depending on the interval-notation).</param>
    /// <param name="step">The size to increase the counter by for each loop.</param>
    /// <param name="iterations">The number of times to repeat the entire loop.</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T> LoopBackAndForth<T>(this IntervalNotation source, T start, T stop, int iterations = 1)
      where T : System.Numerics.IBinaryInteger<T>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(iterations, nameof(iterations));

      while (iterations-- > 0)
      {
        foreach (var item in source.LoopBetween(start, stop, 1)) yield return item;
        foreach (var item in source.InvertNotation().LoopBetween(stop, start, 1)) yield return item;
      }
    }

    /// <summary>
    /// <para>Loop between <paramref name="start"/> and <paramref name="stop"/> in <paramref name="step"/>s for a number of <paramref name="iterations"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The interval-notation to apply for the first and last number in the loop.</param>
    /// <param name="start">The first number in the loop (which may not appear in the output depending on the interval-notation).</param>
    /// <param name="stop">The last number in the loop (which may not appear in the output depending on the interval-notation).</param>
    /// <param name="step">The size to increase the counter by for each loop.</param>
    /// <param name="iterations">The number of times to repeat the entire loop.</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T> LoopBetween<T>(this IntervalNotation source, T start, T stop, int iterations = 1)
      where T : System.Numerics.IBinaryInteger<T>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(iterations, nameof(iterations));

      var step = (stop - start).Sign();

      while (iterations-- > 0)
      {
        if (source is IntervalNotation.Closed or IntervalNotation.HalfRightOpen)
          yield return start;

        for (var index = start + step; index != stop; index += step)
          yield return index;

        if (source is IntervalNotation.Closed or IntervalNotation.HalfLeftOpen)
          yield return stop;
      }
    }

    /// <summary>
    /// <para>Loop the range from <paramref name="start"/> and <paramref name="count"/> numbers in <paramref name="step"/>s for a number of <paramref name="iterations"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The interval-notation to apply for the first and last number in the loop.</param>
    /// <param name="start">The first number in the loop (which may not appear in the output depending on the interval-notation).</param>
    /// <param name="count">The count of numbers the loop should produce.</param>
    /// <param name="step">The size to increase the counter by for each loop.</param>
    /// <param name="iterations">The number of times to repeat the entire loop.</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T> LoopRange<T>(this IntervalNotation source, T start, T count, int iterations = 1)
      where T : System.Numerics.IBinaryInteger<T>
      => source.LoopBetween(start, start + count - T.CopySign(T.One, count), iterations);

    /// <summary>
    /// <para>Gets a char representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) on the left hand side.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static char ToIntervalNotationCharLeft(this IntervalNotation source)
    {
      if (source == IntervalNotation.Closed || source == IntervalNotation.HalfRightOpen)
        return '[';
      else if (source == IntervalNotation.Open || source == IntervalNotation.HalfLeftOpen)
        return '(';
      else
        throw new NotImplementedException();
    }

    /// <summary>
    /// <para>Gets a char representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) on the right hand side.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static char ToIntervalNotationCharRight(this IntervalNotation source)
    {
      if ((source == IntervalNotation.Closed || source == IntervalNotation.HalfLeftOpen))
        return ']';
      else if (source == IntervalNotation.Open || source == IntervalNotation.HalfRightOpen)
        return ')';
      else
        throw new NotImplementedException();
    }

    ///// <summary>
    ///// <para>Creates a string representing the <see cref="IntervalNotation"/> (specified by <paramref name="source"/>) of the interval <paramref name="minValue"/>..<paramref name="maxValue"/>.</para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <returns></returns>
    ///// <exception cref="NotImplementedException"></exception>
    //public static string ToIntervalNotationString<T>(this IntervalNotation source, T minValue, T maxValue, string? format = null, System.IFormatProvider? provider = null)
    //{
    //  format = $"{{0{(format is null ? string.Empty : $":{format}")}}}";

    //  return $"{source.ToIntervalNotationCharLeft()}{string.Format(provider, format, minValue)}, {string.Format(provider, format, maxValue)}{source.ToIntervalNotationCharRight()}";
    //}

    //public static TNumber WrapAround<TNumber>(this IntervalNotation source, TNumber value, TNumber minValue, TNumber maxValue)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  (minValue, maxValue) = source.GetExtentInterval(minValue, maxValue);

    //  var cmp = Interval<TNumber>.CompareMemberToInterval(value, minValue, maxValue, source);

    //  var addon = value != minValue && value != maxValue ? TNumber.One : TNumber.Zero;

    //  if (cmp > 0)
    //    return minValue + (value - maxValue - addon) % (maxValue - minValue + addon);
    //  else if (cmp < 0)
    //    return maxValue - (minValue - value - addon) % (maxValue - minValue + addon);
    //  else
    //    return value;
    //}
  }
}
