//namespace Flux
//{
//  public static partial class Maths
//  {
//#if NET7_0_OR_GREATER

//    /// <summary>Indicates whether <paramref name="number"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
//    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
//    public static bool IsJumbled<TSelf>(this TSelf number, TSelf radix)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      AssertRadix(radix);

//      while (!TSelf.IsZero(number))
//      {
//        var remainder = number % radix;

//        number /= radix;

//        if (TSelf.IsZero(number))
//          break;
//        else if (TSelf.Abs((number % radix) - remainder) > TSelf.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
//          return false;
//      }

//      return true;
//    }

//#else

//    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
//    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
//    public static bool IsJumbled(this System.Numerics.BigInteger value, int radix)
//    {
//      while (value != 0)
//      {
//        value = System.Numerics.BigInteger.DivRem(value, radix, out var digit);

//        if (value == 0)
//          break;
//        else if (System.Numerics.BigInteger.Abs((value % radix) - digit) > 1) // If the difference to the digit is greater than 1, then the number cannot jumbled.
//          return false;
//      }

//      return true;
//    }

//    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
//    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
//    public static bool IsJumbled(this int value, int radix)
//    {
//      while (value != 0)
//      {
//        value = System.Math.DivRem(value, radix, out var digit);

//        if (value == 0)
//          break;
//        else if (System.Math.Abs((value % radix) - digit) > 1) // If the difference to the digit is greater than 1, then the number cannot jumbled.
//          return false;
//      }

//      return true;
//    }

//    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
//    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
//    public static bool IsJumbled(this long value, int radix)
//    {
//      while (value != 0)
//      {
//        value = System.Math.DivRem(value, radix, out var digit);

//        if (value == 0)
//          break;
//        else if (System.Math.Abs((value % radix) - digit) > 1) // If the difference to the digit is greater than 1, then the number cannot jumbled.
//          return false;
//      }

//      return true;
//    }

//#endif
//  }
//}
