namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    ///// <summary>Compute the floor power-of-2 of <paramref name="value"/>.</summary>
    ///// <param name="value">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    ///// <returns>The floor power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static TSelf PowOf2<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //  => value.MostSignificant1Bit();

    /// <summary>Compute the two closest (toward-zero and away-from-zero) power-of-2 of <paramref name="value"/>. Specify <paramref name="proper"/> to ensure results that are not equal to value.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="proper">Proper means nearest but do not include <paramref name="value"/> if it's a power-of-2, i.e. the two power-of-2 will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <returns>The two closest (toward-zero and away-from-zero) power-of-2 to <paramref name="value"/>.</returns>
    public static (TSelf powOf2TowardsZero, TSelf powOf2AwayFromZero) PowOf2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      TSelf powOf2TowardsZero, powOf2AwayFromZero;

      if (TSelf.IsNegative(value))
      {
        (powOf2TowardsZero, powOf2AwayFromZero) = PowOf2(TSelf.Abs(value), proper);

        return (-powOf2TowardsZero, -powOf2AwayFromZero);
      }

      powOf2TowardsZero = value.MostSignificant1Bit();
      powOf2AwayFromZero = powOf2TowardsZero != value ? powOf2TowardsZero << 1 : powOf2TowardsZero;

      return proper && powOf2TowardsZero == powOf2AwayFromZero
        ? (powOf2TowardsZero >>= 1, powOf2AwayFromZero <<= 1)
        : (powOf2TowardsZero, powOf2AwayFromZero);
    }

    /// <summary>Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="powOf2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="powOf2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf PowOf2<TSelf>(this TSelf value, bool proper, RoundingMode mode, out TSelf powOf2TowardsZero, out TSelf powOf2AwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      (powOf2TowardsZero, powOf2AwayFromZero) = PowOf2(value, proper);

      return value.RoundToBoundaries(mode, powOf2TowardsZero, powOf2AwayFromZero);
    }

#else

    /// <summary>Get the power-of-2 nearest to value, away from zero (AFZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 away from zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be away from zero and never equal to value.</param>
    public static System.Numerics.BigInteger RoundToPowOf2Afz(this System.Numerics.BigInteger value)
      => RoundToPowOf2Tz(value) is var ms1b && ms1b < value ? (ms1b.IsZero ? 1 : ms1b << 1) : ms1b;

    /// <summary>Get the power-of-2 nearest to value, away from zero (AFZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 away from zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be away from zero and never equal to value.</param>
    public static System.Numerics.BigInteger RoundToPowOf2Afz(this System.Numerics.BigInteger value, bool proper)
      => value.RoundToPowOf2Afz() is var po2u && proper && po2u == value ? po2u << 1 : po2u;

    /// <summary>Get the power-of-2 nearest to value, toward zero (TZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 towards zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be toward zero and never equal to value.</param>
    public static System.Numerics.BigInteger RoundToPowOf2Tz(this System.Numerics.BigInteger value)
      => value.AssertNonNegative().TruncMod(System.Numerics.BigInteger.One, out var _).MostSignificant1Bit();

    /// <summary>Get the power-of-2 nearest to value, toward zero.</summary>
    /// <param name="value">The value for which the nearest power-of-2 towards zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be toward zero and never equal to value.</param>
    public static System.Numerics.BigInteger RoundToPowOf2Tz(this System.Numerics.BigInteger value, bool proper)
      => value.RoundToPowOf2Tz() is var po2d && proper && po2d == value ? po2d >> 1 : po2d;

    /// <summary>Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="pow2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="pow2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static System.Numerics.BigInteger RoundToPow2(this System.Numerics.BigInteger value, bool proper, RoundingMode mode, out System.Numerics.BigInteger pow2TowardsZero, out System.Numerics.BigInteger pow2AwayFromZero)
    {
      if (value < 0)
      {
        var pow2Nearest = RoundToPow2(System.Numerics.BigInteger.Abs(value), proper, mode, out pow2TowardsZero, out pow2AwayFromZero);

        pow2TowardsZero = -pow2TowardsZero;
        pow2AwayFromZero = -pow2AwayFromZero;

        return -pow2Nearest;
      }
      else // The value is positive/greater-than-zero.
      {
        var quotient = value.TruncMod(System.Numerics.BigInteger.One, out var remainder);

        if ((quotient).IsPow2())
        {
          if (proper)
          {
            pow2TowardsZero = remainder.IsZero ? quotient >> 1 : quotient;
            pow2AwayFromZero = quotient << 1;
          }
          else
          {
            pow2TowardsZero = quotient;
            pow2AwayFromZero = remainder.IsZero ? quotient : quotient << 1;
          }
        }
        else // It's a positive non-power-of-two number.
        {
          pow2TowardsZero = quotient.MostSignificant1Bit(); // Use the MS1B for power-of-two closer to zero.
          pow2AwayFromZero = pow2TowardsZero << 1; // Use the next greater MS1B for power-of-two farther from zero.
        }
      }

      return new System.Numerics.BigInteger(((double)value).RoundToBoundaries(mode, (double)pow2TowardsZero, (double)pow2AwayFromZero));
    }

    /// <summary>Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="pow2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="pow2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static int RoundToPow2(this int value, bool proper, RoundingMode mode, out int pow2TowardsZero, out int pow2AwayFromZero)
    {
      var rv = value.ToBigInteger().RoundToPow2(proper, mode, out System.Numerics.BigInteger p2tz, out System.Numerics.BigInteger p2afz);

      pow2TowardsZero = (int)p2tz;
      pow2AwayFromZero = (int)p2afz;

      return (int)rv;
    }

#endif
  }
}