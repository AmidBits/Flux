namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether <paramref name="number"/> using base <paramref name="radix"/> is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
        if (DigitSum(value, radix) + value == number)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible number that can make <paramref name="number"/> a self number using base <paramref name="radix"/>.</summary>
    public static TSelf SelfNumberLowBound<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (number <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

      var logRadix = NearestIntegerLogTowardsZero(number, AssertRadix(radix, out TSelf tradix), out TSelf _);
      var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (number / IntegerPow(tradix, logRadix));
      return TSelf.Max(number - maxDistinct, TSelf.Zero);
    }
  }
}
