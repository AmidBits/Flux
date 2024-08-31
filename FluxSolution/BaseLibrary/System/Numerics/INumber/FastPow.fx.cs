namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastPowAwayFromZero<TNumber, TExponent>(this TNumber number, TExponent power)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.INumber<TExponent>
      => TNumber.CopySign(TNumber.CreateChecked(double.Pow(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(power)).RoundAwayFromZero()), number);

    public static TNumber FastPowTowardZero<TNumber, TExponent>(this TNumber number, TExponent power)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.INumber<TExponent>
      => TNumber.CopySign(TNumber.CreateChecked(double.Pow(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(power)).RoundTowardZero()), number);
  }
}
