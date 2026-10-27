namespace Flux
{
  public static partial class BinaryFloatingPointIeee754
  {
    #region Binary (16/32/64) functions

    private const ulong HalfSignMask = 0x8000UL;
    private const ulong HalfValueMask = 0x7FFFUL;
    private const ulong SingleSignMask = 0x8000_0000UL;
    private const ulong SingleValueMask = 0x7FFF_FFFFUL;
    private const ulong DoubleSignMask = 0x8000_0000_0000_0000UL;
    private const ulong DoubleValueMask = 0x7FFF_FFFF_FFFF_FFFFUL;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IBinaryFloatingPointIeee754<TFloat>
    {
      [System.CLSCompliant(false)]
      public static (int Sign, long UnbiasedExponent, ulong FullSignificand, TFloat Significand, bool IsInfinity, bool IsNaN, bool IsNormal, bool IsSubnormal, bool IsZero) AnalyzeBinary(ulong binarySign, ulong binaryExponent, ulong binaryFraction, out TFloat recomputedValue)
      {
        var (signWidth, exponentWidth, fractionWidth) = BinaryWidth<TFloat>();

        var exponentBias = (1L << (exponentWidth - 1)) - 1;
        var exponentMax = (1UL << exponentWidth) - 1;

        var isInfinity = binaryExponent == exponentMax && binaryFraction == 0;
        var isNaN = binaryExponent == exponentMax && binaryFraction != 0;
        var isNormal = binaryExponent != 0 && binaryExponent != exponentMax;
        var isSubnormal = binaryExponent == 0 && binaryFraction != 0;
        var isZero = binaryExponent == 0 && binaryFraction == 0;

        long unbiasedExponent;
        ulong fullSignificand; // For IEEE algorithms.
        TFloat significand; // Human-readable normalized significand.

        if (binaryExponent == 0) // Zero or subnormal.
        {
          unbiasedExponent = 1 - exponentBias;
          fullSignificand = binaryFraction;
          significand = TFloat.CreateChecked(binaryFraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(fractionWidth));
        }
        else if (isInfinity || isNaN)
        {
          unbiasedExponent = long.MaxValue;
          fullSignificand = 0;
          significand = TFloat.NaN;
        }
        else // Normal value.
        {
          unbiasedExponent = (long)binaryExponent - exponentBias;
          fullSignificand = (1UL << fractionWidth) | binaryFraction;
          significand = TFloat.One + TFloat.CreateChecked(binaryFraction) / TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(fractionWidth));
        }

        var sign = binarySign == 0 ? 1 : -1;

        recomputedValue = isZero ? FromBinary<TFloat>(binarySign, 0, 0) // Ensure negative zero is preserved, because IEEE multiplication by zero loses the sign (in last calculation below).
          : isNaN ? TFloat.NaN // Preserve exact NaN.
          : isInfinity ? (sign > 0 ? TFloat.PositiveInfinity : TFloat.NegativeInfinity) // Preserve exact infinity value.
          : TFloat.CreateChecked(sign) * TFloat.CreateChecked(fullSignificand) * TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(unbiasedExponent - fractionWidth));

        return (sign, unbiasedExponent, fullSignificand, significand, isInfinity, isNaN, isNormal, isSubnormal, isZero);
      }

      public static (int SignWidth, int ExponentWidth, int FractionWidth) BinaryWidth()
        => typeof(TFloat).IsNumericDouble()
        ? (1, 11, 52)
        : typeof(TFloat).IsNumericSingle()
        ? (1, 8, 23)
        : typeof(TFloat) == typeof(System.Half)
        ? (1, 5, 10)
        : throw new System.NotImplementedException();

      [System.CLSCompliant(false)]
      public static TFloat FromBinary(ulong binarySign, ulong binaryExponent, ulong binaryFraction)
      {
        var (signWidth, exponentWidth, fractionWidth) = BinaryWidth<TFloat>();

        var bits =
          (binarySign << (exponentWidth + fractionWidth)) |
          (binaryExponent << fractionWidth) |
          binaryFraction;

        return FromBits<TFloat>(bits);
      }

      [System.CLSCompliant(false)]
      public static TFloat FromBits(ulong bits)
        => typeof(TFloat).IsNumericDouble()
        ? TFloat.CreateChecked(System.BitConverter.UInt64BitsToDouble(bits))
        : typeof(TFloat).IsNumericSingle()
        ? TFloat.CreateChecked(System.BitConverter.UInt32BitsToSingle((uint)bits))
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked(System.BitConverter.UInt16BitsToHalf((ushort)bits))
        : throw new System.NotImplementedException();

      [System.CLSCompliant(false)]
      public static TFloat FromOrdered(ulong ordered)
      {
        if (typeof(TFloat).IsNumericDouble())
        {
          var bits = ordered < DoubleSignMask ? ~ordered : ordered & DoubleValueMask;
          return TFloat.CreateChecked(System.BitConverter.UInt64BitsToDouble(bits));
        }
        else if (typeof(TFloat).IsNumericSingle())
        {
          var bits = ordered < SingleSignMask ? ~(uint)ordered : (uint)(ordered & SingleValueMask);
          return TFloat.CreateChecked(System.BitConverter.UInt32BitsToSingle(bits));
        }
        else if (typeof(TFloat) == typeof(System.Half))
        {
          var bits = ordered < HalfSignMask ? (ushort)~ordered : (ushort)(ordered & HalfValueMask);
          return TFloat.CreateChecked(System.BitConverter.UInt16BitsToHalf(bits));
        }
        else throw new System.NotImplementedException(typeof(TFloat).Name);
      }

      [System.CLSCompliant(false)]
      public static (ulong BinarySign, ulong BinaryExponent, ulong BinaryFraction) ToBinary(TFloat value)
      {
        var bits = ToBits(value);

        var (signWidth, exponentWidth, fractionWidth) = BinaryWidth<TFloat>();

        var sign = (bits >>> (fractionWidth + exponentWidth)) & 1;
        var exponent = (bits >>> fractionWidth) & ((1UL << exponentWidth) - 1);
        var fraction = bits & ((1UL << fractionWidth) - 1);

        return (sign, exponent, fraction);
      }

      [System.CLSCompliant(false)]
      public static ulong ToBits(TFloat value)
        => typeof(TFloat).IsNumericDouble()
        ? System.BitConverter.DoubleToUInt64Bits(double.CreateChecked(value))
        : typeof(TFloat).IsNumericSingle()
        ? System.BitConverter.SingleToUInt32Bits(float.CreateChecked(value))
        : (typeof(TFloat) == typeof(System.Half))
        ? System.BitConverter.HalfToUInt16Bits(System.Half.CreateChecked(value))
        : throw new System.NotImplementedException();

      [System.CLSCompliant(false)]
      public static ulong ToOrdered(TFloat value)
      {
        var bits = ToBits(value);

        if (typeof(TFloat).IsNumericDouble())
          return (bits & DoubleSignMask) != 0 ? ~bits : bits | DoubleSignMask;
        else if (typeof(TFloat).IsNumericSingle())
          return (bits & SingleSignMask) != 0 ? ~bits : bits | SingleSignMask;
        else if (typeof(TFloat) == typeof(System.Half))
          return (bits & HalfSignMask) != 0 ? ~bits : bits | HalfSignMask;
        else throw new System.NotImplementedException();
      }
    }

    #endregion
  }
}