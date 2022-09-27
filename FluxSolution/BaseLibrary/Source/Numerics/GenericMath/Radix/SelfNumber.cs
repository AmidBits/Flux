#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether the number is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
        if (DigitSum(value, radix) + value == number)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible number that can make the number a self number in the specified radix.</summary>
    public static TSelf SelfNumberLowBound<TSelf>(TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

      var logRadix = IntegerLogFloor(number, AssertRadix(radix));
      var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (number / GenericMath.IntegerPow(radix, logRadix));
      return TSelf.Max(number - maxDistinct, TSelf.Zero);
    }
  }
}
#endif
