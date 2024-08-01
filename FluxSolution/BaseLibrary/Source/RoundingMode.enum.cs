namespace Flux
{
  /// <summary>
  /// <para>The strategy of rounding to the nearest number, and either when a number is halfway between two others or directly to one of two others.</para>
  /// <para><seealso href="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/></para>
  /// <para><seealso href="http://www.cplusplus.com/articles/1UCRko23/"/></para>
  /// </summary>
  public enum RoundingMode
  {
    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the nearest even number, if possible.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</para>
    /// <para>Common rounding: round half, bias: even.</para>
    /// </remarks>
    HalfToEven = MidpointRounding.ToEven,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the number that is further from zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</para>
    /// <para>Symmetric rounding: round half up, bias: away from zero.</para>
    /// </remarks>
    HalfAwayFromZero = MidpointRounding.AwayFromZero,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the number that is closer to zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</para>
    /// <para>Symmetric rounding: round half down, bias: towards zero.</para>
    /// </remarks>
    HalfTowardZero = MidpointRounding.ToZero,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round (down) to the number that is less than.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</para>
    /// <para>Common rounding: round half down, bias: negative infinity.</para>
    /// </remarks>
    HalfToNegativeInfinity = MidpointRounding.ToNegativeInfinity,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round (up) to the number that is greater than.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</para>
    /// <para>Common rounding: round half up, bias: positive infinity.</para>
    /// </remarks>
    HalfToPositiveInfinity = MidpointRounding.ToPositiveInfinity,

    /// <summary>
    /// <para>Round to the nearest number, and when a number is halfway between two others, round to the nearest odd number, if possible.</para>
    /// </summary>
    /// <remarks>
    /// <para>E.g. rounding a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</para>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// </remarks>
    HalfToOdd = 10,

    /// <summary>
    /// <para>Round to the number away-from-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This mode is the opposite of truncating.</para>
    /// <para>Symmetric rounding: round up, bias: away from zero.</para>
    /// </remarks>
    AwayFromZero = 11,

    /// <summary>
    /// <para>Round to the number towards-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Truncate of the number.</para>
    /// <para>Symmetric rounding: round down, bias: towards zero.</para>
    /// </remarks>
    TowardsZero = 12,

    /// <summary>
    /// <para>Round down to the number less-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Floor of the number.</para>
    /// <para>Common rounding: round down, bias: negative infinity.</para>
    /// </remarks>
    ToNegativeInfinity = 13,

    /// <summary>
    /// <para>Round up to the number greater-than-or-equal to value.</para>
    /// </summary>
    /// <remarks>
    /// <para>This is the same as Math.Ceiling of the number.</para>
    /// <para>Common rounding: round up, bias: positive infinity.</para>
    /// </remarks>
    ToPositiveInfinity = 14,
  }
}
