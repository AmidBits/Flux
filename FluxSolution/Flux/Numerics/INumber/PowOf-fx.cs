namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber PowOfAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
      ? value
      : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(r, UniversalRounding.WholeTowardZero, out var _), UniversalRounding.WholeTowardZero, out var _) is var p && (p == v ? p : p * r) is var afz && (unequal || !TNumber.IsInteger(value)) && afz == v ? afz * r : afz, value);

    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the closest power-of-<paramref name="radix"/> (i.e. of <paramref name="powOfTowardsZero"/> and <paramref name="powOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
    /// </summary>
    /// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> of will be found.</param>
    /// <param name="radix">The radix (base) of the power-of-<paramref name="radix"/>.</param>
    /// <param name="unequal">Proper means nearest but <paramref name="unequal"/> to <paramref name="value"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="powOfTowardsZero">Outputs the power-of-<paramref name="radix"/> of that is closer to zero.</param>
    /// <param name="powOfAwayFromZero">Outputs the power-of-<paramref name="radix"/> of that is farther from zero.</param>
    /// <returns>The nearest two power-of-<paramref name="radix"/> as out parameters and the the nearest of those two is returned.</returns>
    public static TNumber PowOf<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal, UniversalRounding mode, out TNumber powOfTowardsZero, out TNumber powOfAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      powOfTowardsZero = value.PowOfTowardZero(radix, unequal);
      powOfAwayFromZero = value.PowOfAwayFromZero(radix, unequal);

      return value.RoundToNearest(mode, powOfTowardsZero, powOfAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber PowOfTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
      ? value
      : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(r, UniversalRounding.WholeTowardZero, out var _), UniversalRounding.WholeTowardZero, out var _) is var p && (unequal && TNumber.IsInteger(value)) && p == v ? p / r : p, value);
  }
}
