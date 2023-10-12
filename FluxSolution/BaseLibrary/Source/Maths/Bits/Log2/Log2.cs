//namespace Flux
//{
//  public static partial class Bits
//  {
//#if NET7_0_OR_GREATER

//    public static TSelf Log2<TSelf>(this TSelf value)
//      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>
//      => TSelf.IsNegative(value)
//      ? -TSelf.Log2(TSelf.Abs(value))
//      : TSelf.Log2(value);

//#else

//    public static double Log2(this double value) => value < 0 ? -System.Math.Log2(System.Math.Abs(value)) : System.Math.Log2(value);

//    public static float Log2(this float value) => (float)Log2((double)value);

//#endif
//  }
//}
