namespace Flux
{
  public static partial class GenericMath
  {

    //public static TNumber WrapAround2<TNumber>(this IntervalNotation source, TNumber value, TNumber minValue, TNumber maxValue)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  if (value >= maxValue)
    //  {
    //    var range = maxValue - minValue;
    //    var above = value - maxValue;
    //  }

    //  if (value <= minValue)
    //  {
    //    var range = maxValue - minValue;
    //    var below = minValue - value;

    //  }
    //}

    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="value"/> around to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAroundClosed<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value > maxValue)
        return minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One));

      if (value < minValue)
        return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One));

      return value;
    }

    public static TNumber WrapAroundOpen<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value >= maxValue)
        return minValue + ((value - maxValue) % (maxValue - minValue));

      if (value <= minValue)
        return maxValue - ((minValue - value) % (maxValue - minValue));

      return value;
    }

    public static TNumber WrapAroundHalfOpenMaximum<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value >= maxValue)
        return minValue + ((value - maxValue) % (maxValue - minValue));

      if (value < minValue)
        return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One));

      return value;
    }

    public static TNumber WrapAroundHalfOpenMinimum<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value > maxValue)
        return minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One));

      if (value <= minValue)
        return maxValue - ((minValue - value) % (maxValue - minValue));

      return value;
    }

    ///// <summary>
    ///// <para>Wraps an out-of-bound <paramref name="value"/> around to the half-open-<paramref name="maxValue"/> interval [<paramref name="minValue"/>, <paramref name="maxValue"/>) until within the interval.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <returns></returns>
    //public static TNumber WrapAroundHalfOpenMax<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  if (value >= maxValue)
    //    return minValue + ((value - maxValue) % (maxValue - minValue));

    //  if (value < minValue)
    //    return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue)) - TNumber.One;

    //  return value;
    //}

    ///// <summary>
    ///// <para>Wraps an out-of-bound <paramref name="value"/> around to the half-open-<paramref name="minValue"/> interval (<paramref name="minValue"/>, <paramref name="maxValue"/>] until within the interval.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="minValue"></param>
    ///// <param name="maxValue"></param>
    ///// <returns></returns>
    //public static TNumber WrapAroundHalfOpenMin<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  if (value > maxValue)
    //    return minValue + (value - minValue).RemainderNonZero(maxValue - minValue, out TNumber _);

    //  if (value <= minValue)
    //    //        return minValue + TNumber.Abs((minValue - value).ReverseRemainderNonZero(maxValue - minValue, out TNumber _));
    //    return maxValue - ((minValue - value) % (maxValue - minValue));

    //  return value;
    //}
  }
}
