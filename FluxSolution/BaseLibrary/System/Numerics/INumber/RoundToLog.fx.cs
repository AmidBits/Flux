namespace Flux
{
  // Unconditional specialty rounding.

  public static partial class Fx
  {
    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest log-of-<paramref name="radix"/> away-from-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TNumber RoundToLogAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Quantities.Radix.AssertMember(radix))).Envelop()), number);

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest log-of-<paramref name="radix"/> toward-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber RoundToLogTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.CreateChecked(double.Truncate(double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Quantities.Radix.AssertMember(radix))))), number);
  }
}
