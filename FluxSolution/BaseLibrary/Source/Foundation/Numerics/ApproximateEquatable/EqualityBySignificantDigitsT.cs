#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision, i.e. the number of decimal places at which the numbers are considered equal.</summary>
  /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
  /// <param name="decimalDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
  /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
  public readonly struct EqualityBySignificantDigits<T>
    : IEqualityApproximable<T>, System.IEquatable<EqualityBySignificantDigits<T>>
    where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
  {
    private readonly T m_radix;
    private readonly T m_significantDigits;

    public EqualityBySignificantDigits(T significantDigits, T radix)
    {
      m_radix = radix;
      m_significantDigits = significantDigits;
    }

    public T Radix { get => m_radix; init => m_radix = value; }
    public T SignificantDigits { get => m_significantDigits; init => m_significantDigits = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(T a, T b)
      => IsApproximatelyEqual(a, b, m_significantDigits, m_radix);

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2, 10)</example>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsApproximatelyEqual(T a, T b, T significantDigits, T radix)
      => T.Abs(a - b) <= T.Pow(radix, -significantDigits);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EqualityBySignificantDigits<T> a, EqualityBySignificantDigits<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EqualityBySignificantDigits<T> a, EqualityBySignificantDigits<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(EqualityBySignificantDigits<T> other) => m_significantDigits == other.m_significantDigits;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is EqualityBySignificantDigits<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_significantDigits);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ DecimalDigits = {m_significantDigits} }}";
    #endregion Object overrides}
  }
}
#endif
