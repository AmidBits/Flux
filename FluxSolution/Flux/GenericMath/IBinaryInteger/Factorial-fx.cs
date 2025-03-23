namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Compute the factorial of the <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TNumber Factorial<TNumber>(this TNumber value)
    where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (TNumber.IsNegative(value))
        return -Factorial(TNumber.Abs(value));

      if (value <= TNumber.One)
        return TNumber.One;

      var f = TNumber.One;

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
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TNumber SplitFactorial<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (TNumber.IsNegative(value))
        return -SplitFactorial(TNumber.Abs(value));

      if (value <= TNumber.One)
        return TNumber.One;

      var two = (TNumber.One + TNumber.One);

      var p = TNumber.One;
      var r = TNumber.One;
      var currentN = TNumber.One;

      var h = TNumber.Zero;
      var shift = TNumber.Zero;
      var high = TNumber.One;

      var log2n = int.CreateChecked(TNumber.Log2(value));

      checked
      {
        while (h != value)
        {
          shift += h;
          h = value >>> log2n--;
          var len = high;
          high = (h - TNumber.One) | TNumber.One;
          len = (high - len) >>> 1;

          if (len > TNumber.Zero)
          {
            p *= Product(len);
            r *= p;
          }
        }

        return r << int.CreateChecked(shift);
      }

      TNumber Product(TNumber n)
      {
        checked
        {
          var m = (n >> 1);
          if (TNumber.IsZero(m)) return (currentN += two);
          if (n == two) return (currentN += two) * (currentN += two);
          return Product(n - m) * Product(m);
        }
      }
    }
  }
}
