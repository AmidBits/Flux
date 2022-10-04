#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="x"/> with the sign of <paramref name="y"/>.</summary>
    public static TValueSign CopySign<TAbsoluteValue, TValueSign>(this TAbsoluteValue x, TValueSign y)
      where TAbsoluteValue : System.Numerics.INumber<TAbsoluteValue>
      where TValueSign : System.Numerics.ISignedNumber<TValueSign>
      => TValueSign.CreateChecked(TValueSign.IsNegative(y) ? -TAbsoluteValue.Abs(x) : TAbsoluteValue.Abs(x));
  }
}
#endif
