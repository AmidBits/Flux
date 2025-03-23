namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="number"/> around to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="number"/> is within the closed interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAround<TNumber>(this TNumber number, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => number < minValue
      ? maxValue - (minValue - number) % (maxValue - minValue)
      : number > maxValue
      ? minValue + (number - minValue) % (maxValue - minValue)
      : number;

    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="number"/> around to the half-open (max/right) interval [<paramref name="minValue"/>, <paramref name="maxValue"/>), until the <paramref name="number"/> is within the half-open (max/right) interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber WrapAroundHalfOpenMax<TNumber>(this TNumber number, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => WrapAround(number, minValue, maxValue) % maxValue;
  }
}
