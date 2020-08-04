namespace Flux
{
  public static partial class Math
  {
    /// <summary>Compute the integral that would include the fractional part, if any. It works like truncate but instead of discarding the fractional part, it picks the "next" integral, if needed.</summary>
    public static decimal Envelop(decimal value)
      => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);
    /// <summary>Compute the integral that would include the fractional part, if any. It works like truncate but instead of discarding the fractional part, it picks the "next" integral, if needed.</summary>
    public static double Envelop(double value)
      => System.Math.Sign(value) < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);
  }
}
