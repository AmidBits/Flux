namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Returns the (floor) square root of the <paramref name="value"/>, using Newton's method (from Wikipedia.com).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Square_root"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Integer_square_root#Using_only_integer_division"/></para>
    /// </summary>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <returns>Returns the integer (i.e. floor) square root of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling square root is <see cref="IntegerSqrt"/> + 1.</remarks>
    public static TInteger IntegerSqrt<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (value.TryFastIntegerSqrt(out TInteger isqrtTz, out TInteger _, out var _)) // First try to use double.Sqrt
        return isqrtTz;

      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      if (value <= TInteger.One)
        return value;

      var x0 = value >> 1;

      while (((x0 + value / x0) >> 1) is var x1 && x1 < x0)
        x0 = x1;

      return x0;
    }

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
    public static bool IsIntegerSqrt<TInteger>(this TInteger value, TInteger root)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    => value >= (root * root) // If GTE to square of root.
    && value < (++root * root); // And if LT to square of (root + 1).

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is a perfect square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is a perfect square of <paramref name="root"/>.</returns>
    /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
    public static bool IsPerfectIntegerSqrt<TInteger>(this TInteger value, TInteger root)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value == (root * root);
  }
}
