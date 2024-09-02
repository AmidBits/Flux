namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastSqrtAwayFromZero<TNumber>(this TNumber number, out double sqrt)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      sqrt = double.Sqrt(double.CreateChecked(TNumber.Abs(number)));

      return TNumber.CopySign(TNumber.CreateChecked(sqrt.RoundAwayFromZero()), number);
    }

    public static TNumber FastSqrtTowardZero<TNumber>(this TNumber number, out double sqrt)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      sqrt = double.Sqrt(double.CreateChecked(TNumber.Abs(number)));

      return TNumber.CopySign(TNumber.CreateChecked(sqrt.RoundTowardZero()), number);
    }
  }
}
