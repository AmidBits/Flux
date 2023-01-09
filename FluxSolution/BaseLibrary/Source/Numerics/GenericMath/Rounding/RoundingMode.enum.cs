namespace Flux
{
  /// <summary>The strategy of rounding to the nearest number, and either when a number is halfway between two others or directly to one of two others.</summary>
  /// <seealso cref="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/>
  /// <seealso cref="http://www.cplusplus.com/articles/1UCRko23/"/>
  public enum RoundingMode
  {
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the nearest even number, if possible.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</remarks>
    HalfToEven = MidpointRounding.ToEven,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the number that is further from zero.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</remarks>
    HalfAwayFromZero = MidpointRounding.AwayFromZero,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the number that is closer to zero.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</remarks>
    HalfTowardZero = MidpointRounding.ToZero,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round (down) to the number that is less than.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</remarks>
    HalfToNegativeInfinity = MidpointRounding.ToNegativeInfinity,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round (up) to the number that is greater than.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</remarks>
    HalfToPositiveInfinity = MidpointRounding.ToPositiveInfinity,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the nearest odd number, if possible.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</remarks>
    HalfToOdd = 5,

    /// <summary>Round to the number that is further/away from zero.</summary>
    /// <remarks>This mode is the opposite of truncating. This means applying either Math.Floor (if less than zero) or Math.Ceiling (if greater than zero) of the number.</remarks>
    Envelop = 11,
    /// <summary>Round to the number that is closer to/towards zero.</summary>
    /// <remarks>This is the same as Math.Truncate of the number.</remarks>
    Truncate = 12,
    /// <summary>Round (down) to the number that is less than.</summary>
    /// <remarks>This is the same as Math.Floor of the number.</remarks>
    Floor = 13,
    /// <summary>Round (up) to the number that is greater.</summary>
    /// <remarks>This is the same as Math.Ceiling of the number.</remarks>
    Ceiling = 14,
  }
}
