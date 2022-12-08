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
    public static bool IsApproximatelyEqualPrecision<TSelf>(this TSelf a, TSelf b, int significantDigits)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => new ApproximateEquality.BySignificantDigits<TSelf>(significantDigits).IsApproximatelyEqual(a, b);
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
    public record class BySignificantDigits<TSelf>
      : IEqualityApproximatable<TSelf>
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      private readonly int m_significantDigits;

      public BySignificantDigits(int significantDigits)
        => m_significantDigits = significantDigits;

      /// <summary>The number of significant digits to consider.</summary>
      public int SignificantDigits { get => m_significantDigits; init => m_significantDigits = value; }

      #region Static methods
      /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
      /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
      /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
      /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
      public static bool IsApproximatelyEqual(TSelf a, TSelf b, int significantDigits)
        => a == b || TSelf.Abs(a - b) <= TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(-significantDigits));
      #endregion Static methods

      #region Implemented interfaces
      /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
      /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
      /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
      /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
      public bool IsApproximatelyEqual(TSelf a, TSelf b)
        => IsApproximatelyEqual(a, b, m_significantDigits);
      #endregion Implemented interfaces
    }
  }
}

//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
//    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
//    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
//    /// <remarks>
//    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2, 10);</para>
//    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2, 10);</para>
//    /// </remarks>
//    public static bool IsApproximatelyEqualPrecision<TSelf>(this TSelf a, TSelf b, int significantDigits, int radix)
//      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
//      => new ApproximateEquality.ApproximateEqualityBySignificantDigits<TSelf>(significantDigits, radix).IsApproximatelyEqual(a, b);
//  }

//  namespace ApproximateEquality
//  {
//    /// <summary>Perform a comparison of the difference against a radix raised to the power of the specified precision, e.g. the number of decimal places at which the numbers are considered equal.</summary>
//    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
//    /// <remarks>The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</remarks>
//    /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2)</example>
//    public record class ApproximateEqualityBySignificantDigits<TSelf>
//      : IEqualityApproximatable<TSelf>
//      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
//    {
//      private readonly int m_radix;
//      private readonly int m_significantDigits;

//      public ApproximateEqualityBySignificantDigits(int significantDigits, int radix = 10)
//      {
//        m_radix = GenericMath.AssertRadix(radix);
//        m_significantDigits = significantDigits;
//      }

//      /// <summary>The radix or base.</summary>
//      public int Radix { get => m_radix; init => m_radix = value; }
//      /// <summary>The number of significant digits to consider.</summary>
//      public int SignificantDigits { get => m_significantDigits; init => m_significantDigits = value; }

//      #region Static methods
//      /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
//      /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
//      /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
//      /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2, 10)</example>
//      public static bool IsApproximatelyEqual(TSelf a, TSelf b, int significantDigits, int radix)
//        => a == b || TSelf.Abs(a - b) <= TSelf.Pow(TSelf.CreateChecked(GenericMath.AssertRadix(radix)), TSelf.CreateChecked(-significantDigits));
//      #endregion Static methods

//      #region Implemented interfaces
//      /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision.</summary>
//      /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
//      /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
//      /// <example>Flux.Math.EqualityApproximation.Almost(1000.02, 1000.015, 2, 10)</example>
//      public bool IsApproximatelyEqual(TSelf a, TSelf b)
//  => IsApproximatelyEqual(a, b, m_significantDigits, m_radix);
//      #endregion Implemented interfaces
//    }
//  }
//}
