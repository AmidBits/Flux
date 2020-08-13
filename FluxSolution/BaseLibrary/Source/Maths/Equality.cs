namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="precision">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
    public static bool AreAlmostEqual(double a, double b, int precision)
      => AreAlmostEqual(a, b, System.Math.Pow(10.0, -precision));
    /// <summary>Determines whether the specified values (x, y) are almost equal, using a straight forward approach. When the difference between the two values is less than the threshold they are considered equal.</summary>
    /// <see cref="https://stackoverflow.com/questions/3420812/how-do-i-find-if-two-variables-are-approximately-equals"/>
    public static bool AreAlmostEqual(double a, double b, double maxDifference)
      => System.Math.Abs(a - b) <= maxDifference;

    /// <summary>Adaptive precision scaled comparison.</summary>
    /// <see cref="https://stackoverflow.com/questions/2411392/double-epsilon-for-equality-greater-than-less-than-less-than-or-equal-to-gre"/>
    // public static bool AreCloseToEqual(double a, double b) => a != b && (System.Math.Abs(a) + System.Math.Abs(b) + 10.0) * Math.EpsilonCpp32 is var epsilon && (a - b) is var delta ? (-epsilon < delta) && (epsilon > delta) : a == b;
    public static bool AreCloseToEqual(double a, double b)
      => (System.Math.Abs(a) + System.Math.Abs(b) + 10.0) * Maths.EpsilonCpp32 > System.Math.Abs(a - b);

    /// <summary>A simple first order estimatation of equality. Applicable for diminishing decimal precision, when comparing high values.</summary>
    /// <remarks>The maxRelativeDifference (1E-15) works when comparing a computed value against a constant. Increase epsilon (maxRelativeDifference) if both a and b are computed values, to compensate for precisional errors.</remarks>
    /// <see cref="https://stackoverflow.com/questions/2411392/double-epsilon-for-equality-greater-than-less-than-less-than-or-equal-to-gre"/>
    public static bool AreRelativelyEqual(double a, double b, double maxRelativeDifference = 1E-15)
      => System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * maxRelativeDifference;
  }
}
