#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether <paramref name="x"/> using base <paramref name="b"/> is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
    public static bool IsSelfNumber<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var value = SelfNumberLowBound(x, b); value < x; value++)
        if (DigitSum(value, b) + value == x)
          return false;

      return true;
    }

    /// <summary>Returns the minimum possible number that can make <paramref name="x"/> a self number using base <paramref name="b"/>.</summary>
    public static TSelf SelfNumberLowBound<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (x <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(x));

      var logRadix = IntegerLogFloor(x, AssertRadix(b));
      var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (x / GenericMath.IntegerPow(b, logRadix));
      return TSelf.Max(x - maxDistinct, TSelf.Zero);
    }
  }
}
#endif
