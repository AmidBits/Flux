using System.Linq;

namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    public static System.Numerics.BigInteger SplitFactorial<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(source))
        return -SplitFactorial(TSelf.Abs(source));

      if (source <= TSelf.One)
        return 1;

      var p = System.Numerics.BigInteger.One;
      var r = System.Numerics.BigInteger.One;
      var currentN = System.Numerics.BigInteger.One;

      TSelf h = TSelf.Zero, shift = TSelf.Zero, high = TSelf.One;
      var log2n = source.ILog2(); // System.Nu merics.BigInteger.Log(n);

      while (h != source)
      {
        shift += h;
        h = source >> log2n--;
        TSelf len = high;
        high = (h - TSelf.One) | TSelf.One;
        len = (high - len).Divide(2);

        if (len > TSelf.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << int.CreateChecked(shift);

      System.Numerics.BigInteger Product(TSelf n)
        => (n >> 1) is var m && (m == TSelf.Zero) ? (currentN += 2) : (n == (TSelf.One << 1)) ? (currentN += 2) * (currentN += 2) : Product(n - m) * Product(m);
    }

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static TSelf Factorial<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(x))
        return +Factorial(x);

      if (x <= TSelf.One)
        return TSelf.One;

      var f = TSelf.One;

      for (var i = TSelf.One + TSelf.One; i <= x; i++)
        f *= i;

      return f;
    }

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static TSelf FactorialParallel<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(x)
      ? -Factorial(-x)
      : x <= TSelf.One
      ? TSelf.One
      : new Loops.RangeLoop<TSelf>(TSelf.One + TSelf.One, x - TSelf.One, TSelf.One).AsParallel().Aggregate(TSelf.One, (a, b) => a * b);

    public static TSelf GroupedFactorial<TSelf>(this TSelf x, int? threadCount = null)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var groupCount = threadCount.HasValue && threadCount.Value <= System.Environment.ProcessorCount
        ? TSelf.CreateChecked(threadCount.Value)
        : TSelf.Min(x.ISqrt(), TSelf.CreateChecked(int.Max(1, System.Environment.ProcessorCount - 2))); // Minimum of square root of x and the number of processors on the system, minus 2 of them, and at least 1 :).

      var (quotient, remainder) = TSelf.DivRem(x, groupCount);

      var list = new System.Collections.Generic.List<Flux.Loops.RangeLoop<TSelf>>();

      for (var index = TSelf.Zero; index < groupCount; index++)
        list.Add(new Flux.Loops.RangeLoop<TSelf>(index * quotient + TSelf.One, quotient, TSelf.One));

      if (remainder > TSelf.Zero)
        list.Add(new Flux.Loops.RangeLoop<TSelf>(groupCount * quotient + TSelf.One, remainder, TSelf.One));

      //foreach (var range in list)
      //  System.Console.WriteLine($"{string.Join(',', range)} = {range.Aggregate(TSelf.One, (a, b) => a * b)}");

      return list.AsParallel().Select(l => l.Aggregate(TSelf.One, (a, b) => a * b)).Aggregate(TSelf.One, (a, b) => a * b);
    }

#else

    public static System.Numerics.BigInteger SplitFactorial(this System.Numerics.BigInteger source)
    {
      if (source < System.Numerics.BigInteger.Zero)
        return -SplitFactorial(-source);

      if (source <= System.Numerics.BigInteger.One)
        return 1;

      var p = System.Numerics.BigInteger.One;
      var r = System.Numerics.BigInteger.One;
      var currentN = System.Numerics.BigInteger.One;

      System.Numerics.BigInteger h = System.Numerics.BigInteger.Zero, shift = System.Numerics.BigInteger.Zero, high = System.Numerics.BigInteger.One;
      var log2n = source.ILog2(); // System.Nu merics.BigInteger.Log(n);

      while (h != source)
      {
        shift += h;
        h = source >> log2n--;
        System.Numerics.BigInteger len = high;
        high = (h - System.Numerics.BigInteger.One) | System.Numerics.BigInteger.One;
        len = (high - len) / 2;

        if (len > System.Numerics.BigInteger.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << (int)shift;

      System.Numerics.BigInteger Product(System.Numerics.BigInteger n)
        => (n >> 1) is var m && (m == System.Numerics.BigInteger.Zero) ? (currentN += 2) : (n == (System.Numerics.BigInteger.One << 1)) ? (currentN += 2) * (currentN += 2) : Product(n - m) * Product(m);
    }

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static System.Numerics.BigInteger Factorial(this System.Numerics.BigInteger x)
    {
      if (x < System.Numerics.BigInteger.Zero)
        return -Factorial(-x);

      if (x <= System.Numerics.BigInteger.One)
        return System.Numerics.BigInteger.One;

      var f = System.Numerics.BigInteger.One;

      for (var i = System.Numerics.BigInteger.One + System.Numerics.BigInteger.One; i <= x; i++)
        f *= i;

      return f;
    }

    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static System.Numerics.BigInteger FactorialParallel(this System.Numerics.BigInteger x)
      => x < System.Numerics.BigInteger.Zero
      ? -Factorial(-x)
      : x <= System.Numerics.BigInteger.One
      ? System.Numerics.BigInteger.One
      : new Loops.RangeLoop(System.Numerics.BigInteger.One + System.Numerics.BigInteger.One, x - System.Numerics.BigInteger.One, System.Numerics.BigInteger.One).AsParallel().Aggregate(System.Numerics.BigInteger.One, (a, b) => a * b);

    public static System.Numerics.BigInteger GroupedFactorial(this System.Numerics.BigInteger x, int? threadCount = null)
    {
      var groupCount = threadCount.HasValue && threadCount.Value <= System.Environment.ProcessorCount
        ? threadCount.Value.ToBigInteger()
        : System.Numerics.BigInteger.Min(x.ISqrt(), System.Numerics.BigInteger.Max(1, System.Environment.ProcessorCount - 2)); // Minimum of square root of x and the number of processors on the system, minus 2 of them, and at least 1 :).

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
