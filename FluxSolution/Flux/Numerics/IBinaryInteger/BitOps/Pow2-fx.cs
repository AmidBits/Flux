using System.Text.RegularExpressions;

namespace Flux
{
  public static partial class BitOps
  {
    #region Pow2

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero, optionally <paramref name="unequal"/> if <paramref name="value"/> is already a power-of-2.</para>
    /// <example>
    /// <para>In order to process for example floating point types:</para>
    /// <code>TValue.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TValue.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TInteger Pow2AwayFromZero<TInteger>(this TInteger value, bool unequal)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TInteger.CopySign(TInteger.Abs(value) is var abs && TInteger.IsPow2(abs) ? (unequal ? abs << 1 : abs) : abs.MostSignificant1Bit() << 1, value);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero, optionally <paramref name="unequal"/> if <paramref name="value"/> is already a power-of-2.</para>
    /// <example>
    /// <para>In order to process for example a double:</para>
    /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TInteger Pow2TowardZero<TInteger>(this TInteger value, bool unequal)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TInteger.CopySign(TInteger.Abs(value) is var abs && TInteger.IsPow2(abs) && unequal ? abs >> 1 : abs.MostSignificant1Bit(), value);

    public static (TInteger pow2Tz, TInteger pow2Afz) Pow2<TInteger>(this TInteger value, bool unequal)
       where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsPow2(value))
      {
        if (unequal)
          return (value >> 1, value << 1);

        return (value, value);
      }

      var ms1b = value.MostSignificant1Bit();

      return (ms1b, ms1b << 1);
    }

#if INCLUDE_SWAR

    public static TInteger ScratchNextLargestPow2<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value.ScratchBitFoldRight() + TInteger.One;

#endif

    #endregion
  }
}
