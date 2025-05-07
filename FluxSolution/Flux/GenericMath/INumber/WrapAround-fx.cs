namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="value"/> around to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAround<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value > maxValue)
        return minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One));

      if (value < minValue)
        return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One));

      return value;
    }

    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="value"/> around to the half-open-<paramref name="maxValue"/> interval [<paramref name="minValue"/>, <paramref name="maxValue"/>) until within the interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAroundHalfOpenMax<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value >= maxValue)
        return minValue + ((value - minValue) % (maxValue - minValue));

      if (value < minValue)
        return maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue)) - TNumber.One;

      return value;
    }

    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="value"/> around to the half-open-<paramref name="minValue"/> interval (<paramref name="minValue"/>, <paramref name="maxValue"/>] until within the interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAroundHalfOpenMin<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value > maxValue)
        return minValue + (value - minValue).RemainderNonZero(maxValue - minValue, out TNumber _);

      if (value <= minValue)
        return minValue + TNumber.Abs((minValue - value).ReverseRemainderNonZero(maxValue - minValue, out TNumber _));

      return value;
    }
  }
}
