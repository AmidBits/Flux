namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Attempts to compute the <paramref name="number"/> raised to the given <paramref name="exponent"/> (power) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="pow"/> (double) is returned as an out parameter.</para>
    /// <para>Uses the built-in <see cref="double.Pow(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative pow.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TPower"></typeparam>
    /// <param name="number"></param>
    /// <param name="exponent"></param>
    /// <param name="mode"></param>
    /// <param name="pow"></param>
    /// <returns>The resulting integer-pow.</returns>
    public static TNumber FastIntegerPow<TNumber, TPower>(this TNumber number, TPower exponent, UniversalRounding mode, out double pow)
      where TNumber : System.Numerics.INumber<TNumber>
      where TPower : System.Numerics.INumber<TPower>
    {
      checked
      {
        pow = double.Pow(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(exponent));

        return TNumber.CopySign(TNumber.CreateChecked(pow.RoundUniversal(mode)), number);
      }
    }
  }
}
