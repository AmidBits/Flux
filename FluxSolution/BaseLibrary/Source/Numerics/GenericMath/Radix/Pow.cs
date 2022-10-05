#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two power-of-radix nearest to value.</summary>
    /// <param name="x">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="b">The power of alignment.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void GetNearestPow<TSelf, TRadix, TResult>(this TSelf x, TRadix b, bool properNearest, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      AssertRadix(b);

      var radixTSelf = TSelf.CreateChecked(b);

      var absTowardsZero = TSelf.Pow(radixTSelf, TSelf.Truncate(TSelf.Log(TSelf.Abs(x), radixTSelf)));

      var radixTResult = TResult.CreateChecked(b);

      nearestTowardsZero = TResult.CreateChecked(TSelf.IsNegative(x) ? -absTowardsZero : absTowardsZero);
      nearestAwayFromZero = nearestTowardsZero * radixTResult;

      if (properNearest)
      {
        if (TSelf.CreateChecked(nearestTowardsZero) == x)
          nearestTowardsZero /= radixTResult;
        if (TSelf.CreateChecked(nearestAwayFromZero) == x)
          nearestAwayFromZero /= radixTResult;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="x">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="b">The power of to align to.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TResult RoundToNearestPow<TSelf, TRadix, TResult>(this TSelf x, TRadix b, bool properNearest, HalfwayRounding mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GetNearestPow(x, b, properNearest, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundToNearest(x, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two power-of-radix, using the specified <see cref="FullRounding"/> <paramref name="mode"/>, and also return both power-of-radix as out parameters.</summary>
    /// <param name="x">The value for which the power-of-radix will be found.</param>
    /// <param name="b">The power of to align to.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The full rounding mode to use.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TResult RoundToPow<TSelf, TRadix, TResult>(this TSelf x, TRadix b, bool properNearest, FullRounding mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GetNearestPow(x, b, properNearest, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundTo(x, nearestTowardsZero, nearestAwayFromZero, mode);
    }
  }
}
#endif
