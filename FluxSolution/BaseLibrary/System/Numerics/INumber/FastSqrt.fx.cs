namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastSqrtAwayFromZero<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(TNumber.CreateChecked(double.Sqrt(double.CreateChecked(TNumber.Abs(number))).RoundAwayFromZero()), number);

    public static TNumber FastSqrtTowardZero<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(TNumber.CreateChecked(double.Sqrt(double.CreateChecked(TNumber.Abs(number))).RoundTowardZero()), number);
  }
}
