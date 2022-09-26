#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>PREVIEW! The strategy of rounding to the nearest number, and when a number is halfway between two others.</summary>
  /// <seealso cref="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/>
  /// <seealso cref="http://www.cplusplus.com/articles/1UCRko23/"/>
  public enum HalfwayRounding
  {
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the nearest even number, if possible.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</remarks>
    ToEven = MidpointRounding.ToEven,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the number that is further from zero.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</remarks>
    AwayFromZero = MidpointRounding.AwayFromZero,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the number that is closer to zero.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</remarks>
    TowardZero = MidpointRounding.ToZero,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round (down) to the number that is less than.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</remarks>
    ToNegativeInfinity = MidpointRounding.ToNegativeInfinity,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round (up) to the number that is greater than.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</remarks>
    ToPositiveInfinity = MidpointRounding.ToPositiveInfinity,
    /// <summary>Round to the nearest number, and when a number is halfway between two others, round to the nearest odd number, if possible.</summary>
    /// <remarks>E.g. rounding a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</remarks>
    ToOdd = 5,
  }
}
#endif
