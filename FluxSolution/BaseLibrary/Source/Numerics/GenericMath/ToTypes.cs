#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Converts from a floating point number to another number base, applying rounding the number.</summary>
    public static TNumber ConvertTo<TSelf, TNumber>(this TSelf number, out TNumber result, MidpointRounding mode = MidpointRounding.ToZero)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TNumber : System.Numerics.INumberBase<TNumber>
      => result = TNumber.CreateChecked(TSelf.Round(number, mode));

    /// <summary>PREVIEW! Converts from one type to another.</summary>
    public static TNumber ConvertTo<TSelf, TNumber>(this TSelf number, out TNumber result)
      where TSelf : System.Numerics.INumberBase<TSelf>
      where TNumber : System.Numerics.INumberBase<TNumber>
      => result = TNumber.CreateChecked(number);
  }
}
#endif
