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
      => value < minValue
      ? maxValue - (minValue - value) % (maxValue - minValue)
      : value > maxValue
      ? minValue + (value - minValue) % (maxValue - minValue)
      : value;

    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="value"/> around to the half-open (max/right) interval [<paramref name="minValue"/>, <paramref name="maxValue"/>), until the <paramref name="value"/> is within the half-open (max/right) interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAroundHalfOpenMax<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => WrapAround(value, minValue, maxValue) % maxValue;
  }
}
