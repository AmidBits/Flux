#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two power-of-radix nearest to value.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="towardsZero">Outputs the power-of-radix towards zero.</param>
    /// <param name="awayFromZero">Outputs the power-of-radix away from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void GetNearestPow<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, bool proper, out TResult towardsZero, out TResult awayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GenericMath.AssertRadix(radix);

      var radixTSelf = TSelf.CreateChecked(radix);

      var absTowardsZero = TSelf.Pow(radixTSelf, TSelf.Truncate(TSelf.Log(TSelf.Abs(value), radixTSelf)));

      var radixTResult = TResult.CreateChecked(radix);

      towardsZero = TResult.CreateChecked(TSelf.IsNegative(value) ? -absTowardsZero : absTowardsZero);
      awayFromZero = towardsZero * radixTResult;

      if (proper)
      {
        if (TSelf.CreateChecked(towardsZero) == value)
          towardsZero /= radixTResult;
        if (TSelf.CreateChecked(awayFromZero) == value)
          awayFromZero /= radixTResult;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="value"></param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="towardsZero">Outputs the power-of-radix towards zero.</param>
    /// <param name="awayFromZero">Outputs the power-of-radix away from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TResult RoundToNearestPow<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, bool proper, HalfwayRounding mode, out TResult towardsZero, out TResult awayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GetNearestPow(value, radix, proper, out towardsZero, out awayFromZero);

      return RoundToNearest(value, towardsZero, awayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two power-of-radix, using the specified <see cref="FullRounding"/> <paramref name="mode"/>, and also return both power-of-radix as out parameters.</summary>
    /// <param name="value"></param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The full rounding mode to use.</param>
    /// <param name="towardsZero">Outputs the power of 2 greater than or equal to <paramref name="value"/>.</param>
    /// <param name="awayFromZero">Outputs the power of 2 less than or equal to <paramref name="value"/>.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TResult RoundToPow<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, bool proper, FullRounding mode, out TResult towardsZero, out TResult awayFromZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.INumber<TResult>
    {
      GetNearestPow(value, radix, proper, out towardsZero, out awayFromZero);

      return RoundTo(value, towardsZero, awayFromZero, mode);
    }
  }
}
#endif
