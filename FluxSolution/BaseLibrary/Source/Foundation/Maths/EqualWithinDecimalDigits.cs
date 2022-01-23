namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
    public static bool EqualWithinDecimalDigits(double a, double b, int decimalDigits)
      => EqualWithinAbsoluteTolerance(a, b, System.Math.Pow(10, -decimalDigits));
  }
}
