namespace Flux
{
  /// <summary>The strategy of rounding to the nearest number, and either when a number is halfway between two others or directly to one of two others.</summary>
  /// <seealso cref="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/>
  /// <seealso cref="http://www.cplusplus.com/articles/1UCRko23/"/>
  public enum RoundingMode
  {
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the nearest even number, if possible.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding. Common rounding: round half, bias: even.</remarks>
    HalfToEven = MidpointRounding.ToEven,

    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the number that is further from zero.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0. Symmetric rounding: round half up, bias: away from zero.</remarks>
    HalfAwayFromZero = MidpointRounding.AwayFromZero,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the number that is closer to zero.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0. Symmetric rounding: round half down, bias: towards zero.</remarks>
    HalfTowardZero = MidpointRounding.ToZero,

    /// <summary>Round to the nearest number, and when a number is halfway between two others, round (down) to the number that is less than.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0. Common rounding: round half down, bias: negative infinity.</remarks>
    HalfToNegativeInfinity = MidpointRounding.ToNegativeInfinity,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round (up) to the number that is greater than.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0. Common rounding: round half up, bias: positive infinity.</remarks>
    HalfToPositiveInfinity = MidpointRounding.ToPositiveInfinity,

    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the nearest odd number, if possible.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0. Common rounding: round half, bias: odd.</remarks>
    HalfToOdd = 10,

    /// <summary>Round to the number away-from-zero.</summary>
    /// <remarks>This mode is the opposite of truncating. Symmetric rounding: round up, bias: away from zero.</remarks>
    AwayFromZero = 11,
    /// <summary>Round to the number towards-zero.</summary>
    /// <remarks>This is the same as Math.Truncate of the number. Symmetric rounding: round down, bias: towards zero.</remarks>
    TowardZero = 12,

    /// <summary>Round down to the number less-than-or-equal to value.</summary>
    /// <remarks>This is the same as Math.Floor of the number. Common rounding: round down, bias: negative infinity.</remarks>
    ToNegativeInfinity = 13,
    /// <summary>Round up to the number greater-than-or-equal to value.</summary>
    /// <remarks>This is the same as Math.Ceiling of the number. Common rounding: round up, bias: positive infinity.</remarks>
    ToPositiveInfinity = 14,

    /// <summary>Round up to the power-of-2 number greater-than-or-equal to value.</summary>
    ToPowOf2AwayFromZero = 21,
    /// <summary>Round down to the power-of-2 number less-than-or-equal to value.</summary>
    ToPowOf2TowardZero = 22,
  }
}
