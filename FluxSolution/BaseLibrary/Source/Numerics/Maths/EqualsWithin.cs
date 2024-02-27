#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance<TValue>(this TValue a, TValue b, TValue absoluteTolerance)
      where TValue : System.Numerics.INumber<TValue>
      => a == b
      || TValue.Abs(a - b) <= absoluteTolerance;

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance<TValue, TRelativeTolerance>(this TValue a, TValue b, TRelativeTolerance relativeTolerance)
      where TValue : System.Numerics.INumber<TValue>
      where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
      => a == b
      || TRelativeTolerance.CreateChecked(TValue.Abs(a - b)) <= TRelativeTolerance.CreateChecked(TValue.Max(TValue.Abs(a), TValue.Abs(b))) * relativeTolerance;

    /// <summary>Perform a comparison of the difference against <paramref name="radix"/> raised to the power of the specified <paramref name="significantDigits"/> of precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see href="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals (fraction part), considered before finding inequality. Using a negative value allows for left side (integer) tolerance.</param>
    /// <remarks>
    /// <para>EqualsWithinSignificantDigits(1000.02, 1000.015, 2, 10); // The difference of abs(<paramref name="a"/> - <paramref name="b"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
    /// <para>EqualsWithinSignificantDigits(1334.261, 1235.272, -2, 10); // The difference of abs(<paramref name="a"/> - <paramref name="b"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
    /// </remarks>
    public static bool EqualsWithinSignificantDigits<TValue>(this TValue a, TValue b, int significantDigits, int radix = 10)
      where TValue : System.Numerics.INumber<TValue>
      => a == b
      || (double.CreateChecked(TValue.Abs(a - b)) <= System.Math.Pow(Units.Radix.AssertMember(radix), -significantDigits));
  }
}
#endif
