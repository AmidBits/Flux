namespace Flux
{
  public static partial class HalfExtensions
  {
    extension(System.Half)
    {
      /// <summary>
      /// <para>The base epsilon 1e‑3 → about the scale where half‑precision (binary16) stops reliably distinguishing differences.</para>
      /// <para>A tolerance for “close enough” comparisons — the smallest meaningful difference before rounding noise dominates.</para>
      /// </summary>
      public static System.Half EngineeringEpsilon => System.Half.CreateChecked(1e-3);

      public static System.Half MachineEpsilon => (System.Half)0.0009765625;

      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Half"/> without losing precision is <c>16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Half"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers up to 16,777,216 = <c>(1 &lt;&lt; 24)</c> = 2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static System.Half MaxExactInteger => System.Half.CreateChecked(+2048);

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Half"/> without losing precision is <c>-16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Half"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers down to -16,777,216 = <c>-(1 &lt;&lt; 24)</c> = -2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static System.Half MinExactInteger => System.Half.CreateChecked(-2048);

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a <see cref="System.Half"/>.</para>
      /// </summary>
      public static System.Half MaxExactPrimeNumber => System.Half.CreateChecked(2039);

      #region HalfUlp/TryGet

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Half"/> value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static System.Half HalfUlp(System.Half value)
        => System.Half.IsNaN(value)
        ? System.Half.NaN
        : System.Half.IsInfinity(value)
        ? System.Half.PositiveInfinity
        : System.Half.BitIncrement(value) - value;

      /// <summary>
      /// <para>Try to get the unit in the last place (ULP) of a <see cref="System.Half"/> value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp16"></param>
      /// <returns></returns>
      public static bool TryGetHalfUlp(System.Half value, out System.Half ulp16)
      {
        ulp16 = HalfUlp(value);

        return System.Half.IsFinite(ulp16);
      }

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Half"/> value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp16FromExponent"></param>
      /// <returns></returns>
      public static bool TryGetHalfUlp(System.Half value, out System.Half ulp16FromBitIncrement, out System.Half ulp16FromExponent)
      {
        var hasMeaningfulUlp = TryGetHalfUlp(value, out ulp16FromBitIncrement);

        if (hasMeaningfulUlp)
        {
          var bits = System.BitConverter.HalfToUInt16Bits(value);
          var exponent = (bits >> 10) & 0x1F; // 5-bit exponent

          if (exponent == 0) // Subnormal: ULP = 2^-24
          {
            ulp16FromExponent = System.Half.BitIncrement((System.Half)0);
          }
          else // Normal: ULP = 2^(e - 10)
          {
            var unbiased = exponent - 15; // bias = 15
            var biased = (unbiased - 10) + 15; // re-bias for ULP exponent

            var ulpBits = (ushort)(biased << 10);
            ulp16FromExponent = System.BitConverter.UInt16BitsToHalf(ulpBits);
          }
        }
        else
          ulp16FromExponent = ulp16FromBitIncrement;

        return hasMeaningfulUlp;
      }

      #endregion

      #region HalfUlpDistance

      public static short HalfUlpDistance(System.Half left, System.Half right)
      {
        var a = BitConverter.HalfToInt16Bits(left);
        var b = BitConverter.HalfToInt16Bits(right);

        if (a < 0)
          a = (short)(short.MinValue - a);

        if (b < 0)
          b = (short)(short.MinValue - b);

        return short.Abs((short)(a - b));
      }

      #endregion
    }
  }
}
