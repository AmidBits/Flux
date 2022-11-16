namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns <paramref name="x"/> raised to the power of <paramref name="n"/>, using exponentiation by squaring. Does not work with a negative exponent.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TExponent>(this TSelf x, TExponent n)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      AssertNonNegative(n);

      if (TExponent.IsZero(n))
        return TSelf.One;

      var y = TSelf.One;

      while (n > TExponent.One)
        checked
        {
          if (TExponent.IsOddInteger(n))
            y *= x;

          x *= x;
          n >>= 1;
        }

      return x * y;
    }
  }
}
