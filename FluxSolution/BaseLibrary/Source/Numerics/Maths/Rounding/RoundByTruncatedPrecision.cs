namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Rounds <paramref name="value"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
    /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
    /// <example>
    /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, RoundingMode, int, int)"/> method)</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="significantDigits"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf RoundByTruncatedPrecision<TSelf>(this TSelf value, RoundingMode mode, int significantDigits, int radix = 10)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      if (significantDigits < 0) throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

      var scalar = TSelf.Pow(TSelf.CreateChecked(radix), TSelf.CreateChecked(significantDigits + 1));

      return RoundByPrecision(TSelf.Truncate(value * scalar) / scalar, mode, significantDigits, radix);
    }
  }
}
