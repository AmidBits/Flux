namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</para>
    /// <example>
    /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, RoundingMode, int, int)"/> method)</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="significantDigits"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TValue RoundByPrecision<TValue>(this TValue value, RoundingMode mode, int significantDigits, int radix = 10)
      where TValue : System.Numerics.IFloatingPointIeee754<TValue>
    {
      var scalar = TValue.Pow(TValue.CreateChecked(radix), TValue.CreateChecked(significantDigits));

      return Round(value * scalar, mode) / scalar;
    }
  }
}
