namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static decimal Round(decimal value, FullRounding mode)
      => mode switch
      {
        FullRounding.AwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
        FullRounding.Ceiling => System.Math.Ceiling(value),
        FullRounding.Floor => System.Math.Floor(value),
        FullRounding.TowardZero => System.Math.Truncate(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static double Round(double value, FullRounding mode)
      => mode switch
      {
        FullRounding.AwayFromZero => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value),
        FullRounding.Ceiling => System.Math.Ceiling(value),
        FullRounding.Floor => System.Math.Floor(value),
        FullRounding.TowardZero => System.Math.Truncate(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
  }

  public static partial class Maths
  {
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static decimal Round(decimal value, HalfRounding mode)
      => mode switch
      {
        HalfRounding.ToEven => System.Math.Floor(value + 0.5M) is var even && even % 2 != 0 && value - System.Math.Floor(value) == 0.5M ? even - 1 : even,
        HalfRounding.AwayFromZero => System.Math.Truncate(value + 0.5M * System.Math.Sign(value)),
        HalfRounding.TowardZero => value < 0 ? System.Math.Floor(value + 0.5M) : System.Math.Ceiling(value - 0.5M),
        HalfRounding.ToNegativeInfinity => System.Math.Ceiling(value - 0.5M),
        HalfRounding.ToPositiveInfinity => System.Math.Floor(value + 0.5M),
        HalfRounding.ToOdd => System.Math.Floor(value + 0.5M) is var odd && odd % 2 == 0 && value - System.Math.Floor(value) == 0.5M ? odd - 1 : odd,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static double Round(double value, HalfRounding mode)
      => mode switch
      {
        HalfRounding.ToEven => System.Math.Floor(value + 0.5) is var even && even % 2 != 0 && value - System.Math.Floor(value) == 0.5 ? even - 1 : even,
        HalfRounding.AwayFromZero => System.Math.Truncate(value + 0.5 * System.Math.Sign(value)),
        HalfRounding.TowardZero => value < 0 ? System.Math.Floor(value + 0.5) : System.Math.Ceiling(value - 0.5),
        HalfRounding.ToNegativeInfinity => System.Math.Ceiling(value - 0.5),
        HalfRounding.ToPositiveInfinity => System.Math.Floor(value + 0.5),
        HalfRounding.ToOdd => System.Math.Floor(value + 0.5) is var odd && odd % 2 == 0 && value - System.Math.Floor(value) == 0.5 ? odd - 1 : odd,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
  }
}
