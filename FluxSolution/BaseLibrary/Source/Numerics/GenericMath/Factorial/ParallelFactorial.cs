namespace Flux
{
#if NET7_0_OR_GREATER
  public class ParallelFactorial<TSelf>
    : IFactorialComputable<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
#else
  public class ParallelFactorial
    : IFactorialComputable<System.Numerics.BigInteger>
#endif
  {
#if NET7_0_OR_GREATER

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public TSelf ComputeFactorial(TSelf x)
      => TSelf.IsNegative(x)
      ? -ComputeFactorial(-x)
      : x <= TSelf.One
      ? TSelf.One
      : new Loops.RangeLoop<TSelf>(TSelf.One + TSelf.One, x - TSelf.One, TSelf.One).AsParallel().Aggregate(TSelf.One, (a, b) => a * b);

#else

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public System.Numerics.BigInteger ComputeFactorial(System.Numerics.BigInteger x)
      => x < System.Numerics.BigInteger.Zero
      ? -ComputeFactorial(-x)
      : x <= System.Numerics.BigInteger.One
      ? System.Numerics.BigInteger.One
      : new Loops.RangeLoop(System.Numerics.BigInteger.One + System.Numerics.BigInteger.One, x - System.Numerics.BigInteger.One, System.Numerics.BigInteger.One).AsParallel().Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b);

#endif
  }
}
