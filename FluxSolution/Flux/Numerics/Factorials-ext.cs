namespace Flux
{
  public static class Factorials
  {
    extension<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>The factorial of a non-negative integer n, denoted by n!, is the product of all positive integers less than or equal to n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public TInteger Factorial()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (TInteger.IsZero(n))
          return TInteger.One;

        if (n < TInteger.CreateChecked(47))
          return NaiveFactorial(n);

        return SplitFactorial(n);
      }

      /// <summary>
      /// <para>The double factorial of a number n, denoted by n‼, is the product of all the positive integers up to n that have the same parity (odd or even) as n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Double_factorial"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public TInteger DoubleFactorial()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var result = TInteger.One;

        for (var two = result + result; n > TInteger.Zero; n -= two)
          result *= n;

        return result;
      }

      /// <summary>
      /// <para>Computes the factorial of <paramref name="value"/>, e.g. <c>Factorial(5) => 1 * 2 * 3 * 4 * 5</c></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// </summary>
      /// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 60 or so, and starts loosing with larger numbers.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      public TInteger NaiveFactorial()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (TInteger.IsZero(n))
          return TInteger.One;

        var f = TInteger.One;

        if (n > f) // Only loop if value is greater than 1.
          checked
          {
            f++;

            for (var m = f + TInteger.One; m <= n; m++)
              f *= m;
          }

        return f;
      }
    }

    /// <summary>
    /// <para>Compute the factorial using divide-and-conquer, a.k.a. split-factorial of <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
    /// <para><see href="http://www.luschny.de/math/factorial/csharp/FactorialSplit.cs.html"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    public static TInteger SplitFactorial<TInteger>(this TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      if (n <= TInteger.One)
        return TInteger.One;

      var two = (TInteger.One + TInteger.One);

      var p = TInteger.One;
      var r = TInteger.One;
      var currentN = TInteger.One;

      var h = TInteger.Zero;
      var shift = TInteger.Zero;
      var high = TInteger.One;

      var log2n = int.CreateChecked(TInteger.Log2(n));

      while (h != n)
        checked
        {
          shift += h;
          h = n >>> log2n--;
          var len = high;
          high = (h - TInteger.One) | TInteger.One;
          len = (high - len) >>> 1;

          if (len > TInteger.Zero)
          {
            p *= Product(len);
            r *= p;
          }
        }

      return r << int.CreateChecked(shift);

      TInteger Product(TInteger n)
      {
        checked
        {
          var m = n >> 1;

          if (TInteger.IsZero(m))
            return currentN += two;

          if (n == two)
            return (currentN += two) * (currentN += two);

          return Product(n - m) * Product(m);
        }
      }
    }
  }
}
