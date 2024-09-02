namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns false.</remarks>
    public static bool IsPowOf<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      value = TValue.Abs(value);

      if (rdx == TValue.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TValue.IsPow2(value);

      if (value >= rdx)
        while (TValue.IsZero(value % rdx))
          value /= rdx;

      return value == TValue.One;
    }

    ///// <summary>Computes the away-from-zero power-of-<paramref name="radix"/> of <paramref name="value"/>.</summary>
    ///// <param name="value">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    ///// <param name="radix">The power of alignment.</param>
    ///// <param name="unequal">Whether the power-of-<paramref name="radix"/> must be unequal to <paramref name="value"/>.</param>
    ///// <returns>The away-from-zero (ceiling, if <paramref name="value"/> is positive) power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static TValue PowOfAwayFromZero<TValue>(this TValue value, TValue radix, bool unequal)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //  => TValue.IsZero(value)
    //  ? value
    //  : TValue.CopySign(TValue.Abs(value) is var v && radix.IntegerPow(IntegerLogTowardZero(v, radix)) is var ipow && (ipow == v ? ipow : ipow * radix) is var afz && unequal && afz == v ? afz * radix : afz, value);

    ///// <summary>Computes the toward-zero power-of-<paramref name="radix"/> of <paramref name="value"/>.</summary>
    ///// <param name="value">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    ///// <param name="radix">The power of alignment.</param>
    ///// <param name="unequal">Whether the power-of-<paramref name="radix"/> must be unequal to <paramref name="value"/>.</param>
    ///// <returns>The toward-zero (floor, if <paramref name="value"/> is positive) power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static TValue PowOfTowardZero<TValue>(this TValue value, TValue radix, bool unequal)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //  => TValue.IsZero(value)
    //  ? value
    //  : TValue.CopySign(TValue.Abs(value) is var v && radix.IntegerPow(IntegerLogTowardZero(v, radix)) is var ipow && unequal && ipow == v ? ipow / radix : ipow, value);
  }
}
