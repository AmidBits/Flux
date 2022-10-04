#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two power-of-radix nearest to value.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, ensure the power-of-radix are not equal to value, i.e. the two power-of-radix will be LT/GT instead of LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void GetNearestPow<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, bool proper, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      AssertRadix(radix);

      var radixTSelf = TSelf.CreateChecked(radix);

      var absTowardsZero = TSelf.Pow(radixTSelf, TSelf.Truncate(TSelf.Log(TSelf.Abs(value), radixTSelf)));

      var radixTResult = TResult.CreateChecked(radix);

      nearestTowardsZero = TResult.CreateChecked(TSelf.IsNegative(value) ? -absTowardsZero : absTowardsZero);
      nearestAwayFromZero = nearestTowardsZero * radixTResult;

      if (proper)
      {
        if (TSelf.CreateChecked(nearestTowardsZero) == value)
          nearestTowardsZero /= radixTResult;
        if (TSelf.CreateChecked(nearestAwayFromZero) == value)
          nearestAwayFromZero /= radixTResult;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">If true, ensure neither power-of-radix are equal to value, i.e. the two power-of-radix will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TResult RoundToNearestPow<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, bool proper, HalfwayRounding mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GetNearestPow(value, radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundToNearest(value, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two power-of-radix, using the specified <see cref="FullRounding"/> <paramref name="mode"/>, and also return both power-of-radix as out parameters.</summary>
    /// <param name="value">The value for which the power-of-radix will be found.</param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">If true, ensure the power-of-radix are not equal to value, i.e. the two power-of-radix will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The full rounding mode to use.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TResult RoundToPow<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, bool proper, FullRounding mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GetNearestPow(value, radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundTo(value, nearestTowardsZero, nearestAwayFromZero, mode);
    }
  }
}
#endif
