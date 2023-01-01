namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2);</para>
    /// </remarks>
    public static bool IsApproximatelyEqualPrecision<TValue>(this TValue a, TValue b, int significantDigits)
      where TValue : System.Numerics.INumber<TValue>
      => ApproximateEquality.BySignificantDigits<TValue>.IsApproximatelyEqual(a, b, significantDigits);
  }

  namespace ApproximateEquality
  {
    /// <summary>Perform a comparison of the difference against a radix raised to the power of the specified precision, e.g. the number of decimal places at which the numbers are considered equal.</summary>
    /// <see href="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <remarks>
    /// <para>The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</para>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2); // 2 will compare the two numbers at 2 decimals, if the difference is smaller than 2 (0.01), which is true in this case.</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2); // -2 = 100 (two zeroes), so if the difference is less than 100, which is true in this case.</para>
    /// </remarks>
    public record class BySignificantDigits<TValue>
      : IEqualityApproximatable<TValue>
      where TValue : System.Numerics.INumber<TValue>
    {
      private readonly int m_significantDigits;

      /// <summary>Create a comparison with a specified precision.</summary>
      /// <param name="significantDigits">A positive number will compare the number of digits on the right side of the decimal point. A negative number will compare the number of digits on the left side of the decimal point.</param>
      public BySignificantDigits(int significantDigits) => m_significantDigits = significantDigits;

      /// <summary>The number of significant digits to consider (positive means right side of decimal point, negative means left side).</summary>
      public int SignificantDigits { get => m_significantDigits; init => m_significantDigits = value; }

      /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision, where: negative numbers compare digits to the left of the decimal point, and positive numbers compare digits to the right of the decimal point.</summary>
      /// <param name="significantDigits">The tolerance, as a number of decimal digits, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
      public static bool IsApproximatelyEqual(TValue a, TValue b, int significantDigits) => a == b || (double.CreateChecked(TValue.Abs(a - b)) <= double.Pow(10, -significantDigits));

      public bool IsApproximatelyEqual(TValue a, TValue b) => IsApproximatelyEqual(a, b, m_significantDigits);
    }
  }
}
