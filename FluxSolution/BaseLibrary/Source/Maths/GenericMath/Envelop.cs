//namespace Flux
//{
//  public static partial class GenericMath
//  {
//#if NET7_0_OR_GREATER

//    /// <summary>Envelops a value, i.e. the opposite of truncating a value. Truncate cuts the fractional portion of a value yielding the integer that is closer to zero, whereas envelop essentially raises the fractional portion of a value yielding the integer that is farther away from zero.</summary>
//    public static TSelf Envelop<TSelf>(this TSelf value)
//      where TSelf : System.Numerics.IFloatingPoint<TSelf>
//      => TSelf.IsNegative(value) ? TSelf.Floor(value) : TSelf.Ceiling(value);

//#else

//    /// <summary>Envelops a value, i.e. the opposite of truncating a value. Truncate cuts the fractional portion of a value yielding the integer that is closer to zero, whereas envelop essentially raises the fractional portion of a value yielding the integer that is farther away from zero.</summary>
//    public static double Envelop(this double value)
//      => value < 0 ? System.Math.Floor(value) : System.Math.Ceiling(value);

//#endif
//  }
//}
