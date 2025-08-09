namespace Flux
{
  public static partial class NumberFast
  {
    /// <summary>
    /// <para>Attempts to compute the <paramref name="number"/> raised to the given <paramref name="power"/> (exponent) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="pow"/> (double) is returned as an out parameter.</para>
    /// <para>Uses the built-in <see cref="double.Pow(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative pow.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TPower"></typeparam>
    /// <param name="number">The number which is raised to the <paramref name="power"/>.</param>
    /// <param name="power">The power for which the <paramref name="number"/> is raised.</param>
    /// <param name="mode">The integer rounding strategy to use.</param>
    /// <param name="pow">The actual floating-point pow as an out parameter.</param>
    /// <returns>The integer pow.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber FastIntegerPow<TNumber, TPower>(this TNumber number, TPower power, UniversalRounding mode, out double pow)
      where TNumber : System.Numerics.INumber<TNumber>
      where TPower : System.Numerics.INumber<TPower>
      => checked(TNumber.CopySign(TNumber.CreateChecked((pow = double.Pow(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(power))).RoundUniversal(mode)), number));
  }
}
