namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastRootNthAwayFromZero<TNumber, TNth>(this TNumber number, TNth nth)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => TNumber.CopySign(TNumber.CreateChecked(double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth)).RoundAwayFromZero()), number);

    public static TNumber FastRootNthTowardZero<TNumber, TNth>(this TNumber number, TNth nth)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => TNumber.CopySign(TNumber.CreateChecked(double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth)).RoundTowardZero()), number);

  }
}
