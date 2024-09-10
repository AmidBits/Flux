namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the (floor) square root of the <paramref name="value"/>, using Newton's method.</para>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    /// </summary>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <returns>Returns the integer (i.e. floor) square root of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling square root is <see cref="IntegerSqrt"/> + 1.</remarks>
    public static TValue IntegerSqrt<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      value.AssertNonNegativeRealNumber();

      if (TryFastIntegerSqrt(value, UniversalRounding.FullTowardZero, out TValue integerSqrt, out var _)) // Testing!
        return integerSqrt;

      var x0 = TValue.One << (value.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the square value.

      if (!TValue.IsZero(x0))
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
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
    public static bool IsIntegerSqrt<TValue, TRoot>(this TValue value, TRoot root)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRoot : System.Numerics.IBinaryInteger<TRoot>
      => TValue.CreateChecked(root * root) <= value && value < TValue.CreateChecked((root + TRoot.One) * (root + TRoot.One));
    // Does this work? => value / root >= root && value / (root + TValue.One) < (root + TValue.One);

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is a perfect square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is a perfect square of <paramref name="root"/>.</returns>
    /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
    public static bool IsPerfectIntegerSqrt<TValue, TRoot>(this TValue value, TRoot root)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRoot : System.Numerics.IBinaryInteger<TRoot>
      => TValue.CreateChecked(root) is var r && value / r == r // Is integer root?
      && TValue.IsZero(value % r); // Is perfect integer root?
  }
}
