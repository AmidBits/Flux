namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns whether <paramref name="value"/> using base <paramref name="radix"/> is a self value. A self value, Colombian value or Devlali value, in a given value base (radix) is a natural value that cannot be written as the sum of any other natural value n and the individual digits of n.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      for (var sn = SelfNumberLowBound(value, radix); sn < value; sn++)
        if (DigitSum(sn, radix) + sn == value)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible value that can make <paramref name="value"/> a self value using base <paramref name="radix"/>.</summary>
    public static TValue SelfNumberLowBound<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (value <= TValue.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var logRadix = TValue.CreateChecked(IntegerLogTowardZero(value, radix));
      var maxDistinct = (TValue.CreateChecked(9) * logRadix) + (value / radix.IntegerPow(logRadix));

      return TValue.Max(value - maxDistinct, TValue.Zero);
    }
  }
}
