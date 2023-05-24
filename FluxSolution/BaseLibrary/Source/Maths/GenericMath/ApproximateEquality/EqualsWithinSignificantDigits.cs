namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Perform a comparison of the difference against <paramref name="radix"/> raised to the power of the specified <paramref name="significantDigits"/> of precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals (fraction part), considered before finding inequality. Using a negative value allows for left side (integer) tolerance.</param>
    /// <remarks>
    /// <para>EqualsWithinDigitPrecision(1000.02, 1000.015, 2); // The difference of the fraction part is less or equal than 0.01.</para>
    /// <para>EqualsWithinDigitPrecision(1334.261, 1235.272, -2); // The difference of the integer part is less or equal than 100.</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits<TValue>(this TValue a, TValue b, int significantDigits, int radix = 10)
      where TValue : System.Numerics.INumber<TValue>
      => a == b || (double.CreateChecked(TValue.Abs(a - b)) <= System.Math.Pow(AssertRadix(radix), -significantDigits));

#else

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2);</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits(this decimal a, decimal b, int significantDigits, int radix = 10)
      => a == b || (System.Math.Abs(a - b) <= (decimal)System.Math.Pow(AssertRadix(radix), -significantDigits));

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2);</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits(this double a, double b, int significantDigits, int radix = 10)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Pow(AssertRadix(radix), -significantDigits));

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2);</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits(this System.Numerics.BigInteger a, System.Numerics.BigInteger b, int significantDigits, int radix = 10)
      => a == b || (System.Numerics.BigInteger.Abs(a - b) <= System.Numerics.BigInteger.Pow(AssertRadix(radix), -significantDigits));

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2);</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits(this int a, int b, int significantDigits, int radix = 10)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Pow(AssertRadix(radix), -significantDigits));

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2);</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits(this long a, long b, int significantDigits, int radix = 10)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Pow(AssertRadix(radix), -significantDigits));

#endif
  }
}
