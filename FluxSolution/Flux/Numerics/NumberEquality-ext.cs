namespace Flux
{
  public static partial class NumberEquality
  {
    extension<TNumber>(TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region EqualsWithin

      /// <summary>
      /// <para>Perform an absolute equality test.</para>
      /// <para>Absolute equality checks if the absolute difference between <paramref name="value"/> and <paramref name="other"/> is smaller than a predefined <paramref name="absoluteTolerance"/>. This is useful when you want to ensure the numbers are "close enough" without considering their scale.</para>
      /// <para>Absolute equality is simpler and works well for small numbers or fixed tolerances.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="absoluteTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public bool EqualsWithinAbsoluteTolerance(TNumber other, TNumber absoluteTolerance)
        => value == other
        || TNumber.Abs(value - other) <= absoluteTolerance;

      /// <summary>
      /// <para>Perform a relative equality test.</para>
      /// <para>Relative equality considers the scale of the numbers by dividing the absolute difference by the magnitude of the numbers. This is useful when comparing numbers that may vary significantly in scale.</para>
      /// <para>Relative equality is better for large numbers or numbers with varying scales, as it adjusts the tolerance dynamically.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TRelativeTolerance"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="relativeTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public bool EqualsWithinRelativeTolerance<TRelativeTolerance>(TNumber other, TRelativeTolerance relativeTolerance)
        where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
        => value == other
        || TRelativeTolerance.CreateChecked(TNumber.Abs(value - other)) <= relativeTolerance * TRelativeTolerance.CreateChecked(TNumber.Abs(TNumber.MaxMagnitude(value, other)));

      /// <summary>
      /// <para>Perform both an absolute and a relative equality test for more robust comparisons. Returns true if any test is considered equal, otherwise false.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TRelativeTolerance"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="absoluteTolerance">E.g. 1e-10.</param>
      /// <param name="relativeTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public bool EqualsWithinTolerance<TRelativeTolerance>(TNumber other, TNumber absoluteTolerance, TRelativeTolerance relativeTolerance)
        where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
        => EqualsWithinAbsoluteTolerance(value, other, absoluteTolerance) || EqualsWithinRelativeTolerance(value, other, relativeTolerance);

      /// <summary>
      /// <para>Perform an equality test involving the most (integer part) or the least (fraction part) <typeparamref name="TSignificantDigits"/> using the specified <paramref name="radix"/>.</para>
      /// <para>Positive means most <paramref name="significantDigits"/> tolerance on the fraction part.</para>
      /// <para>Negative means least <paramref name="significantDigits"/> tolerance on the integer part.</para>
      /// <para><see href="https://stackoverflow.com/questions/9180385/is-this-value-valid-float-comparison-that-accounts-for-value-set-number-of-decimal-place"/></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TSignificantDigits"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="significantDigits">The tolerance, as the number of significant digits, considered for equality. A positive value for most significant digits on the right side (fraction part). A negative value for least significant digits on the left side (integer part).</param>
      /// <param name="radix"></param>
      /// <remarks>
      /// <para>EqualsWithinSignificantDigits(1000.02, 1000.015, 2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
      /// <para>EqualsWithinSignificantDigits(1334.261, 1235.272, -2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
      /// </remarks>
      /// <returns></returns>
      public bool EqualsWithinSignificantDigits<TSignificantDigits, TRadix>(TNumber other, TSignificantDigits significantDigits, TRadix radix)
        where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value == other
        || (double.CreateChecked(TNumber.Abs(value - other)) <= double.Pow(double.CreateChecked(Units.Radix.AssertMember(radix)), -double.CreateChecked(significantDigits)));

      #endregion
    }
  }
}
