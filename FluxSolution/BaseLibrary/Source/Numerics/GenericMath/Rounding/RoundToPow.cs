#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Find the next largest power of 2 less than or equal to <paramref name="value"/>.</summary>
    public static TSelf RoundDownToPow<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      RoundToNearestPow(value, radix, out var lessThanOrEqual, out var _);

      return lessThanOrEqual;
    }
    /// <summary>PREVIEW! Find the next largest power of 2 proper (i.e. always) less than <paramref name="value"/>.</summary>
    public static TSelf RoundDownToPowProper<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      RoundToNearestPowProper(value, radix, out var _, out var greaterThan);

      return greaterThan;
    }

    /// <summary>PREVIEW! Computes the smallest and largest power of 2 greater than or equal to and less than or equal to, respectively, and also returns the nearest (to <paramref name="value"/>) of the two.</summary>
    /// <param name="value"></param>
    /// <param name="greaterThanOrEqual">Outputs the power of 2 greater than or equal to <paramref name="value"/>.</param>
    /// <param name="lessThanOrEqual">Outputs the power of 2 less than or equal to <paramref name="value"/>.</param>
    /// <returns>The nearest power of 2.</returns>
    public static TResult RoundToNearestPowEx<TSelf, TResult>(this TSelf value, TResult radix, out TResult lessThanOrEqual, out TResult greaterThanOrEqual)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>, System.Numerics.ISignedNumber<TResult>
    {
      Radix.AssertRadix(radix);

      var towardZero = TResult.CreateChecked(TSelf.Truncate(TSelf.Log(TSelf.Abs(value), TSelf.CreateChecked(radix))));

      var sign = value < TSelf.Zero ? -TResult.One : TResult.One;

      lessThanOrEqual = sign * IntegerPow(radix, towardZero);
      greaterThanOrEqual = sign * IntegerPow(radix, towardZero + TResult.One);

      // Find and return the one that is closest to the value.
      return TSelf.Abs(TSelf.CreateChecked(greaterThanOrEqual) - value) > TSelf.Abs(value - TSelf.CreateChecked(lessThanOrEqual))
        ? lessThanOrEqual
        : greaterThanOrEqual;
    }

    /// <summary>PREVIEW! Computes the smallest and largest power of 2 greater than or equal to and less than or equal to, respectively, and also returns the nearest (to <paramref name="value"/>) of the two.</summary>
    /// <param name="value"></param>
    /// <param name="greaterThanOrEqual">Outputs the power of 2 greater than or equal to <paramref name="value"/>.</param>
    /// <param name="lessThanOrEqual">Outputs the power of 2 less than or equal to <paramref name="value"/>.</param>
    /// <returns>The nearest power of 2.</returns>
    public static TSelf RoundToNearestPow<TSelf>(this TSelf value, TSelf radix, out TSelf lessThanOrEqual, out TSelf greaterThanOrEqual)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      var truncated = TSelf.Truncate(TSelf.Log(TSelf.Abs(value), radix));

      lessThanOrEqual = TSelf.CopySign(TSelf.Pow(radix, truncated), value);
      greaterThanOrEqual = TSelf.CopySign(TSelf.Pow(radix, truncated + TSelf.One), value);

      return TSelf.Abs(greaterThanOrEqual - value) > TSelf.Abs(value - lessThanOrEqual) ? lessThanOrEqual : greaterThanOrEqual;
    }
    /// <summary>PREVIEW! Computes the smallest and largest power of 2 greater than and larger than, respectively, and also returns the nearest (to <paramref name="value"/>) of the two.</summary>
    /// <param name="value"></param>
    /// <param name="greaterThan">Outputs the power of 2 proper (i.e. always) greater than <paramref name="value"/>.</param>
    /// <param name="lessThan">Outputs the power of 2 proper (i.e. always) less than <paramref name="value"/>.</param>
    /// <returns>The nearest power of 2.</returns>
    public static TSelf RoundToNearestPowProper<TSelf>(this TSelf value, TSelf radix, out TSelf lessThan, out TSelf greaterThan)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      RoundToNearestPow(value, radix, out lessThan, out greaterThan);

      if (value == lessThan)
        lessThan /= radix;

      if (value == greaterThan)
        greaterThan *= radix;

      return TSelf.Abs(greaterThan - value) > TSelf.Abs(value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>PREVIEW! Find the next smallest power of 2 greater than or equal to <paramref name="value"/>.</summary>
    public static TSelf RoundUpToPow<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      RoundToNearestPow(value, radix, out var _, out var greaterThanOrEqual);

      return greaterThanOrEqual;
    }
    /// <summary>PREVIEW! Find the next smallest power of 2 proper (i.e. always) greater than <paramref name="value"/>.</summary>
    public static TSelf RoundUpToPowProper<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    {
      RoundToNearestPowProper(value, radix, out var _, out var greaterThan);

      return greaterThan;
    }
  }
}
#endif
