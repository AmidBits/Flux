#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    private static bool IsSqrt<TSelf>(this TSelf number, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number >= (root * root) && number < ((root + TSelf.One) * (root + TSelf.One));

    /// <summary>PREVIEW! Returns the (floor) root of <paramref name="number"/>. Using Newton's method.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static TSelf ISqrt<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var x0 = TSelf.One << (number.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the sqrt(s).

      if (x0 != TSelf.Zero)
        checked
        {
          var x1 = (x0 + number / x0) >> 1;

          while (x1 < x0)
          {
            x0 = x1;
            x1 = (x0 + number / x0) >> 1;
          }

          return x0;
        }

      return number;
    }

    /// <summary>PREVIEW! Returns whether the <paramref name="number"/> is a perfect square and outputs the (floor) root of <paramref name="number"/>. Uses Newton's method.</summary>
    /// <returns>Whether the <paramref name="number"/> is a perfect square.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static bool TryISqrt<TSelf>(this TSelf number, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      root = ISqrt(number);

      return (root * root) == number;
    }
  }
}
#endif