namespace Flux
{
  public static partial class Maths // Full (integer) rounding:
  {
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static decimal RoundTo(decimal value, FullRoundingBehavior mode)
      => mode switch
      {
        FullRoundingBehavior.AwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
        FullRoundingBehavior.Ceiling => System.Math.Ceiling(value),
        FullRoundingBehavior.Floor => System.Math.Floor(value),
        FullRoundingBehavior.TowardZero => System.Math.Truncate(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static double RoundTo(double value, FullRoundingBehavior mode)
      => mode switch
      {
        FullRoundingBehavior.AwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
        FullRoundingBehavior.Ceiling => System.Math.Ceiling(value),
        FullRoundingBehavior.Floor => System.Math.Floor(value),
        FullRoundingBehavior.TowardZero => System.Math.Truncate(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static float RoundTo(float value, FullRoundingBehavior mode)
      => (float)RoundTo((double)value, mode);
  }

  public static partial class Maths // Half (midpoint) rounding:
  {
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static decimal RoundTo(decimal value, HalfRoundingBehavior mode)
      => mode switch
      {
        HalfRoundingBehavior.ToEven => System.Math.Floor(value + 0.5M) is var even && even % 2 != 0 && value - System.Math.Floor(value) == 0.5M ? even - 1 : even,
        HalfRoundingBehavior.AwayFromZero => System.Math.Truncate(value + 0.5M * System.Math.Sign(value)),
        HalfRoundingBehavior.TowardZero => value < 0 ? System.Math.Floor(value + 0.5M) : System.Math.Ceiling(value - 0.5M),
        HalfRoundingBehavior.ToNegativeInfinity => System.Math.Ceiling(value - 0.5M),
        HalfRoundingBehavior.ToPositiveInfinity => System.Math.Floor(value + 0.5M),
        HalfRoundingBehavior.ToOdd => System.Math.Floor(value + 0.5M) is var odd && odd % 2 == 0 && value - System.Math.Floor(value) == 0.5M ? odd - 1 : odd,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static double RoundTo(double value, HalfRoundingBehavior mode)
      => mode switch
      {
        HalfRoundingBehavior.ToEven => System.Math.Floor(value + 0.5) is var even && even % 2 != 0 && value - System.Math.Floor(value) == 0.5 ? even - 1 : even,
        HalfRoundingBehavior.AwayFromZero => System.Math.Truncate(value + 0.5 * System.Math.Sign(value)),
        HalfRoundingBehavior.TowardZero => value < 0 ? System.Math.Floor(value + 0.5) : System.Math.Ceiling(value - 0.5),
        HalfRoundingBehavior.ToNegativeInfinity => System.Math.Ceiling(value - 0.5),
        HalfRoundingBehavior.ToPositiveInfinity => System.Math.Floor(value + 0.5),
        HalfRoundingBehavior.ToOdd => System.Math.Floor(value + 0.5) is var odd && odd % 2 == 0 && value - System.Math.Floor(value) == 0.5 ? odd - 1 : odd,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static float RoundTo(float value, HalfRoundingBehavior mode)
      => (float)RoundTo((double)value, mode);
  }
}
