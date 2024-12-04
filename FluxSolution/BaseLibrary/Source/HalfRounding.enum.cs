using System.Net.Http.Headers;

namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified rounding <paramref name="mode"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue RoundHalf<TValue>(this TValue value, HalfRounding mode)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => mode switch
      {
        HalfRounding.ToEven or
        HalfRounding.AwayFromZero or
        HalfRounding.TowardZero or
        HalfRounding.ToNegativeInfinity or
        HalfRounding.ToPositiveInfinity => TValue.Round(value, (MidpointRounding)(int)mode),
        HalfRounding.Random => value.RoundHalfRandom(),
        HalfRounding.Alternating => value.RoundHalfAlternating(),
        HalfRounding.ToOdd => value.RoundHalfToOdd(),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>
    /// <para>Try to round <paramref name="value"/> using half-rounding <paramref name="mode"/> into the out parameter <paramref name="result"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="mode"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryRoundHalf<TValue>(this TValue value, HalfRounding mode, out TValue result)
      where TValue : System.Numerics.IFloatingPoint<TValue>
    {
      try
      {
        result = value.RoundHalf(mode);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToRandom"/></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static TValue RoundHalfRandom<TValue>(this TValue value, System.Random? rng = null)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionMidpoint() is var comparison
      && (TValue.Floor(value) is var floor && comparison <= -1)
      ? floor
      : (TValue.Ceiling(value) is var ceiling && comparison >= 1)
      ? ceiling
      : value.RoundToNearestRandom(floor, ceiling);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue RoundHalfAlternating<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionMidpoint() is var cmp
      && (TValue.Floor(value) is var floor && cmp <= -1)
      ? floor
      : (TValue.Ceiling(value) is var ceiling && cmp >= 1)
      ? ceiling
      : value.RoundToNearestAlternating(floor, ceiling);

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </summary>
    /// <remarks>
    /// <para><see cref="HalfRounding.HalfToOdd"/></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue RoundHalfToOdd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionMidpoint() is var cmp
      && (cmp <= -1)
      ? TValue.Floor(value)
      : (cmp >= 1)
      ? TValue.Ceiling(value)
      : value.RoundWholeToOdd();
  }

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
    /// <para>Round to the nearest number, and when a number is halfway between two others, round randomly to one of them.</para>
    /// </summary>
    /// <remarks>
    /// <para>Rounding a fraction part of 0.5 to one of the two integers randomly. E.g. 1.5 rounds to either 1.0 or 2.0, 2.5 to either 2.0 or 3.0, etc.</para>
    /// <para>Rounding: round half either up or down, bias: none (other than the RNG used).</para>
    /// </remarks>
    Random = 8, // There is no built-in rounding of this kind.

    /// <summary>
    /// <para>This kind of rounding is not part of the .NET library.</para>
    /// <para>Round to the nearest number, and when a number is halfway between two others, alternate between them.</para>
    /// </summary>
    Alternating = 9, // There is no built-in rounding of this kind.

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
  }
}
