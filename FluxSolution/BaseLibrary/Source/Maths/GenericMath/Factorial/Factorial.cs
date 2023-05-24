namespace Flux
{
#if NET7_0_OR_GREATER
  public class Factorial<TSelf>
    : IFactorialComputable<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
#else
  public class Factorial
    : IFactorialComputable<System.Numerics.BigInteger>
#endif
  {
#if NET7_0_OR_GREATER

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public TSelf ComputeFactorial(TSelf x)
    {
      if (TSelf.IsNegative(x))
        return -ComputeFactorial(-x);

      if (x <= TSelf.One)
        return TSelf.One;

      var f = TSelf.One;

      for (var i = TSelf.One + TSelf.One; i <= x; i++)
        f *= i;

      return f;
    }

#else

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public System.Numerics.BigInteger ComputeFactorial(System.Numerics.BigInteger x)
    {
      if (x < System.Numerics.BigInteger.Zero)
        return -ComputeFactorial(-x);

      if (x <= System.Numerics.BigInteger.One)
        return System.Numerics.BigInteger.One;

      var f = System.Numerics.BigInteger.One;

      for (var i = System.Numerics.BigInteger.One + System.Numerics.BigInteger.One; i <= x; i++)
        f *= i;

      return f;
    }

#endif
  }
}
