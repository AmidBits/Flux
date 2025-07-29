namespace Flux
{
  public static partial class Intervals
  {
    public static Interval<T> CreateInterval<T>(T minValue, T maxValue)
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
      magnitude.AssertNonNegativeNumber(nameof(magnitude));

      var (minValue, maxValue) = Interval<T>.AssertValid(source.MinValue, source.MaxValue, intervalNotation);

      while (--magnitude >= 0)
      {
        if (intervalNotation == IntervalNotation.Closed)
          return source; // Exit early since this will not change regardless of magnitude.
        else if (intervalNotation == IntervalNotation.HalfRightOpen)
          maxValue = maxValue.GetInfimum();
        else if (intervalNotation == IntervalNotation.HalfLeftOpen)
          minValue = minValue.GetSupremum();
        else if (intervalNotation == IntervalNotation.Open)
          (minValue, maxValue) = (minValue.GetSupremum(), maxValue.GetInfimum());
        else
          throw new NotImplementedException(intervalNotation.ToString());
      }

      Interval<T>.AssertValid(minValue, maxValue, IntervalNotation.Closed, "minExtent/maxExtent");

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
        extent = source.GetExtent(intervalNotation, magnitude);
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
    public static Interval<T> GetMargin<T>(this Interval<T> source, T minMargin, T maxMargin, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.INumber<T>
    {
      if (T.IsNegative(minMargin)) throw new System.ArgumentOutOfRangeException(nameof(minMargin));
      if (T.IsNegative(maxMargin)) throw new System.ArgumentOutOfRangeException(nameof(maxMargin));

      var (minValue, maxValue) = Interval<T>.AssertValid(source.MinValue, source.MaxValue);

      var paramName = "minMargin/maxMargin";

      if (intervalNotation == IntervalNotation.Closed)
        return source; // No need to re-assert.
      else if (intervalNotation == IntervalNotation.Open)
        (minValue, maxValue) = Interval<T>.AssertValid(minValue + minMargin, maxValue - maxMargin, IntervalNotation.Closed, paramName);
      else if (intervalNotation == IntervalNotation.HalfLeftOpen)
        (minValue, maxValue) = Interval<T>.AssertValid(minValue + minMargin, maxValue, IntervalNotation.Closed, paramName);
      else if (intervalNotation == IntervalNotation.HalfRightOpen)
        (minValue, maxValue) = Interval<T>.AssertValid(minValue, maxValue - maxMargin, IntervalNotation.Closed, paramName);
      else
        throw new NotImplementedException(intervalNotation.ToString());

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
    public static bool TryGetMargin<T>(this Interval<T> source, T minMargin, T maxMargin, out Interval<T> margin, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.INumber<T>
    {
      try
      {
        margin = source.GetMargin(minMargin, maxMargin, intervalNotation);
        return true;
      }
      catch { }

      margin = default!;
      return false;
    }

    #endregion // GetMargin

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
      var (minMargin, maxMargin) = source.GetMargin(TSelf.Abs(step), TSelf.Abs(step), intervalNotation);

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
    /// <para>Calculates the offset and length of an <see cref="Interval{T}"/>. The offset is the same as <see cref="Interval{T}.MinValue"/> of <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (T Offset, T Length) GetOffsetAndLength<T>(this Interval<T> source, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.IBinaryInteger<T>
      {
        var index = int.CreateChecked(source.MinValue);
        var length = int.CreateChecked(source.MaxValue - source.MinValue);
        
        if(intervalNotation is IntervalNotation.HalfOpenLeft or IntervalNotation.Open)
          index++;
        
        if(intervalNotation == IntervalNotation.Closed)
          length++;
        else if(intervalNotation == IntervalNotation.Open)
          length--;

        return (index, length);
      }
      //=> (source.MinValue, source.MaxValue - source.MinValue + T.One);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Range ToRange<T>(this Interval<T> source, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.IBinaryInteger<T>
      {
        var rangeStartIndex = int.CreateChecked(source.MinValue);
        var rangeEndIndex = int.CreateChecked(source.MaxValue);
        
        if(intervalNotation is IntervalNotation.HalfOpenLeft or IntervalNotation.Open)
          rangeStartIndex++;
        
        if(intervalNotation is IntervalNotation.HalfOpenRight or IntervalNotation.Open)
          rangeEndIndex--;

        return new(rangeStartIndex, rangeEndIndex);
      }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Slice ToSlice<T>(this Interval<T> source, IntervalNotation intervalNotation = IntervalNotation.Closed)
      where T : System.Numerics.IBinaryInteger<T>
      {
        var sliceIndex = int.CreateChecked(source.MinValue);
        var sliceLength = int.CreateChecked(source.MaxValue - source.MinValue);
        
        if(intervalNotation is IntervalNotation.HalfOpenLeft or IntervalNotation.Open)
          sliceIndex++;
        
        if(intervalNotation == IntervalNotation.Closed)
          sliceLength++;
        else if(intervalNotation == IntervalNotation.Open)
          sliceLength--;

        return new(sliceIndex, sliceLength);
      }

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

      var cmp = Interval<T>.CompareMemberToInterval(value, minValue, maxValue, intervalNotation);

      var addon = value != minValue && value != maxValue ? T.One : T.Zero;

      if (cmp > 0)
        return minValue + (value - maxValue - addon) % (maxValue - minValue + addon);
      else if (cmp < 0)
        return maxValue - (minValue - value - addon) % (maxValue - minValue + addon);
      else
        return value;
    }
  }
}
