namespace Flux.Maths
{
  /// <summary>Deconstructs the decimal number into its basic components as a named ValueType for use.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Double-precision_floating-point_format"/>
  /// <seealso cref="http://www.yoda.arachsys.com/csharp/DoubleConverter.cs"/>
  /// <seealso cref="https://blogs.msdn.microsoft.com/dwayneneed/2010/05/06/fun-with-floating-point/"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record class DoubleParts
  {
    private ulong m_bits;

    public DoubleParts(double value) => Double = value;
    public DoubleParts(long significandPrecision, int exponent, int signBit)
    {
      Exponent = exponent;
      SignBit = signBit;
      SignificandPrecision = significandPrecision;
    }

    public double Double { get => System.BitConverter.UInt64BitsToDouble(m_bits); set => m_bits = System.BitConverter.DoubleToUInt64Bits(value); }

    public int Exponent { get => (int)((m_bits >> 52) & 0x7FF); set => m_bits = (m_bits & 0xFFE0FFFF_FFFF_FFFF) | ((ulong)AssertExponent(value)) << 52; }
    public int SignBit { get => (int)(m_bits >> 63); set => m_bits = (m_bits & 0x7FFFFFFFFFFFFFFF) | ((ulong)AssertSignBit(value)) << 63; }
    public long SignificandPrecision { get => (long)(m_bits & 0x000F_FFFF_FFFF_FFFF); set => m_bits = (m_bits & 0xFFF0_0000_0000_0000) | (ulong)(value & 0x000F_FFFF_FFFF_FFFF); }

    public static int AssertExponent(int exponent) => exponent >= 0 && exponent <= 2047 ? exponent : throw new System.ArgumentOutOfRangeException(nameof(exponent), "A value between 0 and 2047 is required.");
    public static int AssertSignBit(int signBit) => signBit >= 0 && signBit <= 1 ? signBit : throw new System.ArgumentOutOfRangeException(nameof(signBit), "A value of 0 or 1 is required.");
#if NET7_0_OR_GREATER
    public static System.Int128 AssertSignificandPrecision(long significandPrecision) => significandPrecision >= 0 && significandPrecision <= ((System.Int128.One << 52) - 1) ? significandPrecision : throw new System.ArgumentOutOfRangeException(nameof(significandPrecision), "An absolute 52-bit value is required.");
#endif
  }
}
