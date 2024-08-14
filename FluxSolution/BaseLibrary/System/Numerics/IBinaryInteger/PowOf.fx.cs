namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines if <paramref name="number"/> is a power of <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns false.</remarks>
    public static bool IsPowOf<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Quantities.Radix.AssertMember(radix);

      number = TSelf.Abs(number);

      if (radix == TSelf.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(number);

      if (number >= radix)
        while (TSelf.IsZero(number % radix))
          number /= radix;

      return number == TSelf.One;
    }

    /// <summary>Computes the away-from-zero power-of-<paramref name="radix"/> of <paramref name="number"/>.</summary>
    /// <param name="number">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="unequal">Whether the power-of-<paramref name="radix"/> must be unequal to <paramref name="number"/>.</param>
    /// <returns>The away-from-zero (ceiling, if <paramref name="number"/> is positive) power-of-<paramref name="radix"/> of <paramref name="number"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf PowOfAwayFromZero<TSelf>(this TSelf number, TSelf radix, bool unequal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(number) ? number : TSelf.CopySign(TSelf.Abs(number) is var v && radix.IntegerPow(IntegerLogTowardZero(v, radix)) is var ipow && (ipow == v ? ipow : ipow * radix) is var afz && unequal && afz == v ? afz * radix : afz, number);

    /// <summary>Computes the toward-zero power-of-<paramref name="radix"/> of <paramref name="number"/>.</summary>
    /// <param name="number">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="unequal">Whether the power-of-<paramref name="radix"/> must be unequal to <paramref name="number"/>.</param>
    /// <returns>The toward-zero (floor, if <paramref name="number"/> is positive) power-of-<paramref name="radix"/> of <paramref name="number"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf PowOfTowardZero<TSelf>(this TSelf number, TSelf radix, bool unequal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(number) ? number : TSelf.CopySign(TSelf.Abs(number) is var v && radix.IntegerPow(IntegerLogTowardZero(v, radix)) is var ipow && unequal && ipow == v ? ipow / radix : ipow, number);
  }
}
