namespace Flux
{
  public static partial class Em
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

  /// <summary>
  /// <para>The strategy of rounding to the nearest number, and either when a number is halfway between two others or directly to one of two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum UniversalRounding
  {
    /// <summary>
    /// <para>Do not call functions with <see cref="UniversalRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToEven"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the nearest even number, if possible.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</para>
    /// <para>Common rounding: round half, bias: even.</para>
    /// </remarks>
    HalfToEven = HalfRounding.ToEven,

    /// <summary>
    /// <para>Do not call functions with <see cref="UniversalRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.AwayFromZero"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the number that is further from zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</para>
    /// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    /// </remarks>
    HalfAwayFromZero = HalfRounding.AwayFromZero,

    /// <summary>
    /// <para>Do not call functions with <see cref="UniversalRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToZero"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the number that is closer to zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</para>
    /// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    /// </remarks>
    HalfTowardZero = HalfRounding.TowardZero,

    /// <summary>
    /// <para>Do not call functions with <see cref="UniversalRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToNegativeInfinity"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round (down) to the number that is less than.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</para>
    /// <para>Common rounding: round half down, bias: negative infinity.</para>
    /// </remarks>
    HalfToNegativeInfinity = HalfRounding.ToNegativeInfinity,

    /// <summary>
    /// <para>Do not call functions with <see cref="UniversalRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToPositiveInfinity"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round (up) to the number that is greater than.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</para>
    /// <para>Common rounding: round half up, bias: positive infinity.</para>
    /// </remarks>
    HalfToPositiveInfinity = HalfRounding.ToPositiveInfinity,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, use a random number generator (derived from <see cref="System.Random"/>) to decide which.</para>
    /// </summary>
    HalfToRandom = HalfRounding.Random,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, alternate between the lesser and the greater number.</para>
    /// </summary>
    HalfAlternating = HalfRounding.Alternating,

    /// <summary>
    /// <para>This was only added for completeness and to even the odd <see cref="System.MidpointRounding.ToEven"/> method.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the nearest odd number, if possible.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</para>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </remarks>
    HalfToOdd = HalfRounding.ToOdd, // There is no built-in round to nearest odd number.

    /// <summary>
    /// <para></para>
    /// </summary>
    WholeToEven = WholeRounding.ToEven,

    /// <summary>
    /// <para>Round to the number that is away-from-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This mode is the opposite of truncating.</para>
    /// <para>Symmetric rounding: round up, bias: away from zero.</para>
    /// </remarks>
    WholeAwayFromZero = WholeRounding.AwayFromZero,

    /// <summary>
    /// <para>Round to the number that is towards-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Truncate of the number.</para>
    /// <para>Symmetric rounding: round down, bias: towards zero.</para>
    /// </remarks>
    WholeTowardZero = WholeRounding.TowardZero,

    /// <summary>
    /// <para>Round down to the number less-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Floor of the number.</para>
    /// <para>Common rounding: round down, bias: negative infinity.</para>
    /// </remarks>
    WholeToNegativeInfinity = WholeRounding.ToNegativeInfinity,

    /// <summary>
    /// <para>Round up to the number greater-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Ceiling of the number.</para>
    /// <para>Common rounding: round up, bias: positive infinity.</para>
    /// </remarks>
    WholeToPositiveInfinity = WholeRounding.ToPositiveInfinity,

    /// <summary>
    /// <para></para>
    /// </summary>
    WholeToRandom = WholeRounding.Random,

    /// <summary>
    /// <para></para>
    /// </summary>
    WholeAlternating = WholeRounding.Alternating,

    /// <summary>
    /// <para></para>
    /// </summary>
    WholeToOdd = WholeRounding.ToOdd,
  }
}
