namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Returns the (floor) square root of the <paramref name="value"/>, using Newton's method.</para>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    /// </summary>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <returns>Returns the integer (i.e. floor) square root of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling square root is <see cref="IntegerSqrt"/> + 1.</remarks>
    public static TNumber IntegerSqrt<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      value.AssertNonNegativeNumber();

      if (value.TryFastIntegerSqrt(UniversalRounding.WholeTowardZero, out TNumber isqrt, out var _)) // Testing!
        return isqrt;

      var x0 = TNumber.One << (value.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the square value.

      if (!TNumber.IsZero(x0))
      {
        while (((x0 + value / x0) >> 1) is var x1 && x1 < x0)
          x0 = x1;

        return x0;
      }

      return value;
    }

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsIntegerSqrt<TNumber>(this TNumber value, TNumber root)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    => value >= (root * root) // If GTE to square of root.
    && value < (++root * root); // And if LT to square of (root + 1).

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is a perfect square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is a perfect square of <paramref name="root"/>.</returns>
    /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsPerfectIntegerSqrt<TNumber>(this TNumber value, TNumber root)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value == (root * root);
  }
}
