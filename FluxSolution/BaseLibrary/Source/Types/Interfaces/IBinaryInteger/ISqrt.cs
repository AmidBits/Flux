#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the (floor) root of <paramref name="square"/>. Using Newton's method.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static TSelf ISqrt<TSelf>(this TSelf square)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var x0 = TSelf.One << (square.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the sqrt(s).

      if (x0 != TSelf.Zero)
        checked
        {
          var x1 = (x0 + square / x0) >> 1;

          while (x1 < x0)
          {
            x0 = x1;
            x1 = (x0 + square / x0) >> 1;
          }

          return x0;
        }

      return square;
    }

    /// <summary>PREVIEW! Returns whether the <paramref name="square"/> is perfect and outputs the (floor) root of <paramref name="square"/>. Using Newton's method.</summary>
    /// <returns>Whether the <paramref name="square"/> is perfect.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static bool TryISqrt<TSelf>(this TSelf square, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      root = ISqrt(square);

      return (root * root) == square;
    }
  }
}
#endif
