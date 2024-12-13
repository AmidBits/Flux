namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the factorial of the <paramref name="value"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
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
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This divide-and-conquer version is faster with numbers larger than 200 or so, but is slower than <see cref="Factorial{TValue}(TValue)"/> on smaller numbers.</remarks>
    public static TNumber SplitFactorial<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (TNumber.IsNegative(value))
        return -SplitFactorial(TNumber.Abs(value));

      if (value <= TNumber.One)
        return value;

      var p = TNumber.One;
      var r = p;
      var currentN = r;

      var h = TNumber.Zero;
      var shift = h;
      var high = shift;

      var log2n = int.CreateChecked(TNumber.Log2(value));

      while (h != value)
      {
        shift += h;
        h = value >>> log2n--;
        var len = high;
        high = (h - TNumber.One) | TNumber.One;
        len = (high - len) / TNumber.CreateChecked(2);

        if (len > TNumber.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << int.CreateChecked(shift);

      TNumber Product(TNumber n)
        => (TNumber.One + TNumber.One) is var two && (n >> 1) is var m && TNumber.IsZero(m) ? (currentN += two) : (n == (TNumber.One << 1)) ? (currentN += two) * (currentN += two) : Product(n - m) * Product(m);
    }
  }
}
