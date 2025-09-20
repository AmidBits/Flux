//namespace Flux
//{
//  public static partial class BinaryInteger
//  {
//    /// <summary>
//    /// <para>When x is a positive integer, the falling factorial gives the number of n-permutations (sequences of distinct elements) from an x-element set.</para>
//    /// <example>
//    /// <para>The number (3) of different podiums (assignments of gold, silver, and bronze medals) possible in an eight-person race: <c>FallingFactorialPower(8, 3)</c></para>
//    /// </example>
//    /// </summary>
//    /// <typeparam name="TInteger"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="exponent"></param>
//    /// <returns></returns>
//    /// <remarks>The count of permutations no repetitions.</remarks>
//    public static TInteger FallingFactorialPower<TInteger>(this TInteger value, TInteger exponent)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      System.ArgumentOutOfRangeException.ThrowIfNegative(value);
//      System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

//      return checked(value.Factorial() / (value - exponent).Factorial());
//    }

//    /// <summary>
//    /// <para>The rising factorial gives the number of partitions of an n-element set into x ordered sequences (possibly empty).</para>
//    /// <example>
//    /// <para>The "the number of ways to arrange n flags on x flagpoles", where all flags must be used and each flagpole can have any number of flags.</para>
//    /// <para>Equivalently, this is the number of ways to partition a set of size n (the flags) into x distinguishable parts (the poles), with a linear order on the elements assigned to each part (the order of the flags on a given pole).</para>
//    /// </example>
//    /// </summary>
//    /// <typeparam name="TInteger"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="exponent"></param>
//    /// <returns></returns>
//    public static TInteger RisingFactorialPower<TInteger>(this TInteger value, TInteger exponent)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      System.ArgumentOutOfRangeException.ThrowIfNegative(value);
//      System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

//      return checked((value + exponent - TInteger.One).Factorial() / (value - TInteger.One).Factorial());
//    }

//    /// <summary>
//    /// <para>Computes the factorial of <paramref name="value"/>, e.g. Factorial(5); = "5 * 4 * 3 * 2 * 1", but this specialized version adds a <paramref name="threshold"/> at which the algorithm stops, e.g. Factorial(5, 3); = "5 * 4 * 3".</para>
//    /// <para>This enables the prevention of double runs for certain scenarios invovling factorials, e.g. Factorial(5) / Factorial(3).</para>
//    /// <para>It also means that Factorial(5, 1) or Factorial(5, 2) operates the same as Factorial(5).</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
//    /// </summary>
//    /// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
//    /// <typeparam name="TInteger"></typeparam>
//    /// <param name="value"></param>
//    /// <param name="threshold">This cannot be less than one and cannot be greater than or equal to <paramref name="value"/>.</param>
//    /// <returns></returns>
//    public static TInteger Factorial<TInteger>(this TInteger value, TInteger threshold)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      if (TInteger.IsNegative(value))
//        return -Factorial(TInteger.Abs(value), threshold);

//      System.ArgumentOutOfRangeException.ThrowIfLessThan(threshold, TInteger.One);
//      System.ArgumentOutOfRangeException.ThrowIfGreaterThan(threshold, value);

//      var f = TInteger.One;

//      if (value > f) // Only loop if value is greater than 1.
//        for (var m = value; m > threshold; m--) // Only loop from value down to the minimumThreshold.
//          checked { f *= m; }

//      return f;
//    }

//    /// <summary>
//    /// <para>Computes the factorial of <paramref name="value"/>, e.g. Factorial(5); = "5 * 4 * 3 * 2 * 1"</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
//    /// </summary>
//    /// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
//    /// <typeparam name="TInteger"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static TInteger Factorial<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      if (TInteger.IsNegative(value))
//        return -Factorial(TInteger.Abs(value));

//      if (TInteger.IsZero(value))
//        return TInteger.One;

//      var f = TInteger.One;

//      if (value > f) // Only loop if value is greater than 1.
//      {
//        f++;

//        for (var m = f + TInteger.One; m <= value; m++)
//          checked { f *= m; }
//      }

//      return f;
//    }
//    /// <summary>
//    /// <para>Compute the split-factorial of the <paramref name="value"/>.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
//    /// <para><see href="http://www.luschny.de/math/factorial/csharp/FactorialSplit.cs.html"/></para>
//    /// </summary>
//    /// <remarks>This divide-and-conquer version is faster with numbers larger than 200 or so, but is slower than <see cref="Factorial{TValue}(TValue)"/> on smaller numbers.</remarks>
//    /// <typeparam name="TInteger"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    public static TInteger SplitFactorial<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      if (TInteger.IsNegative(value))
//        return -SplitFactorial(TInteger.Abs(value));

//      if (value <= TInteger.One)
//        return TInteger.One;

//      var two = (TInteger.One + TInteger.One);

//      var p = TInteger.One;
//      var r = TInteger.One;
//      var currentN = TInteger.One;

//      var h = TInteger.Zero;
//      var shift = TInteger.Zero;
//      var high = TInteger.One;

//      var log2n = int.CreateChecked(TInteger.Log2(value));

//      checked
//      {
//        while (h != value)
//        {
//          shift += h;
//          h = value >>> log2n--;
//          var len = high;
//          high = (h - TInteger.One) | TInteger.One;
//          len = (high - len) >>> 1;

//          if (len > TInteger.Zero)
//          {
//            p *= Product(len);
//            r *= p;
//          }
//        }

//        return r << int.CreateChecked(shift);
//      }

//      TInteger Product(TInteger n)
//      {
//        checked
//        {
//          var m = n >> 1;

//          if (TInteger.IsZero(m))
//            return currentN += two;

//          if (n == two)
//            return (currentN += two) * (currentN += two);

//          return Product(n - m) * Product(m);
//        }
//      }
//    }
//  }
//}
