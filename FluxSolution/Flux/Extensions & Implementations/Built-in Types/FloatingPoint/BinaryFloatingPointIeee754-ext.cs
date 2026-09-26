namespace Flux
{
  public static partial class BinaryFloatingPointIeee754Extensions
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IBinaryFloatingPointIeee754<TFloat>
    {
      #region Ieee754 Binary Representation functions

      public static (int IeeeExponentBitWidth, int IeeeFractionBitWidth) GetIeeeMeta()
        => (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? (11, 52)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? (8, 23)
        : (typeof(TFloat) == typeof(System.Half))
        ? (5, 10)
        : throw new System.NotImplementedException();

      [System.CLSCompliant(false)]
      public static (ulong IeeeSignBit, ulong IeeeBiasedExponent, ulong IeeeFraction) ToIeeeBinary(TFloat value, out int ieeeExponentBitWidth, out int ieeeFractionBitWidth)
      {
        ulong ieeeBinary;

        if (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          ieeeBinary = System.BitConverter.DoubleToUInt64Bits(double.CreateChecked(value));
        else if (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          ieeeBinary = System.BitConverter.SingleToUInt32Bits(float.CreateChecked(value));
        else if (typeof(TFloat) == typeof(System.Half))
          ieeeBinary = System.BitConverter.HalfToUInt16Bits(System.Half.CreateChecked(value));
        else throw new System.NotImplementedException();

        (ieeeExponentBitWidth, ieeeFractionBitWidth) = GetIeeeMeta<TFloat>();

        var signBit = (ieeeBinary >>> (ieeeFractionBitWidth + ieeeExponentBitWidth)) & 1;
        var biasedExponent = (ieeeBinary >>> ieeeFractionBitWidth) & ((1UL << ieeeExponentBitWidth) - 1);
        var fraction = ieeeBinary & ((1UL << ieeeFractionBitWidth) - 1);

        return (signBit, biasedExponent, fraction);
      }

      [System.CLSCompliant(false)]
      public static TFloat FromIeeeBinary(ulong ieeeSignBit, ulong ieeeBiasedExponent, ulong ieeeFraction)
      {
        var (ieeeExponentBitWidth, ieeeFractionBitWidth) = GetIeeeMeta<TFloat>();

        var ieeeBinary =
          (ieeeSignBit << (ieeeExponentBitWidth + ieeeFractionBitWidth)) |
          (ieeeBiasedExponent << ieeeFractionBitWidth) |
          ieeeFraction;

        TFloat value;

        if (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          value = TFloat.CreateChecked(System.BitConverter.UInt64BitsToDouble(ieeeBinary));
        else if (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          value = TFloat.CreateChecked(System.BitConverter.UInt32BitsToSingle((uint)ieeeBinary));
        else if (typeof(TFloat) == typeof(System.Half))
          value = TFloat.CreateChecked(System.BitConverter.UInt16BitsToHalf((ushort)ieeeBinary));
        else throw new System.NotImplementedException();

        return value;
      }

      public static TFloat ScaleB(TFloat value, int n)
      {
        var (ieeeSignBit, ieeeBiasedExponent, ieeeFraction) = ToIeeeBinary(value, out var ieeeExponentBitWidth, out var ieeeFractionBitWidth);

        var ieeeExponentBias = (1 << (ieeeExponentBitWidth - 1)) - 1;
        var ieeeExponentMax = (1 << ieeeExponentBitWidth) - 1;

        var eminSub = (1L - ieeeExponentBias) - (ieeeFractionBitWidth - 1);

        if (ieeeBiasedExponent == (ulong)ieeeExponentMax)
          return value;

        if (ieeeBiasedExponent == 0) // Handle zero and subnormals.
        {
          if (ieeeFraction == 0)
            return FromIeeeBinary<TFloat>(ieeeSignBit, 0, 0);

          var unbiased = 1L - ieeeExponentBias; // Subnormal: unbiased exponent = 1 - bias.
          var newUnbiased = unbiased + n; // Apply scaling.

          if (newUnbiased < eminSub) // Underflow → zero.
            return FromIeeeBinary<TFloat>(ieeeSignBit, 0, 0);

          if (newUnbiased >= 1) // Overflow → becomes normal
          {
            var newBiased = (ulong)(newUnbiased + ieeeExponentBias);
            return FromIeeeBinary<TFloat>(ieeeSignBit, newBiased, ieeeFraction);
          }

          var shift = newUnbiased - (1 - ieeeExponentBias); // Still subnormal, shift fraction right or left depending on exponent change.

          if (shift > 0)
            ieeeFraction <<= (int)shift;
          else
            ieeeFraction >>= (int)(-shift);

          return FromIeeeBinary<TFloat>(ieeeSignBit, 0, ieeeFraction);
        }
        else
        {
          var unbiased = (long)ieeeBiasedExponent - ieeeExponentBias; // Normal exponent.
          var newUnbiased = unbiased + n;

          if (newUnbiased > ieeeExponentBias) // Overflow → Infinity.
            return FromIeeeBinary<TFloat>(ieeeSignBit, (ulong)ieeeExponentMax, 0);

          if (newUnbiased < eminSub) // Underflow beyond subnormal range → ±0
            return FromIeeeBinary<TFloat>(ieeeSignBit, 0, 0);

          var eminNormal = 1L - ieeeExponentBias;

          if (newUnbiased >= eminNormal) // If still within normal range, just update exponent.
          {
            var newExponent = (ulong)(newUnbiased + ieeeExponentBias); // Recompute biased exponent.

            return FromIeeeBinary<TFloat>(ieeeSignBit, newExponent, ieeeFraction);
          }

          // Otherwise: we’ve crossed into the subnormal range.
          // We need to convert the normal significand into a subnormal fraction.

          // For a normal:
          //   value = sign * (1.fraction) * 2^unbiased
          // For a subnormal:
          //   value = sign * (0.fraction_sub) * 2^(eminNormal)
          //
          // So we need:
          //   (0.fraction_sub) = (1.fraction) * 2^(newUnbiased - eminNormal)

          var shift = newUnbiased - eminNormal; // this is negative or zero

          // Start from the implicit leading 1 plus fraction
          // Represented as an integer with (fractionBits + 1) bits of precision.
          var fullSignificand = (1UL << ieeeFractionBitWidth) | ieeeFraction;

          if (shift < 0)
          {
            // Shift right to move into subnormal range
            fullSignificand >>= (int)(-shift);
          }
          // If shift == 0, we’re exactly at eminNormal: still normal, but we already
          // handled newUnbiased >= eminNormal above, so we won’t hit this case.

          // Now drop the implicit 1; subnormal has exponent field = 0
          var subnormalFraction = fullSignificand & ((1UL << ieeeFractionBitWidth) - 1);

          return FromIeeeBinary<TFloat>(ieeeSignBit, 0, subnormalFraction);
        }
      }

      public static (int Sign, int Exponent, long Fraction, TFloat Significand) ExtractIeeeBinaryFields(TFloat value, out TFloat recomputedValue)
      {
        var (ieeeSignBit, ieeeBiasedExponent, ieeeFraction) = ToIeeeBinary(value, out var ieeeExponentBitWidth, out var ieeeFractionBitWidth);

        var ieeeExponentBias = (1L << (ieeeExponentBitWidth - 1)) - 1;

        var significand = TFloat.CreateChecked(ieeeFraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(ieeeFractionBitWidth));
        if (ieeeBiasedExponent != 0UL) significand++;
        var unbiasedExponent = (int)ieeeBiasedExponent - (int)ieeeExponentBias;

        recomputedValue = (ieeeSignBit == 0UL ? significand : -significand) * TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(unbiasedExponent));

        var signum = TFloat.IsZero(value) ? 0 : (ieeeSignBit == 0UL ? 1 : -1);

        return (signum, unbiasedExponent, (long)ieeeFraction, significand);
      }

      #endregion

      #region GetParts

      /// <summary>
      /// <para>Get the integral part and the fractional part of a <see cref="System.Double"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
      /// </summary>
      /// <returns>
      /// <para>The integral (integer) part and the fractional part of a 64-bit floating point value.</para>
      /// </returns>
      public static (TFloat IntegralPart, TFloat FractionalPart) GetParts(TFloat value)
      {
        var integralPart = TFloat.Truncate(value);
        var fractionalPart = value - integralPart;

        return (integralPart, fractionalPart);
      }

      #endregion


    }
  }
}
