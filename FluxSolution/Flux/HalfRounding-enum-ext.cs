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

  public static class XtensionHalfRounding
  {
    #region RoundHalfAlternating

    private static bool m_roundHalfAlternatingState; // Internal state.

    #endregion

    /// <summary>
    /// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified <see cref="HalfRounding"/> <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TFloat Round<TFloat>(this TFloat value, HalfRounding mode)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => mode switch
      {
        HalfRounding.ToEven or
        HalfRounding.AwayFromZero or
        HalfRounding.TowardZero or
        HalfRounding.ToNegativeInfinity or
        HalfRounding.ToPositiveInfinity => TFloat.Round(value, (MidpointRounding)(int)mode),
        HalfRounding.ToRandom => value.RoundHalfToRandom(),
        HalfRounding.ToAlternating => value.RoundHalfToAlternating(),
        HalfRounding.ToOdd => value.RoundHalfToOdd(),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>
    /// <para>Try to round <paramref name="value"/> using half-rounding <paramref name="mode"/> into the out parameter <paramref name="result"/>.</para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryRound<TFloat>(this TFloat value, HalfRounding mode, out TFloat result)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      try
      {
        result = value.Round(mode);
        return true;
      }
      catch
      {
        result = default!;
        return false;
      }
    }

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// <para><see cref="HalfRounding.ToOdd"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TFloat RoundHalfToOdd<TFloat>(this TFloat value)
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

    /// <summary>
    /// <para><see cref="HalfRounding.ToRandom"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static TFloat RoundHalfToRandom<TFloat>(this TFloat value)
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

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public static TFloat RoundHalfToAlternating<TFloat>(this TFloat value)
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

    /// <summary>
    /// <para>Round to number of significant digits using <see cref="HalfRounding"/>.</para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="digits"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static TFloat Round<TFloat, TInteger>(this TFloat value, TInteger digits, HalfRounding mode)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IPowerFunctions<TFloat>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var m = TFloat.Pow(TFloat.CreateChecked(10), TFloat.CreateChecked(digits));

      return (value * m).Round(mode) / m;
    }
  }
}
