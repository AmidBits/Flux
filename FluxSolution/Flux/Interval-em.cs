namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Creates a new sequence of values by iterating over the <paramref name="source"/> <see cref="Interval{TSelf}"/> using the <paramref name="constraint"/> .</para>
    /// </summary>
    /// <param name="source">The <see cref="Interval{TSelf}"/>.</param>
    /// <param name="step">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="order">Iterate the range in either <see cref="SortOrder"/> order.</param>
    /// <param name="notation">Specified by <see cref="IntervalNotation"/>.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> Range<TSelf>(this Interval<TSelf> source, TSelf step, IntervalNotation notation = IntervalNotation.Closed)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var (minMargin, maxMargin) = notation.GetMarginInterval(source.MinValue, source.MaxValue, TSelf.Abs(step), TSelf.Abs(step));

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
    /// <para>Create a new <see cref="Interval{T}"/> with the <see cref="Interval{T}.MinValue"/>/<see cref="Interval{T}.MaxValue"/> set to the <paramref name="notation"/> extents (+- 1 for integers, +- EPSILON for floating point) of <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="notation"></param>
    /// <returns></returns>
    public static Interval<TSelf> ToExtentInterval<TSelf>(this Interval<TSelf> source, IntervalNotation notation)
      where TSelf : System.Numerics.INumber<TSelf>
      => (Interval<TSelf>)notation.GetExtentInterval(source.MinValue, source.MaxValue);

    /// <summary>
    /// <para>Create a new <see cref="Interval{T}"/> with the <see cref="Interval{T}.MinValue"/>/<see cref="Interval{T}.MaxValue"/> set to the <paramref name="notation"/> margins (<paramref name="minMargin"/>/<paramref name="maxMargin"/>) of <paramref name="source"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="minMargin"></param>
    /// <param name="maxMargin"></param>
    /// <param name="notation"></param>
    /// <returns></returns>
    public static Interval<TSelf> ToMarginInterval<TSelf>(this Interval<TSelf> source, TSelf minMargin, TSelf maxMargin, IntervalNotation notation)
      where TSelf : System.Numerics.INumber<TSelf>
      => (Interval<TSelf>)notation.GetMarginInterval(source.MinValue, source.MaxValue, minMargin, maxMargin);
  }
}
