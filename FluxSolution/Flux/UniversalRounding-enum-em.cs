namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Returns the <paramref name="value"/> rounded according to the strategy <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue RoundUniversal<TValue>(this TValue value, UniversalRounding mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.TryRoundWhole((WholeRounding)mode, out var whole)
      ? whole
      : value.TryRoundHalf((HalfRounding)mode, out var half)
      ? half
      : throw new System.ArgumentOutOfRangeException(nameof(mode));

    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</para>
    /// <example>
    /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, UniversalRounding, int, int)"/> method)</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="significantDigits"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TValue RoundUniversalByPrecision<TValue, TRadix>(this TValue value, UniversalRounding mode, int significantDigits, TRadix radix)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IPowerFunctions<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var scalar = TValue.Pow(TValue.CreateChecked(radix), TValue.CreateChecked(significantDigits));

      return (value * scalar).RoundUniversal(mode) / scalar;
    }

    /// <summary>
    /// <para>Rounds <paramref name="value"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
    /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
    /// <example>
    /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, UniversalRounding, int, int)"/> method)</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="significantDigits"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue RoundUniversalByTruncatedPrecision<TValue, TRadix>(this TValue value, UniversalRounding mode, int significantDigits, TRadix radix)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IPowerFunctions<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

      var scalar = TValue.Pow(TValue.CreateChecked(radix), TValue.CreateChecked(significantDigits + 1));

      return (TValue.Truncate(value * scalar) / scalar).RoundUniversalByPrecision(mode, significantDigits, radix);
    }

    /// <summary>Returns the <paramref name="value"/> (of floating-point type <typeparamref name="TValue"/>) as a <see cref="System.Numerics.BigInteger"/> using the rounding <paramref name="mode"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TValue>(this TValue value, UniversalRounding mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => System.Numerics.BigInteger.CreateChecked(value.RoundUniversal(mode));

    //mode switch
    //{
    //  UniversalRounding.WholeToEven => throw new NotImplementedException(),
    //  UniversalRounding.WholeAwayFromZero => Envelop(value),
    //  UniversalRounding.WholeTowardZero => TValue.Truncate(value),
    //  UniversalRounding.WholeToNegativeInfinity => TValue.Floor(value),
    //  UniversalRounding.WholeToPositiveInfinity => TValue.Ceiling(value),
    //  UniversalRounding.WholeToRandom => value.RoundToNearestRandom(TValue.Floor(value), TValue.Ceiling(value)),
    //  UniversalRounding.WholeAlternating => value.RoundToNearestAlternating(TValue.Floor(value), TValue.Ceiling(value)),
    //  UniversalRounding.WholeToOdd => value.RoundWholeToOdd(),
    //  UniversalRounding.HalfToEven or
    //  UniversalRounding.HalfAwayFromZero or
    //  UniversalRounding.HalfTowardZero or
    //  UniversalRounding.HalfToNegativeInfinity or
    //  UniversalRounding.HalfToPositiveInfinity => TValue.Round(value, (MidpointRounding)(int)mode),
    //  UniversalRounding.HalfToRandom => value.RoundHalfRandom(),
    //  UniversalRounding.HalfAlternating => value.RoundHalfAlternating(),
    //  UniversalRounding.HalfToOdd => value.RoundHalfToOdd(),
    //  _ => throw new System.ArgumentOutOfRangeException(mode.ToString()), // value.Round((HalfRounding)(int)mode),
    //};
  }
}
