namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastLogAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundAwayFromZero()), number);

    public static TNumber FastLogTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundTowardZero()), number);

    public static TNumber FastPowAwayFromZero<TNumber, TExponent>(this TNumber number, TExponent power)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      => TNumber.CopySign(TNumber.CreateChecked(double.Pow(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(power)).RoundAwayFromZero()), number);

    public static TNumber FastPowTowardZero<TNumber, TExponent>(this TNumber number, TExponent power)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      => TNumber.CopySign(TNumber.CreateChecked(double.Pow(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(power)).RoundTowardZero()), number);

    public static TNumber FastRootAwayFromZero<TNumber, TNth>(this TNumber number, TNth nth)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => TNumber.CopySign(TNumber.CreateChecked(double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth)).RoundAwayFromZero()), number);

    public static TNumber FastRootTowardZero<TNumber, TNth>(this TNumber number, TNth nth)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => TNumber.CopySign(TNumber.CreateChecked(double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth)).RoundTowardZero()), number);

    public static TNumber FastSqrtAwayFromZero<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(TNumber.CreateChecked(double.Sqrt(double.CreateChecked(TNumber.Abs(number))).RoundAwayFromZero()), number);

    public static TNumber FastSqrtTowardZero<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(TNumber.CreateChecked(double.Sqrt(double.CreateChecked(TNumber.Abs(number))).RoundTowardZero()), number);
  }
}
