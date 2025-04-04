namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Folds an out-of-bound <paramref name="number"/> (back and forth) over the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="number"/> is within the closed interval.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static TNumber FoldBackAndForth<TNumber>(this TNumber number, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => (number > maxValue)
      ? TruncRem(number - maxValue, maxValue - minValue) is var (tqHi, remHi) && TNumber.IsEvenInteger(tqHi) ? maxValue - remHi : minValue + remHi
      : (number < minValue)
      ? TruncRem(minValue - number, maxValue - minValue) is var (tqLo, remLo) && TNumber.IsEvenInteger(tqLo) ? minValue + remLo : maxValue - remLo
      : number;
  }
}
