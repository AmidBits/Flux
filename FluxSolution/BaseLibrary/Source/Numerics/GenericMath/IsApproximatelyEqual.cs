#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool IsApproximatelyEqualAbsolute<TSelf>(this TSelf a, TSelf b, TSelf absoluteTolerance)
      where TSelf : System.Numerics.INumber<TSelf>
      => a == b || (TSelf.Abs(a - b) <= absoluteTolerance);

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool IsApproximatelyEqualRelative<TSelf, TMu>(this TSelf a, TSelf b, TMu percentTolerance)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IMultiplyOperators<TSelf, TMu, TSelf>
      => a == b || TSelf.Abs(a - b) <= TSelf.Max(TSelf.Abs(a), TSelf.Abs(b)) * percentTolerance;

    /// <summary>Perform a comparison of the difference against 10 raised to the power of the specified precision. Positive <paramref name="significantDigits"/> means digit tolerance on the right side and negative <paramref name="significantDigits"/> allows for left side tolerance.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="significantDigits">The tolerance, as a number of decimals, considered before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <remarks>
    /// <para>IsApproximatelyEqual(1000.02, 1000.015, 2, 10);</para>
    /// <para>IsApproximatelyEqual(1334.261, 1235.272, -2, 10);</para>
    /// </remarks>
    public static bool IsApproximatelyEqualPrecision<TSelf>(this TSelf a, TSelf b, TSelf significantDigits, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => a == b || TSelf.Abs(a - b) <= TSelf.Pow(GenericMath.AssertRadix(radix), -significantDigits);
  }
}
#endif
