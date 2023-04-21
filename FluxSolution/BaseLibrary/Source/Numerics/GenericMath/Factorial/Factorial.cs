using System.Linq;

namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static TSelf Factorial<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x < TSelf.Zero
      ? -Factorial(-x)
      : x <= TSelf.One
      ? TSelf.One
      : new Loops.RangeLoop<TSelf>(TSelf.One + TSelf.One, x - TSelf.One, TSelf.One).AsParallel().Aggregate(TSelf.One, (a, b) => a * b);


    public static TSelf FactorialEx<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var groupCount = TSelf.Min(x.ISqrt(), TSelf.CreateChecked(int.Max(1, System.Environment.ProcessorCount - 2))); // Minimum of square root of x and the number of processors on the system, minus 2 of them, and at least 1 :).

      var (quotient, remainder) = TSelf.DivRem(x, groupCount);

      var list = new System.Collections.Generic.List<Flux.Loops.RangeLoop<TSelf>>();

      for (var index = TSelf.Zero; index < groupCount; index++)
        list.Add(new Flux.Loops.RangeLoop<TSelf>(index * quotient + TSelf.One, quotient, TSelf.One));

      if (remainder > TSelf.Zero)
        list.Add(new Flux.Loops.RangeLoop<TSelf>(groupCount * quotient + TSelf.One, remainder, TSelf.One));

      foreach (var range in list)
        System.Console.WriteLine($"{string.Join(',', range)} = {range.Aggregate(TSelf.One, (a, b) => a * b)}");

      return list.AsParallel().Select(l => l.Aggregate(TSelf.One, (a, b) => a * b)).Aggregate(TSelf.One, (a, b) => a * b);
    }

#else

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static System.Numerics.BigInteger Factorial(this System.Numerics.BigInteger x)
      => x < System.Numerics.BigInteger.Zero
      ? -Factorial(-x)
      : x <= System.Numerics.BigInteger.One
      ? System.Numerics.BigInteger.One
      : new Loops.RangeLoop(System.Numerics.BigInteger.One + System.Numerics.BigInteger.One, x - System.Numerics.BigInteger.One, System.Numerics.BigInteger.One).AsParallel().Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b);


    public static System.Numerics.BigInteger FactorialEx(this System.Numerics.BigInteger x)
    {
      var groupCount = System.Numerics.BigInteger.Min(x.ISqrt(), System.Numerics.BigInteger.Max(1, System.Environment.ProcessorCount - 2)); // Minimum of square root of x and the number of processors on the system, minus 2 of them, and at least 1 :).

      var quotient = System.Numerics.BigInteger.DivRem(x, groupCount, out var remainder);

      var list = new System.Collections.Generic.List<Flux.Loops.RangeLoop>();

      for (var index = System.Numerics.BigInteger.Zero; index < groupCount; index++)
        list.Add(new Flux.Loops.RangeLoop(index * quotient + System.Numerics.BigInteger.One, quotient, System.Numerics.BigInteger.One));

      if (remainder > System.Numerics.BigInteger.Zero)
        list.Add(new Flux.Loops.RangeLoop(groupCount * quotient + System.Numerics.BigInteger.One, remainder, System.Numerics.BigInteger.One));

      foreach (var range in list)
        System.Console.WriteLine($"{string.Join(',', range)} = {range.Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b)}");

      return list.AsParallel().Select(l => l.Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b)).Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b);
    }

#endif
  }
}
