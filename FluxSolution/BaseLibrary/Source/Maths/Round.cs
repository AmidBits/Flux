
namespace Flux
{
  /// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
  /// <seealso cref="http://www.jackleitch.net/2010/06/adventures-in-net-rounding-part-2-exotic-rounding-algorithms/"/>
  /// <seealso cref="http://www.cplusplus.com/articles/1UCRko23/"/>
  public enum RoundingBehavior
  {
    /// <summary>Rounds a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</summary>
    HalfToEven = System.MidpointRounding.ToEven,
    /// <summary>Rounds a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</summary>
    HalfAwayFromZero = System.MidpointRounding.AwayFromZero,
    /// <summary>Rounds a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</summary>
    HalfTowardZero = 2, // System.MidpointRounding.ToZero,
    /// <summary>Rounds a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</summary>
    HalfToNegativeInfinity = 3, // System.MidpointRounding.ToNegativeInfinity,
    /// <summary>Rounds a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</summary>
    HalfToPositiveInfinity = 4, // System.MidpointRounding.ToPositiveInfinity,
    /// <summary>Rounds a fraction part of 0.5 to the nearest odd integer instead. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</summary>
    HalfToOdd = 5,
    /// <summary>Rounds every number with a fraction part to the surrounding integer that is further from zero.</summary>
    RoundAwayFromZero = 6,
    /// <summary>Rounds a number with a fraction part to the next lower integer.</summary>
    RoundCeiling = 7,
    /// <summary>Rounds a number with a fraction part to the next greater integer.</summary>
    RoundFloor = 8,
    /// <summary>Rounds every number with a fraction part to the surrounding integer that is closer to zero.</summary>
    RoundTowardZero = 9
  }

  public static partial class Maths
  {
    /// <summary>Rounds a value to the nearest integer. The mode specifies how to round if it is midwat between two numbers.</summary>
    public static decimal RoundToNearest(decimal value, RoundingBehavior mode) => mode switch
    {
      RoundingBehavior.HalfToEven => System.Math.Floor(value + 0.5M) is var even && even % 2 != 0 && value - System.Math.Floor(value) == 0.5M ? even - 1 : even,
      RoundingBehavior.HalfAwayFromZero => System.Math.Truncate(value + 0.5M * System.Math.Sign(value)),
      RoundingBehavior.HalfTowardZero => value < 0 ? System.Math.Floor(value + 0.5M) : System.Math.Ceiling(value - 0.5M),
      RoundingBehavior.HalfToNegativeInfinity => System.Math.Ceiling(value - 0.5M),
      RoundingBehavior.HalfToPositiveInfinity => System.Math.Floor(value + 0.5M),
      RoundingBehavior.HalfToOdd => System.Math.Floor(value + 0.5M) is var odd && odd % 2 == 0 && value - System.Math.Floor(value) == 0.5M ? odd - 1 : odd,
      RoundingBehavior.RoundAwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
      RoundingBehavior.RoundCeiling => System.Math.Ceiling(value),
      RoundingBehavior.RoundFloor => System.Math.Floor(value),
      RoundingBehavior.RoundTowardZero => System.Math.Truncate(value),
      _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
    };
    /// <summary>Rounds a value to the nearest integer. The mode specifies how to round if it is midwat between two numbers.</summary>
    public static double RoundToNearest(double value, RoundingBehavior mode) => mode switch
    {
      RoundingBehavior.HalfToEven => System.Math.Floor(value + 0.5) is var even && even % 2 != 0 && value - System.Math.Floor(value) == 0.5 ? even - 1 : even,
      RoundingBehavior.HalfAwayFromZero => System.Math.Truncate(value + 0.5 * System.Math.Sign(value)),
      RoundingBehavior.HalfTowardZero => value < 0 ? System.Math.Floor(value + 0.5) : System.Math.Ceiling(value - 0.5),
      RoundingBehavior.HalfToNegativeInfinity => System.Math.Ceiling(value - 0.5),
      RoundingBehavior.HalfToPositiveInfinity => System.Math.Floor(value + 0.5),
      RoundingBehavior.HalfToOdd => System.Math.Floor(value + 0.5) is var odd && odd % 2 == 0 && value - System.Math.Floor(value) == 0.5 ? odd - 1 : odd,
      RoundingBehavior.RoundAwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
      RoundingBehavior.RoundCeiling => System.Math.Ceiling(value),
      RoundingBehavior.RoundFloor => System.Math.Floor(value),
      RoundingBehavior.RoundTowardZero => System.Math.Truncate(value),
      _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
    };

    ///// <summary>Rounds a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</summary>
    //public static decimal RoundToNearestAwayFromZero(decimal value) => System.Math.Truncate(value + 0.5M * System.Math.Sign(value));
    ///// <summary>Rounds a fraction part of 0.5 results in the nearest integer that is further from zero. For example, suppose we’re rounding 1.5. The nearest integers are 1.0 and 2.0, so the result would be 2.0 because it’s further from zero. Similarly, -1.5 would round to -2.0.</summary>
    //public static double RoundToNearestAwayFromZero(double value) => System.Math.Truncate(value + 0.5 * System.Math.Sign(value));

    ///// <summary>Rounds a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</summary>
    //public static decimal RoundToNearestEven(decimal value) => System.Math.Floor(value + 0.5M) is var tpi && tpi % 2 != 0 && value - System.Math.Floor(value) == 0.5M ? tpi - 1 : tpi;
    ///// <summary>Rounds a fraction part of 0.5 to the nearest even integer. For example, 1.5 and 2.5 both round to 2.0 and -1.5 and -2.5 both round to -2.0. A.k.a. Bankers Rounding.</summary>
    //public static double RoundToNearestEven(double value) => System.Math.Floor(value + 0.5) is var tpi && tpi % 2 != 0 && value - System.Math.Floor(value) == 0.5 ? tpi - 1 : tpi;

    ///// <summary>Rounds a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</summary>
    //public static decimal RoundToNearestNegativeInfinity(decimal value) => System.Math.Ceiling(value - 0.5M);
    ///// <summary>Rounds a fraction part of 0.5 to the nearest lower integer. For example, 1.5 rounds to 1.0 and -1.5 rounds to -2.0.</summary>
    //public static double RoundToNearestNegativeInfinity(double value) => System.Math.Ceiling(value - 0.5);

    ///// <summary>Rounds a fraction part of 0.5 to the nearest odd integer instead. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</summary>
    //public static decimal RoundToNearestOdd(decimal value) => System.Math.Floor(value + 0.5M) is var tpi && tpi % 2 == 0 && value - System.Math.Floor(value) == 0.5M ? tpi - 1 : tpi;
    ///// <summary>Rounds a fraction part of 0.5 to the nearest odd integer instead. For example, 1.5 rounds to 1.0, 2.5 rounds to 3.0, -1.5 rounds to -1.0, and -2.5 rounds to -3.0.</summary>
    //public static double RoundToNearestOdd(double value) => System.Math.Floor(value + 0.5) is var tpi && tpi % 2 == 0 && value - System.Math.Floor(value) == 0.5 ? tpi - 1 : tpi;

    ///// <summary>Rounds a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</summary>
    //public static decimal RoundToNearestPositiveInfinity(decimal value) => System.Math.Floor(value + 0.5M);
    ///// <summary>Rounds a fraction part of 0.5 to the nearest greater integer. For example, 1.5 would round to 2.0 and -1.5 would round to -1.0.</summary>
    //public static double RoundToNearestPositiveInfinity(double value) => System.Math.Floor(value + 0.5);

    ///// <summary>Rounds a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</summary>
    //public static decimal RoundToNearestTowardZero(decimal value) => value < 0 ? System.Math.Floor(value + 0.5M) : System.Math.Ceiling(value - 0.5M);
    ///// <summary>Rounds a fraction part of 0.5 to the nearest integer that is closer to zero. For example, 1.5 would round to 1.0 and -1.5 would round to -1.0.</summary>
    //public static double RoundToNearestTowardZero(double value) => value < 0 ? System.Math.Floor(value + 0.5) : System.Math.Ceiling(value - 0.5);

    /// <summary>Like System.Math.Round but that truncates the number at the specified number of fractional digits and then performs the rounding of the number.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static decimal TruncatedRound(decimal value, int digits, System.MidpointRounding mode)
      => digits > 0 && (decimal)System.Math.Pow(10, digits + 1) is var scalar ? System.Math.Round(System.Math.Truncate(scalar * value) / scalar, digits, mode) : throw new System.ArgumentOutOfRangeException(nameof(digits));
    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static double TruncatedRound(double value, int digits, System.MidpointRounding mode)
      => digits >= 0 && System.Math.Pow(10, digits + 1) is var scalar ? System.Math.Round(System.Math.Truncate(scalar * value) / scalar, digits, mode) : throw new System.ArgumentOutOfRangeException(nameof(digits));
  }
}
