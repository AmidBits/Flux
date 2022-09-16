#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Perform a comparison of the difference against radix (base) raised (negated) to the power of the specified number of digits.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="digitCount">The tolerance, as a number of digits (to either side of the decimal point) considered, before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <param name="radix">The radix (or base) to use when testing equality.</param>
    /// <example>Flux.Maths.IsEqualToSignificantDigits(1000.02, 1000.015, 2, 10);</example>
    //public static bool IsEqualToSignificantDigits<TSelf>(TSelf a, TSelf b, TSelf digitCount, TSelf radix)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IPowerFunctions<TSelf>
    //  => TSelf.Abs(a - b) <= TSelf.Pow(radix, -digitCount);

    ///// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    //public static TSelf RoundToMultiple<TSelf>(TSelf value, TSelf interval, System.MidpointRounding mode)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //{
    //  return (value / interval).Round(mode) * interval;
    //}

    /// <summary>PREVIEW! Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is 
    /// 
    /// 
    /// less than (or equal to, depending on the proper flag).</returns>
    public static TSelf RoundDownToPowerOf<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => RoundToNearestPowerOf(value, radix, HalfRounding.TowardZero);

    /// <summary>PREVIEW! Round to a multiple of the provided positive radix.</summary>
    /// <param name="value">Number to be rounded.</param>
    /// <param name="radix">The basis to whose powers to round to. Must be positive.</param>
    public static TSelf RoundToNearestPowerOf<TSelf>(this TSelf value, TSelf radix, HalfRounding mode = HalfRounding.TowardZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.ISignedNumber<TSelf>
    {
      var abs = TSelf.Abs(value);
      var log = TSelf.Log(abs, radix);
      var round = log.Round(mode);
      var pow = TSelf.Pow(radix, round);
      var signed = TSelf.CopySign(pow, value);
      return signed;
    }

    /// <summary>PREVIEW! Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    public static TSelf RoundUpToPowerOf<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => RoundToNearestPowerOf(value, radix, HalfRounding.TowardZero);
  }
}
#endif
