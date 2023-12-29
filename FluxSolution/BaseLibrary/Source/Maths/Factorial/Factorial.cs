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
  }
}
