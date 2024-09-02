namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastPow<TNumber, TExponent>(this TNumber number, TExponent power, out double pow)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.INumber<TExponent>
    {
      pow = double.Pow(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(power));

      return TNumber.CopySign(TNumber.CreateChecked(double.Round(pow)), number);
    }
  }
}
