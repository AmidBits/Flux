namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastRootNthAwayFromZero<TNumber, TNth>(this TNumber number, TNth nth, out double rootN)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      rootN = double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth));

      return TNumber.CopySign(TNumber.CreateChecked(rootN.RoundAwayFromZero()), number);
    }

    public static TNumber FastRootNthTowardZero<TNumber, TNth>(this TNumber number, TNth nth, out double rootN)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      rootN = double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth));

      return TNumber.CopySign(TNumber.CreateChecked(rootN.RoundTowardZero()), number);
    }
  }
}
