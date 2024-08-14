namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</para>
    /// <example>
    /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, RoundingMode, int, int)"/> method)</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="significantDigits"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TSelf RoundByPrecision<TSelf>(this TSelf value, RoundingMode mode, int significantDigits, int radix = 10)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      var scalar = TSelf.Pow(TSelf.CreateChecked(radix), TSelf.CreateChecked(significantDigits));

      return Round(value * scalar, mode) / scalar;
    }
  }
}
