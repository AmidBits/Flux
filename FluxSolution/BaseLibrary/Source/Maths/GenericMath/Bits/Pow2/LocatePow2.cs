namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Find the nearest (to <paramref name="value"/>) two (optionally <paramref name="proper"/>) power-of-2.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The nearest two power-of-2 to <paramref name="value"/>.</returns>
    public static (TSelf Pow2TowardsZero, TSelf Pow2AwayFromZero) LocatePow2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(value))
      {
        var (pow2TowardsZero, pow2AwayFromZero) = LocatePow2(TSelf.Abs(value), proper);

        return (-pow2TowardsZero, -pow2AwayFromZero);
      }
      else // The value is positive/greater-than-zero.
      {
        var quotient = value.TruncMod(TSelf.One, out var remainder);

        if (quotient.IsPow2())
        {
          if (proper)
          {
            return (
              TSelf.IsZero(remainder) ? quotient >> 1 : quotient,
              quotient << 1
            );
          }
          else
          {
            return (
              quotient,
              TSelf.IsZero(remainder) ? quotient : quotient << 1
            );
          }
        }
        else // It's a positive NOT power-of-2 number.
        {
          var ms1b = MostSignificant1Bit(quotient);

          return (
            ms1b, // Use the MS1B for power-of-two closer to zero.
            ms1b << 1 // Use the next greater MS1B for power-of-two farther from zero.
          );
        }
      }
    }

#else

    /// <summary>Find the nearest (to <paramref name="value"/>) two (optionally <paramref name="proper"/>) power-of-2.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The nearest two power-of-2 to <paramref name="value"/>.</returns>
    public static (System.Numerics.BigInteger Pow2TowardsZero, System.Numerics.BigInteger Pow2AwayFromZero) LocatePow2(this System.Numerics.BigInteger value, bool proper)
    {
      if (value < 0)
      {
        var (pow2TowardsZero, pow2AwayFromZero) = LocatePow2(System.Numerics.BigInteger.Abs(value), proper);

        return (-pow2TowardsZero, -pow2AwayFromZero);
      }
      else // The value is positive/greater-than-zero.
      {
        var quotient = value.TruncMod(System.Numerics.BigInteger.One, out var remainder);

        if (quotient.IsPow2())
        {
          if (proper)
          {
            return (
              remainder.IsZero ? quotient >> 1 : quotient,
              quotient << 1
            );
          }
          else
          {
            return (
              quotient,
              remainder.IsZero ? quotient : quotient << 1
            );
          }
        }
        else // It's a positive NOT power-of-2 number.
        {
          var ms1b = MostSignificant1Bit(quotient);

          return (
            ms1b, // Use the MS1B for power-of-two closer to zero.
            ms1b << 1 // Use the next greater MS1B for power-of-two farther from zero.
          );
        }
      }
    }

#endif
  }
}
