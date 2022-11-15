//using System;
//using System.Linq;

//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Returns the digits (as numbers) of a value.</summary>
//    public static System.Span<System.Numerics.BigInteger> GetDigits(System.Numerics.BigInteger value, int radix)
//    {
//      AssertRadix(radix);

//      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

//      while (value != 0)
//      {
//        list.Insert(0, value % radix);
//        value /= radix;
//      }

//      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);

//      //var span = GetDigitsReversed(value, radix);
//      //span.Reverse();
//      //return span;
//    }

//    /// <summary>Returns the digits (as numbers) of a value.</summary>
//    public static System.Span<int> GetDigits(int value, int radix)
//    {
//      AssertRadix(radix);

//      var list = new System.Collections.Generic.List<int>();

//      while (value != 0)
//      {
//        list.Insert(0, value % radix);
//        value /= radix;
//      }

//      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);

//      //var span = GetDigitsReversed(value, radix);
//      //span.Reverse();
//      //return span;
//    }
//    /// <summary>Returns the digits (as numbers) of a value.</summary>
//    public static System.Span<long> GetDigits(long value, int radix)
//    {
//      AssertRadix(radix);

//      var list = new System.Collections.Generic.List<long>();

//      while (value != 0)
//      {
//        list.Insert(0, value % radix);
//        value /= radix;
//      }

//      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);

//      //var span = GetDigitsReversed(value, radix);
//      //span.Reverse();
//      //return span;
//    }
//  }
//}
