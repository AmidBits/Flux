namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    // Testing Stuff
    //public static TSelf GenericSquareRoot<TSelf>(this TSelf number)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  AssertNonNegative(number);

    //  //if (TryFastIntegerSqrt(number, out var root)) // Testing!
    //  //  return root;



    //  var x0 = TSelf.CreateChecked(System.Numerics.BigInteger.One << (System.Numerics.BigInteger.CreateChecked(number).GetShortestBitLength() / 2 + 1)); // The least power of two bigger than the square number.

    //  if (!TSelf.IsZero(x0))
    //  {
    //    while (((x0 + number / x0) / (TSelf.One + TSelf.One)) is var x1 && x1 < x0)
    //      x0 = x1;

    //    return x0;
    //  }

    //  return number;
    //}



    /// <summary>
    /// <para>Returns the (floor) square root of the <paramref name="number"/>, using Newton's method.</para>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    /// </summary>
    /// <param name="number">The square number to find the square-<paramref name="root"/> of.</param>
    /// <returns>Returns the integer (i.e. floor) square root of <paramref name="number"/>.</returns>
    /// <remarks>The ceiling square root is <see cref="IntegerSqrt"/> + 1.</remarks>
    public static TSelf IntegerSqrt<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(number);

      if (TryFastIntegerSqrt(number, out var root)) // Testing!
        return root;

      var x0 = TSelf.One << (number.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the square number.

      if (!TSelf.IsZero(x0))
      {
        while (((x0 + number / x0) >> 1) is var x1 && x1 < x0)
          x0 = x1;

        return x0;
      }

      return number;
    }

    /// <summary>
    /// <para>Returns whether <paramref name="number"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The square number to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="number"/>.</param>
    /// <returns>Whether the <paramref name="number"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
    public static bool IsIntegerSqrt<TSelf>(this TSelf number, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (root * root) <= number && number < ((root + TSelf.One) * (root + TSelf.One));
    // Does this work? => number / root >= root && number / (root + TSelf.One) < (root + TSelf.One);

    /// <summary>
    /// <para>Returns whether <paramref name="number"/> is a perfect square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The square number to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="number"/>.</param>
    /// <returns>Whether the <paramref name="number"/> is a perfect square of <paramref name="root"/>.</returns>
    /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
    public static bool IsPerfectIntegerSqrt<TSelf>(this TSelf number, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / root == root // Is integer root?
      && TSelf.IsZero(number % root); // Is perfect integer root?

    /// <summary>
    /// <para>Attempts to compute the (floor) square root of <paramref name="number"/> into the out parameter <paramref name="root"/>. This is a faster but limited to integer of 53 bits.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The square number to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="number"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerSqrt<TSelf>(TSelf number, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number.GetBitLengthEx() <= 53)
      {
        root = TSelf.CreateChecked(double.Sqrt(double.CreateChecked(number)));
        return true;
      }

      root = TSelf.Zero;
      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the (floor) square root of <paramref name="number"/> into the out parameter <paramref name="root"/>, using Newton's method.</para>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The square number to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="number"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryIntegerSqrt<TSelf>(this TSelf number, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        root = IntegerSqrt(number);

        return true;
      }
      catch { }

      root = TSelf.Zero;

      return false;
    }

#else

    /// <summary>Returns the (floor) square root of the <paramref name="y"/>, using Newton's method.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    public static System.Numerics.BigInteger IntegerSqrt(this System.Numerics.BigInteger y)
    {
      AssertNonNegative(y);

      var x0 = System.Numerics.BigInteger.One << (y.GetShortestBitLength() / 2 + 1); // The least power of two bigger than the square number.

      if (!x0.IsZero)
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

    /// <summary>Returns whether <paramref name="y"/> is the integer (not necessarily perfect) square of <paramref name="x"/>.</summary>
    public static bool IsIntegerSqrt(this System.Numerics.BigInteger y, System.Numerics.BigInteger x)
      => y >= (x * x) && (x + System.Numerics.BigInteger.One) is var x1 && y < (x1 * x1);

    /// <summary>Returns whether <paramref name="y"/> is a perfect square of <paramref name="x"/>.</summary>
    public static bool IsPerfectIntegerSqrt(this System.Numerics.BigInteger y, System.Numerics.BigInteger x)
      => y / x == x && y % x == System.Numerics.BigInteger.Zero; // Not using "y == checked(x * x)" because risk of overflow.

    /// <summary>Attempts to compute the (floor) square root of <paramref name="y"/> into the out parameter <paramref name="x"/>, using Newton's method.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    /// <see href="https://en.wikipedia.org/wiki/Square_root"/>
    public static bool TryIntegerSqrt(this System.Numerics.BigInteger y, out System.Numerics.BigInteger x)
    {
      try
      {
        x = IntegerSqrt(y);

        return true;
      }
      catch { }

      x = System.Numerics.BigInteger.Zero;

      return false;
    }

#endif
  }
}
