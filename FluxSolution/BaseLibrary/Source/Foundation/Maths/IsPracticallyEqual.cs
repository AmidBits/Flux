namespace Flux
{
  public static partial class Maths
  {
    /// <summary>A combination of the ApproximatelyEqual and IsAlmostEqual methods.</summary>
    /// <param name="absoluteEpsilon">A constant value to compare against the difference between the values.</param>
    /// <param name="relativeEpsilon">A value representing a decimal percentage.</param>
    public static bool IsPracticallyEqual(double a, double b, double absoluteEpsilon, double relativeEpsilon)
      => IsAlmostEqual(a, b, absoluteEpsilon) || IsApproximatelyEqual(a, b, relativeEpsilon);
  }
}
