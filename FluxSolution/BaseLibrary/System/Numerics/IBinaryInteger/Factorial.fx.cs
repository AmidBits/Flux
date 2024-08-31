namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the factorial of the <paramref name="value"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
    public static TValue Factorial<TValue>(this TValue value)
    where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (TValue.IsNegative(value))
        return -Factorial(TValue.Abs(value));

      if (value <= TValue.One)
        return TValue.One;

      var f = TValue.One;

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
    public static TValue SplitFactorial<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (TValue.IsNegative(value))
        return -SplitFactorial(TValue.Abs(value));

      if (value <= TValue.One)
        return value;

      var p = TValue.One;
      var r = p;
      var currentN = r;

      var h = TValue.Zero;
      var shift = h;
      var high = shift;

      var log2n = int.CreateChecked(TValue.Log2(value));

      while (h != value)
      {
        shift += h;
        h = value >>> log2n--;
        var len = high;
        high = (h - TValue.One) | TValue.One;
        len = (high - len) / TValue.CreateChecked(2);

        if (len > TValue.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << int.CreateChecked(shift);

      TValue Product(TValue n)
        => (TValue.One + TValue.One) is var two && (n >> 1) is var m && TValue.IsZero(m) ? (currentN += two) : (n == (TValue.One << 1)) ? (currentN += two) * (currentN += two) : Product(n - m) * Product(m);
    }
  }
}
