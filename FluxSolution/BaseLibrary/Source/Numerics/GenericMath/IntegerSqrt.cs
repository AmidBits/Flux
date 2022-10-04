#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the (floor) root of the <paramref name="y"/>, using Newton's method.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static TSelf IntegerSqrt<TSelf>(this TSelf y)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegativeValue(y);

      var x0 = TSelf.One << (y.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the square number.

      if (!TSelf.IsZero(x0))
      {
        checked
        {
          while (((x0 + y / x0) >> 1) is var x1 && x1 < x0)
            x0 = x1;
        }

        return x0;
      }

      return y;
    }

    /// <summary>PREVIEW! Returns whether <paramref name="y"/> is the integer (not necessarily perfect) square of <paramref name="x"/>.</summary>
    public static bool IsIntegerSqrt<TSelf>(this TSelf y, TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => y >= (x * x) && (x + TSelf.One) is var x1 && y < (x1 * x1);

    /// <summary>PREVIEW! Returns whether <paramref name="y"/> is a perfect square of <paramref name="x"/>.</summary>
    public static bool IsPerfectIntegerSqrt<TSelf>(this TSelf y, TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => y / x == x && y % x == TSelf.Zero; // Not using "y == checked(x * x)" because risk of overflow.

    /// <summary>PREVIEW! Attempts to compute the (floor) square root of <paramref name="y"/> into the out parameter <paramref name="x"/>, using Newton's method.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    public static bool TryIntegerSqrt<TSelf>(this TSelf y, out TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        x = IntegerSqrt(y);
        return true;
      }
      catch { }

      x = TSelf.Zero;
      return false;
    }
  }
}
#endif
