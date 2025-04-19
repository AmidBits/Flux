namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Folds an out-of-bound <paramref name="value"/> (back and forth) over the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber FoldBackAndForth<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => (value > maxValue)
      ? TruncRem(value - maxValue, maxValue - minValue) is var (tqHi, remHi) && TNumber.IsEvenInteger(tqHi) ? maxValue - remHi : minValue + remHi
      : (value < minValue)
      ? TruncRem(minValue - value, maxValue - minValue) is var (tqLo, remLo) && TNumber.IsEvenInteger(tqLo) ? minValue + remLo : maxValue - remLo
      : value;
  }
}
