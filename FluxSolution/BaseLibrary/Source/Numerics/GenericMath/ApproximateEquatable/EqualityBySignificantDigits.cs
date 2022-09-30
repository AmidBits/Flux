#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2, 10);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2, 10);</para>
    /// </remarks>
    public static bool IsApproximatelyEqualPrecision<TValue, TTolerance>(this TValue a, TValue b, TTolerance significantDigits, TTolerance radix)
      where TValue : System.Numerics.INumber<TValue>
      where TTolerance : System.Numerics.INumber<TTolerance>, System.Numerics.IComparisonOperators<TTolerance, TValue, bool>, System.Numerics.IPowerFunctions<TTolerance>
      => a == b
      || TTolerance.Pow(radix, -significantDigits) > TValue.Abs(a - b);
  }

  /// <summary>Perform a comparison of the difference against a radix raised to the power of the specified precision, e.g. the number of decimal places at which the numbers are considered equal.</summary>
  /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
  /// <remarks>The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</remarks>
  /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
  public record class EqualityBySignificantDigits<TValue, TTolerance>
   : IEqualityApproximatable<TValue>
    where TValue : System.Numerics.INumber<TValue>
    where TTolerance : System.Numerics.INumber<TTolerance>, System.Numerics.IComparisonOperators<TTolerance, TValue, bool>, System.Numerics.IPowerFunctions<TTolerance>
  {
    private TTolerance m_radix;
    private TTolerance m_significantDigits;

    public EqualityBySignificantDigits(TTolerance significantDigits, TTolerance radix)
    {
      m_radix = radix;
      m_significantDigits = significantDigits;
    }

    public TTolerance Radix { get => m_radix; init => m_radix = value; }
    public TTolerance SignificantDigits { get => m_significantDigits; init => m_significantDigits = value; }

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2, 10)</example>
    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(TValue a, TValue b)
      => a == b
      || TTolerance.Pow(m_radix, -m_significantDigits) > TValue.Abs(a - b);
  }
}
#endif
