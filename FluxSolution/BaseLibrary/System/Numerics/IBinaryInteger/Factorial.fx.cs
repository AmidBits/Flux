namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the factorial of the <paramref name="number"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TSelf}(TSelf)"/> on larger numbers.</remarks>
    public static TSelf Factorial<TSelf>(this TSelf number)
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(number))
        return -Factorial(TSelf.Abs(number));

      if (number <= TSelf.One)
        return TSelf.One;

      var f = TSelf.One;

      if (number > f)
        for (var m = f + f; m <= number; m++)
          f *= m;

      return f;
    }

    /// <summary>
    /// <para>Compute the split-factorial of the <paramref name="number"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This divide-and-conquer version is faster with numbers larger than 200 or so, but is slower than <see cref="Factorial{TSelf}(TSelf)"/> on smaller numbers.</remarks>
    public static TSelf SplitFactorial<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(number))
        return -SplitFactorial(TSelf.Abs(number));

      if (number <= TSelf.One)
        return number;

      var p = TSelf.One;
      var r = p;
      var currentN = r;

      var h = TSelf.Zero;
      var shift = h;
      var high = shift;

      var log2n = int.CreateChecked(TSelf.Log2(number));

      while (h != number)
      {
        shift += h;
        h = number >>> log2n--;
        var len = high;
        high = (h - TSelf.One) | TSelf.One;
        len = (high - len) / TSelf.CreateChecked(2);

        if (len > TSelf.Zero)
        {
          p *= Product(len);
          r *= p;
        }
      }

      return r << int.CreateChecked(shift);

      TSelf Product(TSelf n)
        => (TSelf.One + TSelf.One) is var two && (n >> 1) is var m && TSelf.IsZero(m) ? (currentN += two) : (n == (TSelf.One << 1)) ? (currentN += two) * (currentN += two) : Product(n - m) * Product(m);
    }
  }
}
