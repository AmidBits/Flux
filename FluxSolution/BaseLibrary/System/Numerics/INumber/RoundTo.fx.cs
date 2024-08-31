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
    public static TNumber RoundToLogOfAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundAwayFromZero()), number);

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest log-of-<paramref name="radix"/> toward-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber RoundToLogOfTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.CreateChecked(double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Quantities.Radix.AssertMember(radix))).RoundTowardZero()), number);

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest <paramref name="multiple"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="multiple"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber RoundToMultipleOfAwayFromZero<TNumber>(this TNumber number, TNumber multiple, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(multiple, number) is var msv && number - (number % multiple) is var motz && (motz != number || (unequal || !TNumber.IsInteger(number))) ? motz + msv : motz;

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest <paramref name="multiple"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="multiple"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber RoundToMultipleOfTowardZero<TNumber>(this TNumber number, TNumber multiple, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      //=> MultipleOfTowardZero(value, multiple, unequal && TNumber.IsInteger(value)); // value - (value % multiple) is var motz && unequal && motz == value ? motz - TSelf.CopySign(multiple, value) : motz;
      => number - (number % multiple) is var motz && (unequal && TNumber.IsInteger(number)) && motz == number ? motz - TNumber.CopySign(multiple, number) : motz;

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest power-of-<paramref name="radix"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber RoundToPowOfAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Quantities.Radix.AssertMember(radix)) is var r && TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.Abs(number) is var v && r.FastPowAwayFromZero(v.FastLogOfTowardZero(r)) is var p && (p == v ? p : p * r) is var afz && (unequal || !TNumber.IsInteger(number)) && afz == v ? afz * r : afz, number);

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest power-of-<paramref name="radix"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber RoundToPowOfTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Quantities.Radix.AssertMember(radix)) is var r && TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.Abs(number) is var v && r.FastPowTowardZero(v.FastLogOfTowardZero(r)) is var p && (unequal && TNumber.IsInteger(number)) && p == v ? p / r : p, number);
  }
}
