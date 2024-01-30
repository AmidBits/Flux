#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Compute the factorial of <paramref name="value"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TSelf}(TSelf)"/> on larger numbers.</remarks>
    public static TSelf Factorial<TSelf>(this TSelf value)
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(value))
        return -Factorial(-value);

      if (value <= TSelf.One)
        return TSelf.One;

      var factorial = TSelf.One;

      if (value > factorial)
        for (var m = factorial + factorial; m <= value; m++)
          factorial *= m;

      return factorial;
    }

    /// <summary>
    /// <para>Compute the factorial of <paramref name="value"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Factorial"/>
    /// </summary>
    /// <remarks>This divide-and-conquer version is faster with numbers larger than 200 or so, but is slower than <see cref="Factorial{TSelf}(TSelf)"/> on smaller numbers.</remarks>
    public static TSelf SplitFactorial<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(value))
        return -SplitFactorial(TSelf.Abs(value));

      if (value <= TSelf.One)
        return value;

      var p = TSelf.One;
      var r = p;
      var currentN = r;

      var h = TSelf.Zero;
      var shift = h;
      var high = shift;

      var log2n = int.CreateChecked(TSelf.Log2(value));

      while (h != value)
      {
        shift += h;
        h = value >> log2n--;
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
#endif
