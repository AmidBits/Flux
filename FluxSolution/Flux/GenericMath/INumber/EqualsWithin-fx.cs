namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="absoluteTolerance"></param>
    /// <returns></returns>
    public static bool EqualsWithinAbsoluteTolerance<TNumber>(this TNumber a, TNumber b, TNumber absoluteTolerance)
      where TNumber : System.Numerics.INumber<TNumber>
      => a == b
      || TNumber.Abs(a - b) <= absoluteTolerance;

    /// <summary>
    /// <para>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRelativeTolerance"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="relativeTolerance"></param>
    /// <returns></returns>
    public static bool EqualsWithinRelativeTolerance<TNumber, TRelativeTolerance>(this TNumber a, TNumber b, TRelativeTolerance relativeTolerance)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
      => a == b
      || TRelativeTolerance.CreateChecked(TNumber.Abs(a - b)) <= TRelativeTolerance.CreateChecked(TNumber.Max(TNumber.Abs(a), TNumber.Abs(b))) * relativeTolerance;

    /// <summary>
    /// <para>Perform a comparison of the difference against <paramref name="radix"/> raised to the power of the specified <paramref name="significantDigits"/> of precision.</para>
    /// <para>Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</para>
    /// <para><see href="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/></para>
    /// </summary>
    /// <param name="significantDigits">The tolerance, as a number of decimals (fraction part), considered before finding inequality. Using a negative value allows for left side (integer) tolerance.</param>
    /// <remarks>
    /// <para>EqualsWithinSignificantDigits(1000.02, 1000.015, 2, 10); // The difference of abs(<paramref name="a"/> - <paramref name="b"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
    /// <para>EqualsWithinSignificantDigits(1334.261, 1235.272, -2, 10); // The difference of abs(<paramref name="a"/> - <paramref name="b"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits<TNumber>(this TNumber a, TNumber b, int significantDigits, int radix = 10)
      where TNumber : System.Numerics.INumber<TNumber>
      => a == b
      || (double.CreateChecked(TNumber.Abs(a - b)) <= double.Pow(Units.Radix.AssertMember(radix), -significantDigits));
  }
}
