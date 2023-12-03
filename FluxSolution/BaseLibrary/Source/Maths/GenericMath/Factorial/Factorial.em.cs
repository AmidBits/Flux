namespace Flux
{
  public static partial class Em
  {
#if NET7_0_OR_GREATER

    public static TSelf ComputeFactorial<TSelf>(this TSelf number, FactorialFunction function)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => function switch
      {
        FactorialFunction.Factorial => new Factorial<TSelf>().ComputeFactorial(number),
        //FactorialFunction.GroupedFactorial => new GroupedFactorial<TSelf>().ComputeFactorial(number),
        //FactorialFunction.ParallelFactorial => new ParallelFactorial<TSelf>().ComputeFactorial(number),
        //FactorialFunction.ParallelSplitFactorial => TSelf.CreateChecked(new ParallelSplitFactorial().ComputeFactorial(System.Numerics.BigInteger.CreateChecked(number))),
        FactorialFunction.SplitFactorial => new SplitFactorial<TSelf>().ComputeFactorial(number),
        _ => throw new System.NotImplementedException(nameof(function)),
      };

#else

    public static System.Numerics.BigInteger ComputeFactorial(this System.Numerics.BigInteger number, FactorialFunction function)
      => function switch
      {
        FactorialFunction.Factorial => new Factorial().ComputeFactorial(number),
        //FactorialFunction.GroupedFactorial => new GroupedFactorial().ComputeFactorial(number),
        //FactorialFunction.ParallelFactorial => new ParallelFactorial().ComputeFactorial(number),
        //FactorialFunction.ParallelSplitFactorial => new ParallelSplitFactorial().ComputeFactorial(number),
        FactorialFunction.SplitFactorial => new SplitFactorial().ComputeFactorial(number),
        _ => throw new System.NotImplementedException(nameof(function)),
      };

#endif

    //    /// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    //    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    //    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //    public static System.Numerics.BigInteger Factorial(System.Numerics.BigInteger value)
    //      => value < 0
    //      ? -Factorial(-value)
    //      : value <= 1
    //      ? 1
    //      : Enumerable.Loop(2, value - 1, 1).AsParallel().Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b);
    //    // => ParallelSplitFactorial.Default.ComputeProduct(value);

    //    /// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    //    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    //    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //    public static int Factorial(int value)
    //      => value < 0
    //      ? -Factorial(-value)
    //      : Flux.Enumerable.Loop(2, value - 1, 1).AsParallel().Aggregate(1, (a, b) => checked(a * b));
    //    /// <summary>The factorial of a non-negative integer value, is the product of all positive integers less than or equal to the value.</summary>
    //    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
    //    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    //    public static long Factorial(long value)
    //      => value < 0
    //      ? -Factorial(-value)
    //      : Flux.Enumerable.Loop(2, value - 1, 1).AsParallel().Aggregate(1L, (a, b) => checked(a * b));
  }
}
