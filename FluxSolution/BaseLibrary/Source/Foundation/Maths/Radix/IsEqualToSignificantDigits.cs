namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison of the difference against radix (base) raised (negated) to the power of the specified number of digits.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="digitCount">The tolerance, as a number of digits (to either side of the decimal point) considered, before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <param name="radix">The radix (or base) to use when testing equality.</param>
    /// <example>Flux.Maths.IsEqualToSignificantDigits(1000.02, 1000.015, 2, 10);</example>
    public static bool IsEqualToSignificantDigits(double a, double b, int digitCount, byte radix)
      => System.Math.Abs(a - b) <= System.Math.Pow(radix, -digitCount);
    /// <summary>Perform a comparison of the difference against radix (base) raised (negated) to the power of the specified number of digits.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="digitCount">The tolerance, as a number of digits (to either side of the decimal point) considered, before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <param name="radix">The radix (or base) to use when testing equality.</param>
    /// <example>Flux.Maths.IsEqualToSignificantDigits(1000.02, 1000.015, 2, 10);</example>
    public static bool IsEqualToSignificantDigits(float a, float b, int digitCount, byte radix)
      => System.Math.Abs(a - b) <= System.Math.Pow(radix, -digitCount);
  }
}
