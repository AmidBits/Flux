namespace Flux
{
  /// <summary>
  /// <para>The strategy of rounding to the nearest number, and either when a number is halfway between two others or directly to one of two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum HalfRounding
  {
    /// <summary>
    /// <para>Do not call functions with <see cref="HalfRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToEven"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the nearest even number, if possible.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</para>
    /// <para>Common rounding: round half, bias: even.</para>
    /// </remarks>
    ToEven = MidpointRounding.ToEven,

    /// <summary>
    /// <para>Do not call functions with <see cref="HalfRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.AwayFromZero"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the number that is further from zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</para>
    /// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    /// </remarks>
    AwayFromZero = MidpointRounding.AwayFromZero,

    /// <summary>
    /// <para>Do not call functions with <see cref="HalfRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToZero"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the number that is closer to zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</para>
    /// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    /// </remarks>
    TowardZero = MidpointRounding.ToZero,

    /// <summary>
    /// <para>Do not call functions with <see cref="HalfRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToNegativeInfinity"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round (down) to the number that is less than.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</para>
    /// <para>Common rounding: round half down, bias: negative infinity.</para>
    /// </remarks>
    ToNegativeInfinity = MidpointRounding.ToNegativeInfinity,

    /// <summary>
    /// <para>Do not call functions with <see cref="HalfRounding"/> as parameters unless you need them. Use the built-in rounding method (<see cref="System.MidpointRounding.ToPositiveInfinity"/>) unless you need universal rounding access.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round (up) to the number that is greater than.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</para>
    /// <para>Common rounding: round half up, bias: positive infinity.</para>
    /// </remarks>
    ToPositiveInfinity = MidpointRounding.ToPositiveInfinity,

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// <para>This was only added for completeness and to even the odd <see cref="System.MidpointRounding.ToEven"/> method.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the nearest odd number, if possible.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</para>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </remarks>
    ToOdd = 10, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round randomly to one of them.</para>
    /// </summary>
    /// <remarks>
    /// <para>Rounding a fraction part of 0.5 to one of the two integers randomly. E.g. 1.5 rounds to either 1.0 or 2.0, 2.5 to either 2.0 or 3.0, etc.</para>
    /// <para>Rounding: round half either up or down, bias: none (other than the RNG used).</para>
    /// </remarks>
    ToRandom = 20, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, alternate between them.</para>
    /// </summary>
    ToAlternating = 30, // There is no built-in rounding of this kind.
  }

  public static partial class HalfRoundingExtensions
  {
    extension<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      /// <summary>
      /// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified <see cref="HalfRounding"/> <paramref name="mode"/>.</para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TFloat RoundHalf(HalfRounding mode)
        => mode switch
        {
          HalfRounding.ToEven or
          HalfRounding.AwayFromZero or
          HalfRounding.TowardZero or
          HalfRounding.ToNegativeInfinity or
          HalfRounding.ToPositiveInfinity => TFloat.Round(value, (MidpointRounding)(int)mode), // Use built-in .NET functionality for standard cases.
          HalfRounding.ToAlternating => RoundHalfToAlternating(value),
          HalfRounding.ToOdd => RoundHalfToOdd(value),
          HalfRounding.ToRandom => RoundHalfToRandom(value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
        };
    }

    extension<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
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
      public TFloat RoundByPrecision<TRadix>(HalfRounding mode, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var scalar = TFloat.Pow(TFloat.CreateChecked(Units.Radix.AssertMember(radix)), TFloat.CreateChecked(significantDigits));

        return (value * scalar).RoundHalf(mode) / scalar;
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
      public TFloat RoundByTruncatedPrecision<TRadix>(HalfRounding mode, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var scalar = TFloat.Pow(TFloat.CreateChecked(Units.Radix.AssertMember(radix)), TFloat.CreateChecked(significantDigits + 1));

        return RoundByPrecision((TFloat.Truncate(value * scalar) / scalar), mode, significantDigits, radix);
      }
    }

    #region RoundHalfToAlternating

    private static bool m_roundHalfAlternatingState; // Internal state.

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    private static TFloat RoundHalfToAlternating<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = value.CompareToFractionMidpoint();

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return (m_roundHalfAlternatingState = !m_roundHalfAlternatingState) ? floor : ceiling;
    }

    #endregion

    #region RoundHalfToOdd

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// <para><see cref="HalfRounding.ToOdd"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    private static TFloat RoundHalfToOdd<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = value.CompareToFractionMidpoint();

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return TFloat.IsOddInteger(floor) ? floor : ceiling;
    }

    #endregion

    #region RoundHalfToRandom

    /// <summary>
    /// <para><see cref="HalfRounding.ToRandom"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    private static TFloat RoundHalfToRandom<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = value.CompareToFractionMidpoint();

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return RandomNumberGenerators.SscRng.Shared.Next(2) == 0 ? floor : ceiling;
    }

    #endregion
  }
}
