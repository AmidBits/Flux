//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
//    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
//    public static int Pow(int value, int exponent)
//      => value >= 0 ? (int)Pow((uint)value, exponent) : throw new System.ArgumentOutOfRangeException(nameof(value));
//    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
//    /// <see cref="https://tkramesh.wordpress.com/2011/04/17/numerical-computations-in-c-exponentiation-by-repeated-squaring/"/>
//    public static long Pow(long value, int exponent)
//      => value >= 0 ? (long)Pow((ulong)value, exponent) : throw new System.ArgumentOutOfRangeException(nameof(value));

//    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
//    [System.CLSCompliant(false)]
//    public static uint Pow(uint value, int exponent)
//    {
//      if (exponent >= 1)
//      {
//        var result = 1U;
//        while (exponent > 0)
//          checked
//          {
//            if ((exponent & 1) == 1)
//              result *= value;
//            value *= value;
//            exponent >>= 1;
//          }
//        return result;
//      }
//      else if (exponent == 0) return 1;
//      else throw new System.ArgumentOutOfRangeException(nameof(exponent));
//    }
//    /// <summary>Calculates the power of the specified value and exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double x, and if pow has a 1 bit at that position, we multiply/accumulate that into the return value.</summary>
//    [System.CLSCompliant(false)]
//    public static ulong Pow(ulong value, int exponent)
//    {
//      if (exponent >= 1)
//      {
//        var result = 1UL;
//        while (exponent > 0)
//          checked
//          {
//            if ((exponent & 1) == 1)
//              result *= value;
//            value *= value;
//            exponent >>= 1;
//          }
//        return result;
//      }
//      else if (exponent == 0) return 1;
//      else throw new System.ArgumentOutOfRangeException(nameof(exponent));
//    }
//  }
//}
