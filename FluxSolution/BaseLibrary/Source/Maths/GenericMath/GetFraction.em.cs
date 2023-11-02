//namespace Flux
//{
//  public static partial class NumericsExtensionMethods
//  {
//#if NET7_0_OR_GREATER

//    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
//    public static TSelf GetFraction<TSelf, TWhole>(this TSelf source, out TWhole integer)
//      where TSelf : System.Numerics.IFloatingPoint<TSelf>
//      where TWhole : System.Numerics.IBinaryInteger<TWhole>
//    {
//      var whole = TSelf.Truncate(source);

//      integer = TWhole.CreateChecked(whole);

//      return source - whole;
//    }

//#else

//    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
//    public static double GetFraction(this double source, out System.Numerics.BigInteger integer)
//    {
//      var whole = System.Math.Truncate(source);

//      integer = new System.Numerics.BigInteger(whole);

//      return source - whole;
//    }

//#endif
//  }
//}
