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
    /// <param name="log"></param>
    /// <param name="reciprocal"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber FastLogOfAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix, out double log)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.INumber<TRadix>
    {
      if (TNumber.IsZero(number)) throw new System.ArgumentOutOfRangeException(nameof(number));

      log = double.Log(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(Quantities.Radix.AssertMember(radix)));

      if (double.IsInteger(log)) log++; // If log is at a radix boundary, we need to adjust it. This is where it gets wrong with using floating point.

      return TNumber.CopySign(TNumber.CreateChecked(log.RoundAwayFromZero()), number);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="log"></param>
    /// <param name="reciprocal"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber FastLogOfTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix, out double log)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.INumber<TRadix>
    {
      if (TNumber.IsZero(number)) throw new System.ArgumentOutOfRangeException(nameof(number));

      log = double.Log(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(Quantities.Radix.AssertMember(radix)));

      return TNumber.CopySign(TNumber.CreateChecked(log.RoundTowardZero()), number);
    }
  }
}
