//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
//    public static decimal Round(decimal value, FullRoundingBehavior mode)
//      => mode switch
//      {
//        FullRoundingBehavior.AwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
//        FullRoundingBehavior.Ceiling => System.Math.Ceiling(value),
//        FullRoundingBehavior.Floor => System.Math.Floor(value),
//        FullRoundingBehavior.TowardZero => System.Math.Truncate(value),
//        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
//      };

//    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
//    public static double Round(double value, FullRoundingBehavior mode)
//      => mode switch
//      {
//        FullRoundingBehavior.AwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
//        FullRoundingBehavior.Ceiling => System.Math.Ceiling(value),
//        FullRoundingBehavior.Floor => System.Math.Floor(value),
//        FullRoundingBehavior.TowardZero => System.Math.Truncate(value),
//        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
//      };
//  }

//  public static partial class Maths
//  {
//    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
//    public static decimal Round(decimal value, HalfRoundingBehavior mode)
//      => mode switch
//      {
//        HalfRoundingBehavior.ToEven => System.Math.Floor(value + 0.5M) is var pi && pi % 2 != 0 && value - System.Math.Floor(value) == 0.5M ? pi - 1 : pi,
//        HalfRoundingBehavior.AwayFromZero => System.Math.Truncate(value + 0.5M * System.Math.Sign(value)),
//        HalfRoundingBehavior.TowardZero => value < 0 ? System.Math.Floor(value + 0.5M) : System.Math.Ceiling(value - 0.5M),
//        HalfRoundingBehavior.ToNegativeInfinity => System.Math.Ceiling(value - 0.5M),
//        HalfRoundingBehavior.ToPositiveInfinity => System.Math.Floor(value + 0.5M),
//        HalfRoundingBehavior.ToOdd => System.Math.Floor(value + 0.5M) is var pi && pi % 2 == 0 && value - System.Math.Floor(value) == 0.5M ? pi - 1 : pi,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
//      };

//    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
//    public static double Round(double value, HalfRoundingBehavior mode)
//      => mode switch
//      {
//        HalfRoundingBehavior.ToEven => System.Math.Floor(value + 0.5) is var pi && pi % 2 != 0 && value - System.Math.Floor(value) == 0.5 ? pi - 1 : pi,
//        HalfRoundingBehavior.AwayFromZero => System.Math.Truncate(value + 0.5 * System.Math.Sign(value)),
//        HalfRoundingBehavior.TowardZero => value < 0 ? System.Math.Floor(value + 0.5) : System.Math.Ceiling(value - 0.5),
//        HalfRoundingBehavior.ToNegativeInfinity => System.Math.Ceiling(value - 0.5),
//        HalfRoundingBehavior.ToPositiveInfinity => System.Math.Floor(value + 0.5),
//        HalfRoundingBehavior.ToOdd => System.Math.Floor(value + 0.5) is var pi && pi % 2 == 0 && value - System.Math.Floor(value) == 0.5 ? pi - 1 : pi,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
//      };
//  }
//}
