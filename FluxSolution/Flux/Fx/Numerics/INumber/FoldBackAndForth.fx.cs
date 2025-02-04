namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Folds an out-of-bound <paramref name="number"/> (back and forth) over the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="number"/> is within the closed interval.</para>
    /// </summary>
    public static TNumber FoldBackAndForth<TNumber>(this TNumber number, TNumber minValue, TNumber maxValue)
      where TNumber : System.Numerics.INumber<TNumber>
      => (number > maxValue)
      ? TNumber.IsEvenInteger(TruncRem(number - maxValue, maxValue - minValue, out var remHi)) ? maxValue - remHi : minValue + remHi
      : (number < minValue)
      ? TNumber.IsEvenInteger(TruncRem(minValue - number, maxValue - minValue, out var remLo)) ? minValue + remLo : maxValue - remLo
      : number;
  }
}
