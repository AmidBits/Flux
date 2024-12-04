namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="number"/> to the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="number"/> is within the closed interval.</para>
    /// </summary>
    public static TNumber Wrap<TNumber>(this TNumber number, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => number < minValue
      ? maxValue - (minValue - number) % (maxValue - minValue)
      : number > maxValue
      ? minValue + (number - minValue) % (maxValue - minValue)
      : number;

    /// <summary>
    /// <para>Wraps an out-of-bound <paramref name="number"/> to the half-open interval [<paramref name="minValue"/>, <paramref name="maxValue"/>), until the <paramref name="number"/> is within the half-open interval.</para>
    /// </summary>
    public static TNumber WrapOpenEnd<TNumber>(this TNumber number, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => Wrap(number, minValue, maxValue) % maxValue;
  }
}
