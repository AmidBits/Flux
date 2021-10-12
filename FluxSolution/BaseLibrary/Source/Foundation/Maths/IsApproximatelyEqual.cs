namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines if two values are relatively equal. Suggested by Donald Knuth. Handles values away from zero better, in a relative manner.</summary>
    /// <param name="relativeEpsilon">A value representing a decimal percentage.</param>
    public static bool IsApproximatelyEqual(double a, double b, double relativeEpsilon)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeEpsilon);
  }
}
