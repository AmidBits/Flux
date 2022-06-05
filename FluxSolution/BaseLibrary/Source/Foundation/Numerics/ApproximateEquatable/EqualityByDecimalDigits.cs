namespace Flux
{
  /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision, i.e. the number of decimal places at which the numbers are considered equal.</summary>
  /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
  /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
  /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
  public readonly struct EqualityByDecimalDigits
   : IEqualityApproximatable, System.IEquatable<EqualityByDecimalDigits>
  {
    private readonly int m_decimalDigits;

    public EqualityByDecimalDigits(int decimalDigits)
      => m_decimalDigits = decimalDigits;
    public EqualityByDecimalDigits()
      : this(2)
    {
    }

    public int DecimalDigits { get => m_decimalDigits; init => m_decimalDigits = value; }

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

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EqualityByDecimalDigits a, EqualityByDecimalDigits b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EqualityByDecimalDigits a, EqualityByDecimalDigits b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(EqualityByDecimalDigits other) => m_decimalDigits == other.m_decimalDigits;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is EqualityByDecimalDigits o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_decimalDigits);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ DecimalDigits = {m_decimalDigits} }}";
    #endregion Object overrides}
  }
}
