namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines if two values are relatively equal. Suggested by Donald Knuth. Handles values away from zero better, in a relative manner.</summary>
    /// <param name="relativeTolerance">A value to indirectly (multiplied with the maximum of the two values) compare the difference between the two values. I.e. the relative tolerance is the percentage of the maximum of the two values.</param>
    public static bool EqualWithinRelativeTolerance(double a, double b, double relativeTolerance = 1E-15)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);
  }
}
