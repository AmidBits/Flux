namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring. Does not work with a negative exponent.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TExponent>(this TSelf radix, TExponent exponent)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      AssertNonNegative(exponent);

      if (TExponent.IsZero(exponent))
        return TSelf.One;

      var result = TSelf.One;

      while (exponent > TExponent.One)
        checked
        {
          if (TExponent.IsOddInteger(exponent)) // Only act on set bits in exponent.
            result *= radix; // Multiply by the current corresponding power-of-radix (adjusted in radix below for each iteration).

          radix *= radix; // Compute power-of-radix for the next iteration.
          exponent >>= 1; // Half the exponent for the next iteration.
        }

      return radix * result;
    }
  }
}
