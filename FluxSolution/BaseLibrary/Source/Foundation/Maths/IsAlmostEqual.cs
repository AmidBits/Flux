namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="digits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
    public static bool IsAlmostEqual(double a, double b, int digits, int radix)
      => IsAlmostEqual(a, b, System.Math.Pow(radix, -digits));

    /// <summary>Determines if two values are almost equal by some absolute difference. Handles smaller values better.</summary>
    /// <param name="absoluteEpsilon">A constant value to compare against the difference between the values.</param>
    public static bool IsAlmostEqual(double a, double b, double absoluteEpsilon)
      => a == b || (System.Math.Abs(a - b) <= absoluteEpsilon);
  }
}
