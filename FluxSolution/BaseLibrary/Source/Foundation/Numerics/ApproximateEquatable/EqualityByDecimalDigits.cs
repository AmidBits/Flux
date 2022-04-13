namespace Flux
{
  /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision, i.e. the number of decimal places at which the numbers are considered equal.</summary>
  /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
  /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
  /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
  public sealed class EqualityByDecimalDigits
   : IEqualityApproximatable
  {
    private readonly int m_decimalDigits;

    public EqualityByDecimalDigits(int decimalDigits)
      => m_decimalDigits = decimalDigits;
    public EqualityByDecimalDigits()
      : this(2)
    {
    }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(double a, double b)
      => IsApproximatelyEqual(a, b, m_decimalDigits);

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsApproximatelyEqual(double a, double b, int decimalDigits)
      => EqualityByAbsoluteTolerance.IsApproximatelyEqual(a, b, System.Math.Pow(10, -decimalDigits));
  }
}
