namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rounds <paramref name="value"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
    /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
    /// <example>
    /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, RoundingMode, int, int)"/> method)</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="significantDigits"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue RoundByTruncatedPrecision<TValue, TRadix>(this TValue value, RoundingMode mode, int significantDigits, TRadix radix)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IPowerFunctions<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (significantDigits < 0) throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

      var scalar = TValue.Pow(TValue.CreateChecked(radix), TValue.CreateChecked(significantDigits + 1));

      return RoundByPrecision(TValue.Truncate(value * scalar) / scalar, mode, significantDigits, radix);
    }
  }
}
