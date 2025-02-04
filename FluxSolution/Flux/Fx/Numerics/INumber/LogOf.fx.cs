namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="logTowardsZero"></param>
    /// <param name="logAwayFromZero"></param>
    /// <returns></returns>
    public static TNumber LogOf<TNumber, TRadix>(this TNumber number, TRadix radix, UniversalRounding mode, out TNumber logTowardsZero, out TNumber logAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      logAwayFromZero = number.LogOfAwayFromZero(radix);
      logTowardsZero = number.LogOfTowardZero(radix);

      return number.RoundToNearest(mode, logTowardsZero, logAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest log-of-<paramref name="radix"/> away-from-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TNumber LogOfAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix)
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
    public static TNumber LogOfTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.CreateChecked(double.Truncate(double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Quantities.Radix.AssertMember(radix))))), number);
  }
}
