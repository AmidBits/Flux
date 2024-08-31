namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastLogOfAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.INumber<TRadix>
      => TNumber.IsZero(number)
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundAwayFromZero()), number);

    public static TNumber FastLogOfTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.INumber<TRadix>
      => TNumber.IsZero(number)
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundTowardZero()), number);
  }
}
