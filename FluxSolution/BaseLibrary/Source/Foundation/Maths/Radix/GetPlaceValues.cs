//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
//    public static System.Span<System.Numerics.BigInteger> GetPlaceValues(System.Numerics.BigInteger value, int radix, bool skipZeroes = false)
//    {
//      var list = GetDigitsReversed(value, radix);

//      for (var index = 0; index < list.Length; index++)
//        if (list[index] != 0 || !skipZeroes)
//          list[index] *= System.Convert.ToInt32(System.Math.Pow(radix, index));

//      return list;
//    }

//    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
//    public static System.Span<int> GetPlaceValues(int value, int radix, bool skipZeroes = false)
//    {
//      var list = GetDigitsReversed(value, radix);

//      for (var index = 0; index < list.Length; index++)
//        if (list[index] != 0 || !skipZeroes)
//          list[index] *= System.Convert.ToInt32(System.Math.Pow(radix, index));

//      return list;
//    }
//    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
//    public static System.Span<long> GetPlaceValues(long value, int radix, bool skipZeroes = false)
//    {
//      var list = GetDigitsReversed(value, radix);

//      for (var index = 0; index < list.Length; index++)
//        if (list[index] != 0 || !skipZeroes)
//          list[index] *= System.Convert.ToInt32(System.Math.Pow(radix, index));

//      return list;
//    }
//  }
//}
