namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether <paramref name="value"/> using base <paramref name="radix"/> is a self value. A self value, Colombian value or Devlali value, in a given value base (radix) is a natural value that cannot be written as the sum of any other natural value n and the individual digits of n.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      for (var sn = SelfNumberLowBound(value, radix); sn < value; sn++)
        if (DigitSum(sn, radix) + sn == value)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible value that can make <paramref name="value"/> a self value using base <paramref name="radix"/>.</summary>
    public static TNumber SelfNumberLowBound<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (value <= TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var logRadix = TNumber.CreateChecked(IntegerLogTowardZero(value, radix));
      var maxDistinct = (TNumber.CreateChecked(9) * logRadix) + (value / TNumber.CreateChecked(radix.IntegerPow(logRadix)));

      return TNumber.Max(value - maxDistinct, TNumber.Zero);
    }
  }
}
