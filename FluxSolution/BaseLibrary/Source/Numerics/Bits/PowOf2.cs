namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Get the power-of-2 nearest to value, toward zero (TZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 towards zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be toward zero and never equal to value.</param>
    public static System.Numerics.BigInteger PowOf2Tz<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsNegative(value)
      ? -PowOf2Tz(TSelf.Abs(value), proper)
      // The value is positive/greater-than-or-equal-to-zero.
      : (System.Numerics.BigInteger.CreateChecked(value.TruncMod(TSelf.One, out var remainder)) is var quotient && System.Numerics.BigInteger.IsPow2(quotient))
      ? (proper && TSelf.IsZero(remainder) ? quotient >> 1 : quotient)
      // It's a positive non-power-of-two number.
      : MostSignificant1Bit(quotient); // Use the MS1B for power-of-two closer to zero.

    /// <summary>Get the power-of-2 nearest to value, away from zero (AFZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 away from zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be away from zero and never equal to value.</param>
    public static System.Numerics.BigInteger PowOf2Afz<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsNegative(value)
      ? -PowOf2Afz(TSelf.Abs(value), proper)
      // The value is positive/greater-than-or-equal-to-zero.
      : (System.Numerics.BigInteger.CreateChecked(value.TruncMod(TSelf.One, out var remainder)) is var quotient && System.Numerics.BigInteger.IsPow2(quotient))
      ? (proper ? quotient << 1 : quotient)
      // It's a positive non-power-of-two number.
      : MostSignificant1Bit(quotient) << 1; // Use the next greater MS1B for power-of-two away from zero.

    /// <summary>Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="pow2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="pow2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static System.Numerics.BigInteger NearestPowOf2<TSelf>(this TSelf value, bool proper, RoundingMode mode, out System.Numerics.BigInteger pow2TowardsZero, out System.Numerics.BigInteger pow2AwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (TSelf.IsNegative(value))
      {
        var pow2Nearest = NearestPowOf2(TSelf.Abs(value), proper, mode, out pow2TowardsZero, out pow2AwayFromZero);

        pow2TowardsZero = -pow2TowardsZero;
        pow2AwayFromZero = -pow2AwayFromZero;

        return -pow2Nearest;
      }
      else // The value is positive/greater-than-zero.
      {
        var quotient = System.Numerics.BigInteger.CreateChecked(value.TruncMod(TSelf.One, out var remainder));

        if (System.Numerics.BigInteger.IsPow2(quotient))
        {
          if (proper)
          {
            pow2TowardsZero = TSelf.IsZero(remainder) ? quotient >> 1 : quotient;
            pow2AwayFromZero = quotient << 1;
          }
          else
          {
            pow2TowardsZero = quotient;
            pow2AwayFromZero = TSelf.IsZero(remainder) ? quotient : quotient << 1;
          }
        }
        else // It's a positive non-power-of-two number.
        {
          pow2TowardsZero = MostSignificant1Bit(quotient); // Use the MS1B for power-of-two closer to zero.
          pow2AwayFromZero = pow2TowardsZero << 1; // Use the next greater MS1B for power-of-two farther from zero.
        }
      }

      return BoundaryRounding<TSelf, System.Numerics.BigInteger>.Round(value, mode, pow2TowardsZero, pow2AwayFromZero);
    }

#else

    /// <summary>Get the power-of-2 nearest to value, toward zero.</summary>
    /// <param name="value">The value for which the nearest power-of-2 towards zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be toward zero and never equal to value.</param>
    public static System.Numerics.BigInteger PowOf2Tz(this System.Numerics.BigInteger value, bool proper)
      => value < 0
      ? -PowOf2Tz(-value, proper)
      : value.MostSignificant1Bit() is var ms1b && value == ms1b
      ? (proper ? value >> 1 : value)
      : value.MostSignificant1Bit();

    public static int PowOf2Tz(this int value, bool proper)
      => (int)PowOf2Tz((System.Numerics.BigInteger)value, proper);

    public static long PowerOf2Tz(this long value, bool proper)
      => (long)PowOf2Tz((System.Numerics.BigInteger)value, proper);

    /// <summary>Get the power-of-2 nearest to value, away from zero.</summary>
    /// <param name="value">The value for which the nearest power-of-2 away from zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be away from zero and never equal to value.</param>
    public static System.Numerics.BigInteger PowOf2Afz(this System.Numerics.BigInteger value, bool proper)
      => value < 0
      ? -PowOf2Afz(-value, proper)
      : value.MostSignificant1Bit() is var ms1b && value == ms1b
      ? (proper ? value << 1 : value)
      : value.MostSignificant1Bit() << 1;

    public static int PowOf2Afz(this int value, bool proper)
      => (int)PowOf2Afz((System.Numerics.BigInteger)value, proper);

    public static long PowOf2Afz(this long value, bool proper)
      => (long)PowOf2Afz((System.Numerics.BigInteger)value, proper);

#endif
  }
}
