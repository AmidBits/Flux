#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the (floor) root of <paramref name="number"/>. Using Newton's method.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static TSelf IntegerSqrt<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var x0 = TSelf.One << (number.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the square number.

      if (x0 != TSelf.Zero)
        checked
        {
          while (((x0 + number / x0) >> 1) is var x1 && x1 < x0)
            x0 = x1;

          return x0;
        }

      return number;
    }

    /// <summary>PREVIEW! Returns whether <paramref name="number"/> is the integer (not perfect) square of <paramref name="root"/>.</summary>
    public static bool IsIntegerSqrt<TSelf>(this TSelf number, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number >= (root * root) && number < ((root + TSelf.One) * (root + TSelf.One));

    /// <summary>PREVIEW! Returns whether <paramref name="number"/> is a perfect square of <paramref name="root"/>.</summary>
    public static bool IsPerfectIntegerSqrt<TSelf>(this TSelf number, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number == checked(root * root);
    // => number / root == root && number % root == TSelf.Zero;

    /// <summary>PREVIEW! Returns whether the <paramref name="number"/> is a perfect square and outputs the (floor) root of <paramref name="number"/>. Uses Newton's method.</summary>
    /// <returns>Whether the <paramref name="number"/> is a perfect square.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static bool TryIntegerSqrt<TSelf>(this TSelf number, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      root = IntegerSqrt(number);

      return IsPerfectIntegerSqrt(number, root);
    }
  }
}
#endif
