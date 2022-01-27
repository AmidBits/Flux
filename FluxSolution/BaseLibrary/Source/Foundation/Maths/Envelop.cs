namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Compute the integral that would envelop the fractional part, i.e. the opposite of truncate.</summary>
    public static decimal Envelop(decimal value)
      => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);

    /// <summary>Compute the integral that would envelop the fractional part, i.e. the opposite of truncate.</summary>
    public static double Envelop(double value)
      => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);
  }
}
