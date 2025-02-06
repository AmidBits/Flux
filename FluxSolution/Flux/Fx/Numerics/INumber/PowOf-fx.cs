namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rounds the <paramref name="number"/> to the closest power-of-<paramref name="radix"/> (i.e. of <paramref name="powOfTowardsZero"/> and <paramref name="powOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="number"/> resilient.</para>
    /// </summary>
    /// <param name="number">The value for which the nearest power-of-<paramref name="radix"/> of will be found.</param>
    /// <param name="radix">The radix (base) of the power-of-<paramref name="radix"/>.</param>
    /// <param name="unequal">Proper means nearest but <paramref name="unequal"/> to <paramref name="number"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="powOfTowardsZero">Outputs the power-of-<paramref name="radix"/> of that is closer to zero.</param>
    /// <param name="powOfAwayFromZero">Outputs the power-of-<paramref name="radix"/> of that is farther from zero.</param>
    /// <returns>The nearest two power-of-<paramref name="radix"/> as out parameters and the the nearest of those two is returned.</returns>
    public static TNumber PowOf<TNumber, TRadix>(this TNumber number, TRadix radix, bool unequal, UniversalRounding mode, out TNumber powOfTowardsZero, out TNumber powOfAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      powOfTowardsZero = number.PowOfTowardZero(radix, unequal);
      powOfAwayFromZero = number.PowOfAwayFromZero(radix, unequal);

      return number.RoundToNearest(mode, powOfTowardsZero, powOfAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest power-of-<paramref name="radix"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber PowOfAwayFromZero<TNumber, TRadix>(this TNumber number, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Quantities.Radix.AssertMember(radix)) is var r && TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.Abs(number) is var v && r.FastIntegerPow(v.FastIntegerLog(r, UniversalRounding.WholeTowardZero, out var _), UniversalRounding.WholeTowardZero, out var _) is var p && (p == v ? p : p * r) is var afz && (unequal || !TNumber.IsInteger(number)) && afz == v ? afz * r : afz, number);

    /// <summary>
    /// <para>Round a <paramref name="number"/> to the nearest power-of-<paramref name="radix"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber PowOfTowardZero<TNumber, TRadix>(this TNumber number, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Quantities.Radix.AssertMember(radix)) is var r && TNumber.IsZero(number)
      ? number
      : TNumber.CopySign(TNumber.Abs(number) is var v && r.FastIntegerPow(v.FastIntegerLog(r, UniversalRounding.WholeTowardZero, out var _), UniversalRounding.WholeTowardZero, out var _) is var p && (unequal && TNumber.IsInteger(number)) && p == v ? p / r : p, number);
  }
}
