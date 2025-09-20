namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Returns whether <paramref name="value"/> using base <paramref name="radix"/> is a self value. A self value, Colombian value or Devlali value, in a given value base (radix) is a natural value that cannot be written as the sum of any other natural value n and the individual digits of n.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      for (var sn = SelfNumberLowBound(value, radix); sn < value; sn++)
        if (DigitSum(sn, radix) + sn == value)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible value that can make <paramref name="value"/> a self value using base <paramref name="radix"/>.</summary>
    public static TInteger SelfNumberLowBound<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (value <= TInteger.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var logRadix = TInteger.CreateChecked(IntegerLogTowardZero(value, radix));
      var maxDistinct = (TInteger.CreateChecked(9) * logRadix) + (value / TInteger.CreateChecked(radix.IntegerPow(logRadix)));

      return TInteger.Max(value - maxDistinct, TInteger.Zero);
    }
  }
}
