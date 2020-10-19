namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
    public static bool IsAlmostEqual(double a, double b, int decimalDigits)
      => IsAlmostEqual(a, b, System.Math.Pow(10d, -decimalDigits));

    private static bool IsAlmostEqualImpl(double a, double b, double absoluteEpsilon)
      => System.Math.Abs(a - b) <= absoluteEpsilon;
    /// <summary>Determines if two values are almost equal by some absolute difference. Handles smaller values better.</summary>
    /// <param name="absoluteEpsilon">A constant value to compare against the difference between the values.</param>
    public static bool IsAlmostEqual(double a, double b, double absoluteEpsilon)
      => a == b || IsAlmostEqualImpl(a, b, absoluteEpsilon);

    private static bool IsApproximatelyEqualImpl(double a, double b, double relativeEpsilon)
      => System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeEpsilon;
    /// <summary>Determines if two values are relatively equal. Suggested by Donald Knuth. Handles values away from zero better, in a relative manner.</summary>
    /// <param name="relativeEpsilon">A value representing a decimal percentage.</param>
    public static bool IsApproximatelyEqual(double a, double b, double relativeEpsilon)
      => a == b || IsApproximatelyEqualImpl(a, b, relativeEpsilon);

    /// <summary>A combination of the ApproximatelyEqual and IsAlmostEqual methods.</summary>
    /// <param name="absoluteEpsilon">A constant value to compare against the difference between the values.</param>
    /// <param name="relativeEpsilon">A value representing a decimal percentage.</param>
    public static bool IsPracticallyEqual(double a, double b, double absoluteEpsilon, double relativeEpsilon)
      => a == b || IsAlmostEqualImpl(a, b, absoluteEpsilon) || IsApproximatelyEqualImpl(a, b, relativeEpsilon);



    /// <summary>Perform a comparison of the difference against radix 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
    public static bool IsAlmostEqual(float a, float b, int decimalDigits)
      => IsAlmostEqual(a, b, System.MathF.Pow(10f, -decimalDigits));

    private static bool IsAlmostEqualImpl(float a, float b, float absoluteEpsilon)
      => System.MathF.Abs(a - b) <= absoluteEpsilon;
    /// <summary>Determines if two values are almost equal by some absolute difference. Handles smaller values better.</summary>
    /// <param name="absoluteEpsilon">A constant value to compare against the difference between the values.</param>
    public static bool IsAlmostEqual(float a, float b, float absoluteEpsilon)
      => a == b || IsAlmostEqualImpl(a, b, absoluteEpsilon);

    private static bool IsApproximatelyEqualImpl(float a, float b, float relativeEpsilon)
      => System.MathF.Abs(a - b) <= System.MathF.Max(System.MathF.Abs(a), System.MathF.Abs(b)) * relativeEpsilon;
    /// <summary>Determines if two values are relatively equal. Suggested by Donald Knuth. Handles values away from zero better, in a relative manner.</summary>
    /// <param name="relativeEpsilon">A value representing a decimal percentage.</param>
    public static bool IsApproximatelyEqual(float a, float b, float relativeEpsilon)
      => a == b || IsApproximatelyEqualImpl(a, b, relativeEpsilon);

    /// <summary>A combination of the ApproximatelyEqual and IsAlmostEqual methods.</summary>
    /// <param name="absoluteEpsilon">A constant value to compare against the difference between the values.</param>
    /// <param name="relativeEpsilon">A value representing a decimal percentage.</param>
    public static bool PracticallyEqual(float a, float b, float absoluteEpsilon, float relativeEpsilon)
      => a == b || IsAlmostEqualImpl(a, b, absoluteEpsilon) || IsApproximatelyEqualImpl(a, b, relativeEpsilon);
  }
}
