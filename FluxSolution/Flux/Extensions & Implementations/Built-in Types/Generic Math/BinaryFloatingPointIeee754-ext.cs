namespace Flux
{
  public static partial class BinaryFloatingPointIeee754
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IBinaryFloatingPointIeee754<TFloat>
    {
      #region Ieee754Binary functions

      /// <summary>
      /// <para>Analyze IEEE 754 binary components and return the sign, unbiased exponent, significand, classification flags, and a reconstructed TFloat value.</para>
      /// </summary>
      /// <remarks>Exponent and fraction widths are obtained from Ieee754BinaryMeta<TFloat>(). A biased exponent equal to its maximum denotes NaN or Infinity; a biased exponent of zero denotes zero or a subnormal. The reconstructor returns exact NaN/Infinity values and rebuilds finite values from the computed components.</remarks>
      /// <param name="signBit">Sign bit from the IEEE 754 encoding (0 for positive, 1 for negative).</param>
      /// <param name="biasedExponent">Biased exponent field from the IEEE 754 encoding.</param>
      /// <param name="fraction">Fraction (mantissa) field from the IEEE 754 encoding.</param>
      /// <param name="recomputedValue">Outputs the reconstructed TFloat value; preserves NaN and Infinity exactly or yields the finite numeric reconstruction.</param>
      /// <returns>Tuple with elements: Sign (int): +1 for positive, -1 for negative, or 0 for signed zero; UnbiasedExponent (long): exponent with bias removed; FullSignificand (ulong): integer significand (includes implicit leading one for normals); Significand (TFloat): normalized significand suitable for human interpretation; IsInfinity (bool), IsNaN (bool), IsNormal (bool), IsSubnormal (bool), IsZero (bool): classification flags.</returns>
      [System.CLSCompliant(false)]
      public static (int Sign, long UnbiasedExponent, ulong FullSignificand, TFloat Significand, bool IsInfinity, bool IsNaN, bool IsNormal, bool IsSubnormal, bool IsZero) AnalyzeIeee754Binary(ulong signBit, ulong biasedExponent, ulong fraction, out TFloat recomputedValue)
      {
        var (ieeeExponentBitWidth, ieeeFractionBitWidth) = Ieee754BinaryMeta<TFloat>();

        var ieeeExponentBias = (1L << (ieeeExponentBitWidth - 1)) - 1;
        var ieeeExponentMax = (1UL << ieeeExponentBitWidth) - 1;

        var isInfinity = biasedExponent == ieeeExponentMax && fraction == 0;
        var isNaN = biasedExponent == ieeeExponentMax && fraction != 0;
        var isNormal = biasedExponent != 0 && biasedExponent != ieeeExponentMax;
        var isSubnormal = biasedExponent == 0 && fraction != 0;
        var isZero = biasedExponent == 0 && fraction == 0;

        long unbiasedExponent;
        ulong fullSignificand; // For IEEE algorithms.
        TFloat significand; // Human-readable normalized significand.

        if (biasedExponent == 0) // Zero or subnormal.
        {
          unbiasedExponent = 1 - ieeeExponentBias;
          fullSignificand = fraction;
          significand = TFloat.CreateChecked(fraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(ieeeFractionBitWidth));
        }
        else // Normal value, or raw decomposition of Infinity/NaN.
        {
          unbiasedExponent = (long)biasedExponent - ieeeExponentBias;
          fullSignificand = (1UL << ieeeFractionBitWidth) | fraction;
          significand = TFloat.One + TFloat.CreateChecked(fraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(ieeeFractionBitWidth));
        }

        var sign = isZero ? 0 : (signBit == 0 ? 1 : -1);

        recomputedValue = isNaN ? TFloat.NaN : isInfinity ? (sign > 0 ? TFloat.PositiveInfinity : TFloat.NegativeInfinity) // Preserve exact NaN/Infinity value.
          : (sign == 0 ? TFloat.Zero : TFloat.CreateChecked(sign) * TFloat.CreateChecked(fullSignificand) * TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(unbiasedExponent - ieeeFractionBitWidth)));

        return (sign, unbiasedExponent, fullSignificand, significand, isInfinity, isNaN, isNormal, isSubnormal, isZero);
      }

      /// <summary>
      /// <para>Creates a TFloat from IEEE 754 binary fields: sign, biased exponent, and fraction.</para>
      /// </summary>
      /// <remarks>Combines the fields into an IEEE 754 bit pattern and converts it to TFloat using BitConverter helpers and TFloat.CreateChecked. Encodings for normals, subnormals, infinities, and NaNs follow IEEE 754. Field widths must match those returned by Ieee754BinaryMeta<TFloat>().</remarks>
      /// <param name="signBit">Sign bit (0 for positive, 1 for negative) placed at the most-significant bit of the IEEE 754 representation.</param>
      /// <param name="biasedExponent">Biased exponent field encoded with the exponent width returned by Ieee754BinaryMeta<TFloat>().</param>
      /// <param name="fraction">Fraction (significand) field encoded with the fraction width returned by Ieee754BinaryMeta<TFloat>().</param>
      /// <returns>A TFloat whose bit pattern corresponds to the provided IEEE 754 fields.</returns>
      /// <exception cref="System.NotImplementedException">Thrown if TFloat is not supported. Supported types: System.Half, float, double, or System.Runtime.InteropServices.NFloat with Size equal to 4 or 8.</exception>
      [System.CLSCompliant(false)]
      public static TFloat FromIeee754Binary(ulong signBit, ulong biasedExponent, ulong fraction)
      {
        var (ieeeExponentBitWidth, ieeeFractionBitWidth) = Ieee754BinaryMeta<TFloat>();

        var ieeeBinary =
          (signBit << (ieeeExponentBitWidth + ieeeFractionBitWidth)) |
          (biasedExponent << ieeeFractionBitWidth) |
          fraction;

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

      /// <summary>
      /// <para>Gets the IEEE 754 binary exponent and fraction bit widths for the generic floating-point type TFloat.</para>
      /// </summary>
      /// <remarks>Mappings: double -> (11, 52); float -> (8, 23); System.Half -> (5, 10); System.Runtime.InteropServices.NFloat -> (11, 52) when Size == 8 or (8, 23) when Size == 4.</remarks>
      /// <returns>A tuple (ExponentBitWidth, FractionBitWidth) representing the bit widths of the exponent and fraction for TFloat.</returns>
      /// <exception cref="System.NotImplementedException">Thrown when TFloat is not a supported IEEE 754 binary floating-point type (not double, float, System.Half, or System.Runtime.InteropServices.NFloat with Size 4 or 8).</exception>
      public static (int ExponentBitWidth, int FractionBitWidth) Ieee754BinaryMeta()
        => (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? (11, 52)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? (8, 23)
        : (typeof(TFloat) == typeof(System.Half))
        ? (5, 10)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>Decomposes a floating-point value into its IEEE 754 sign bit, biased exponent, and fraction fields.</para>
      /// </summary>
      /// <remarks>Supported types: System.Double, System.Single, System.Half, and System.Runtime.InteropServices.NFloat when Size is 4 or 8. Uses BitConverter to obtain raw IEEE 754 bits and extracts fields using the format's exponent and fraction widths provided by Ieee754BinaryMeta<TFloat>().</remarks>
      /// <param name="value">Floating-point value to decompose into IEEE 754 components.</param>
      /// <returns>A tuple (SignBit, BiasedExponent, Fraction) containing the sign bit, the biased exponent, and the fraction extracted from the value's IEEE 754 binary representation.</returns>
      /// <exception cref="System.NotImplementedException">If TFloat is not a supported floating-point type.</exception>
      [System.CLSCompliant(false)]
      public static (ulong SignBit, ulong BiasedExponent, ulong Fraction) ToIeee754Binary(TFloat value)
      {
        ulong ieeeBinary;

        if (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          ieeeBinary = System.BitConverter.DoubleToUInt64Bits(double.CreateChecked(value));
        else if (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          ieeeBinary = System.BitConverter.SingleToUInt32Bits(float.CreateChecked(value));
        else if (typeof(TFloat) == typeof(System.Half))
          ieeeBinary = System.BitConverter.HalfToUInt16Bits(System.Half.CreateChecked(value));
        else throw new System.NotImplementedException();

        var (ieeeExponentBitWidth, ieeeFractionBitWidth) = Ieee754BinaryMeta<TFloat>();

        var signBit = (ieeeBinary >>> (ieeeFractionBitWidth + ieeeExponentBitWidth)) & 1;
        var biasedExponent = (ieeeBinary >>> ieeeFractionBitWidth) & ((1UL << ieeeExponentBitWidth) - 1);
        var fraction = ieeeBinary & ((1UL << ieeeFractionBitWidth) - 1);

        return (signBit, biasedExponent, fraction);
      }

      #endregion

      #region ExtractFields

      /// <summary>
      /// <para>Extracts the IEEE 754 components of the specified floating-point value.</para>
      /// </summary>
      /// <remarks>For normal values, FullSignificand includes the implicit leading one and Significand is 1.fraction. For subnormal values, Exponent is the minimum unbiased exponent, FullSignificand is the raw fraction, and Significand is fraction / 2^fractionBits. The boolean flags identify special categories. recomputedValue is reconstructed from the extracted fields and equals the original value except that NaN and Infinity are preserved exactly.</remarks>
      /// <param name="value">The floating-point value to decompose into IEEE 754 fields.</param>
      /// <param name="recomputedValue">When the method returns, contains the value recomposed from the extracted fields; NaN and Infinity are preserved exactly.</param>
      /// <returns>A tuple containing: Sign (1 for positive, -1 for negative, or 0 for zero); Exponent (unbiased integer exponent); FullSignificand (integer significand, including the implicit leading bit for normal values or the raw fraction for subnormals); Significand (normalized significand as TFloat); IsInfinity; IsNaN; IsNormal; IsSubnormal; IsZero.</returns>
      public static (int Sign, long Exponent, long FullSignificand, TFloat Significand, bool IsInfinity, bool IsNaN, bool IsNormal, bool IsSubnormal, bool IsZero) ExtractFields(TFloat value, out TFloat recomputedValue)
      {
        var (signBit, biasedExponent, fraction) = ToIeee754Binary(value);

        var (sign, unbiasedExponent, fullSignificand, significand, isInfinity, isNaN, isNormal, isSubnormal, isZero) = AnalyzeIeee754Binary(signBit, biasedExponent, fraction, out recomputedValue);

        return (sign, unbiasedExponent, (long)fullSignificand, significand, isInfinity, isNaN, isNormal, isSubnormal, isZero);

        //var ieeeExponentBias = (1 << (ieeeExponentBitWidth - 1)) - 1;

        //var ieeeExponentMax = (1UL << ieeeExponentBitWidth) - 1;

        //var isSubnormal = ieeeBiasedExponent == 0 && ieeeFraction != 0;

        //long unbiasedExponent;
        //ulong fullSignificand; // For IEEE algorithms.
        //TFloat significand; // Human-readable normalized significand.

        //if (ieeeBiasedExponent == 0) // Zero or subnormal.
        //{
        //  unbiasedExponent = 1 - ieeeExponentBias;
        //  fullSignificand = ieeeFraction;
        //  significand = TFloat.CreateChecked(ieeeFraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(ieeeFractionBitWidth));
        //}
        //else // Normal value, or raw decomposition of Infinity/NaN.
        //{
        //  unbiasedExponent = (int)ieeeBiasedExponent - ieeeExponentBias;
        //  fullSignificand = (1UL << ieeeFractionBitWidth) | ieeeFraction;
        //  significand = TFloat.One + TFloat.CreateChecked(ieeeFraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(ieeeFractionBitWidth));
        //}

        //var sign = TFloat.IsZero(value) ? 0 : (ieeeSignBit == 0 ? 1 : -1);

        //recomputedValue = ieeeBiasedExponent == ieeeExponentMax
        //  ? value // Preserve exact NaN/Infinity value.
        //  : sign == 0 ? TFloat.Zero : TFloat.CreateChecked(sign) * TFloat.CreateChecked(fullSignificand) * TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(unbiasedExponent - ieeeFractionBitWidth));

        //return (ieeeSignBit, sign, ieeeBiasedExponent, unbiasedExponent, ieeeFraction, fullSignificand, significand, isSubnormal);
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

      #region ..OrderedBits

      [System.CLSCompliant(false)]
      public static ulong ToOrderedBits(TFloat value)
      {
        if (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        {
          var bits = System.BitConverter.DoubleToInt64Bits(double.CreateChecked(value));
          return bits < 0 ? (ulong)(long.MinValue - bits) : (ulong)bits + 0x8000_0000_0000_0000UL;
        }
        else if (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        {
          var bits = System.BitConverter.SingleToInt32Bits(float.CreateChecked(value));
          return bits < 0 ? (uint)(int.MinValue - bits) : (uint)bits + 0x8000_0000U;
        }
        else if (typeof(TFloat) == typeof(System.Half))
        {
          var bits = System.BitConverter.HalfToInt16Bits(System.Half.CreateChecked(value));
          return bits < 0 ? (ushort)(short.MinValue - bits) : (ushort)bits + 0x8000U;
        }
        else throw new System.NotImplementedException(typeof(TFloat).Name);
      }

      [System.CLSCompliant(false)]
      public static TFloat FromOrderedBits(ulong ordered)
      {
        if (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        {
          var bits = ordered < 0x8000_0000_0000_0000UL ? unchecked(long.MinValue - (long)ordered) : unchecked((long)(ordered - 0x8000_0000_0000_0000UL));
          return TFloat.CreateChecked(System.BitConverter.Int64BitsToDouble(bits));
        }
        else if (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        {
          var bits = ordered < 0x8000_0000U ? unchecked(int.MinValue - (int)ordered) : unchecked((int)(ordered - 0x8000_0000U));
          return TFloat.CreateChecked(System.BitConverter.Int32BitsToSingle(bits));
        }
        else if (typeof(TFloat) == typeof(System.Half))
        {
          var bits = ordered < 0x8000U ? (short)unchecked(short.MinValue - (short)ordered) : (short)unchecked((short)(ordered - 0x8000U));
          return TFloat.CreateChecked(System.BitConverter.Int16BitsToHalf(bits));
        }
        else throw new System.NotImplementedException(typeof(TFloat).Name);
      }

      #endregion
    }
  }
}