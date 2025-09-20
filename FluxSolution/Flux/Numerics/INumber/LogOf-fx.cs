namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest log-of-<paramref name="radix"/> away-from-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TNumber LogOfAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value <= TNumber.Zero || radix <= TRadix.One
      ? TNumber.Zero
      : TNumber.CreateChecked(double.Log(double.CreateChecked(value), double.CreateChecked(radix)).Envelop());

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="logTowardsZero"></param>
    /// <param name="logAwayFromZero"></param>
    /// <returns></returns>
    public static TNumber LogOfNearest<TNumber, TRadix>(this TNumber value, TRadix radix, UniversalRounding mode, out TNumber logTowardsZero, out TNumber logAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      logAwayFromZero = value.LogOfAwayFromZero(radix);
      logTowardsZero = value.LogOfTowardZero(radix);

      return value.RoundToNearest(mode, logTowardsZero, logAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest log-of-<paramref name="radix"/> toward-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber LogOfTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value <= TNumber.Zero || radix <= TRadix.One
      ? TNumber.Zero
      : TNumber.CreateChecked(double.Truncate(double.Log(double.CreateChecked(TNumber.Abs(value)), double.CreateChecked(radix))));
  }
}
