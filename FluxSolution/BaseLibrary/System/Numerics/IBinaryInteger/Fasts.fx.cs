namespace Flux
{
  public static partial class Fx
  {
    public static TNumber FastLogAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastLogAwayFromZero(radix)
      : TNumber.CreateChecked(double.Log(double.CreateChecked(number), int.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundAwayFromZero());

    public static TNumber FastLogTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? throw new System.ArgumentOutOfRangeException(nameof(number))
      : TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastLogTowardZero(radix)
      : TNumber.CreateChecked(double.Log(double.CreateChecked(number), int.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundTowardZero());

    public static TNumber FastPowAwayFromZero<TNumber, TExponent>(this TNumber number, TExponent power)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      => TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastPowAwayFromZero(power)
      : TNumber.CreateChecked(double.Pow(double.CreateChecked(number), int.CreateChecked(power)).RoundAwayFromZero());

    public static TNumber FastPowTowardZero<TNumber, TExponent>(this TNumber number, TExponent power)
      where TNumber : System.Numerics.INumber<TNumber>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      => TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastPowTowardZero(power)
      : TNumber.CreateChecked(double.Pow(double.CreateChecked(number), int.CreateChecked(power)).RoundTowardZero());

    public static TNumber FastRootAwayFromZero<TNumber, TNth>(this TNumber number, TNth nth)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastRootAwayFromZero(nth)
      : TNumber.CreateChecked(double. RootN(double.CreateChecked(number), int.CreateChecked(nth)).RoundAwayFromZero());

    public static TNumber FastRootTowardZero<TNumber, TNth>(this TNumber number, TNth nth)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastRootTowardZero(nth)
      : TNumber.CreateChecked(double.RootN(double.CreateChecked(number), int.CreateChecked(nth)).RoundTowardZero());

    public static TNumber FastSqrtAwayFromZero<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastSqrtAwayFromZero()
      : TNumber.CreateChecked(double.Sqrt(double.CreateChecked(number)).RoundAwayFromZero());

    public static TNumber FastSqrtTowardZero<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsNegative(number)
      ? -TNumber.Abs(number).FastSqrtTowardZero()
      : TNumber.CreateChecked(double.Sqrt(double.CreateChecked(number)).RoundTowardZero());
  }
}
