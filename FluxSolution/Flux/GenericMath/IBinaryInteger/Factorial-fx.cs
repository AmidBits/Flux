namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Compute the factorial of the <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInteger Factorial<TInteger>(this TInteger value)
    where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsNegative(value))
        return -Factorial(TInteger.Abs(value));

      if (value <= TInteger.One)
        return TInteger.One;

      var f = TInteger.One;

      if (value > f)
        for (var m = f + f; m <= value; m++)
          f *= m;

      return f;
    }

    /// <summary>
    /// <para>Compute the split-factorial of the <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
    /// <para><see href="http://www.luschny.de/math/factorial/csharp/FactorialSplit.cs.html"/></para>
    /// </summary>
    /// <remarks>This divide-and-conquer version is faster with numbers larger than 200 or so, but is slower than <see cref="Factorial{TValue}(TValue)"/> on smaller numbers.</remarks>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInteger SplitFactorial<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsNegative(value))
        return -SplitFactorial(TInteger.Abs(value));

      if (value <= TInteger.One)
        return TInteger.One;

      var two = (TInteger.One + TInteger.One);

      var p = TInteger.One;
      var r = TInteger.One;
      var currentN = TInteger.One;

      var h = TInteger.Zero;
      var shift = TInteger.Zero;
      var high = TInteger.One;

      var log2n = int.CreateChecked(TInteger.Log2(value));

      checked
      {
        while (h != value)
        {
          shift += h;
          h = value >>> log2n--;
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
      }

      TInteger Product(TInteger n)
      {
        checked
        {
          var m = (n >> 1);
          if (TInteger.IsZero(m)) return (currentN += two);
          if (n == two) return (currentN += two) * (currentN += two);
          return Product(n - m) * Product(m);
        }
      }
    }
  }
}
