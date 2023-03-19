namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether <paramref name="number"/> using base <paramref name="radix"/> is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
        if (DigitSum(value, radix) + value == number)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible number that can make <paramref name="value"/> a self number using base <paramref name="radix"/>.</summary>
    public static TSelf SelfNumberLowBound<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (value <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

      var logRadix = LocateIntegerLogTz(value, radix);
      var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (value / IntegerPow(radix, logRadix));

      return TSelf.Max(value - maxDistinct, TSelf.Zero);
    }
  }
}
