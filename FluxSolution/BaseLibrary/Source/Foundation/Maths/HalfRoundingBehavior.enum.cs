namespace Flux
{
  /// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
  /// <seealso cref="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/>
  /// <seealso cref="http://www.cplusplus.com/articles/1UCRko23/"/>
  public enum HalfRounding
  {
    /// <summary>Rounds a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</summary>
    ToEven = System.MidpointRounding.ToEven,
    /// <summary>Rounds a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</summary>
    AwayFromZero = System.MidpointRounding.AwayFromZero,
    /// <summary>Rounds a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</summary>
    TowardZero = System.MidpointRounding.ToZero,
    /// <summary>Rounds a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</summary>
    ToNegativeInfinity = System.MidpointRounding.ToNegativeInfinity,
    /// <summary>Rounds a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</summary>
    ToPositiveInfinity = System.MidpointRounding.ToPositiveInfinity,
    /// <summary>Rounds a fraction part of 0.5 to the nearest odd integer. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</summary>
    ToOdd = 5,
  }
}
