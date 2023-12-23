//namespace Flux
//{
//  public static partial class Maths
//  {
//#if NET7_0_OR_GREATER

//    /// <summary>Returns whether <paramref name="number"/> using base <paramref name="radix"/> is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
//    public static bool IsSelfNumber<TSelf>(this TSelf number, TSelf radix)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
//        if (DigitSum(value, radix) + value == number)
//          return false;

//      return true;
//    }

//    /// <summary>Returns the minimum possible number that can make <paramref name="value"/> a self number using base <paramref name="radix"/>.</summary>
//    public static TSelf SelfNumberLowBound<TSelf>(this TSelf value, TSelf radix)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (value <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

//      var logRadix = TSelf.CreateChecked(IntegerLogFloor(value, radix));
//      var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (value / IntegerPow(radix, logRadix));

//      return TSelf.Max(value - maxDistinct, TSelf.Zero);
//    }

//#else

//    /// <summary>Returns whether the number is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
//    public static bool IsSelfNumber(System.Numerics.BigInteger number, int radix)
//    {
//      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
//        if (DigitSum(value, radix) + value == number)
//          return false;

//      return true;
//    }

//    /// <summary>Returns whether the number is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
//    public static bool IsSelfNumber(int number, int radix)
//    {
//      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
//        if (DigitSum(value, radix) + value == number)
//          return false;

//      return true;
//    }
//    /// <summary>Returns whether the number is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
//    public static bool IsSelfNumber(long number, int radix)
//    {
//      for (var value = SelfNumberLowBound(number, radix); value < number; value++)
//        if (DigitSum(value, radix) + value == number)
//          return false;

//      return true;
//    }

//    /// <summary>Returns the minimum possible number that can make the number a self number in the specified radix.</summary>
//    public static System.Numerics.BigInteger SelfNumberLowBound(System.Numerics.BigInteger number, int radix)
//    {
//      if (number <= 0) throw new System.ArgumentOutOfRangeException(nameof(number));

//      var logRadix = (int)System.Numerics.BigInteger.Log(number, (int)AssertRadix(radix));
//      var maxDistinct = (9 * logRadix) + (number / System.Numerics.BigInteger.Pow(radix, logRadix));
//      return System.Numerics.BigInteger.Max(number - maxDistinct, System.Numerics.BigInteger.Zero);
//    }

//    /// <summary>Returns the minimum possible number that can make the number a self number in the specified radix.</summary>
//    public static int SelfNumberLowBound(int number, int radix)
//    {
//      if (number <= 0) throw new System.ArgumentOutOfRangeException(nameof(number));

//      var logRadix = (int)System.Math.Log(number, (int)AssertRadix(radix));
//      var maxDistinct = (9 * logRadix) + (number / (int)System.Math.Pow(radix, logRadix));
//      return System.Math.Max(number - maxDistinct, 0);
//    }

//    /// <summary>Returns the minimum possible number that can make the number a self number in the specified radix.</summary>
//    public static long SelfNumberLowBound(long number, int radix)
//    {
//      if (number <= 0) throw new System.ArgumentOutOfRangeException(nameof(number));

//      var logRadix = (long)System.Math.Log(number, (int)AssertRadix(radix));
//      var maxDistinct = (9 * logRadix) + (number / (long)System.Math.Pow(radix, logRadix));
//      return System.Math.Max(number - maxDistinct, 0L);
//    }

//#endif
//  }
//}
