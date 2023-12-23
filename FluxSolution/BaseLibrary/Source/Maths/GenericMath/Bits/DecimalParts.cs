//namespace Flux
//{
//  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//  public record class DecimalParts
//  {
//    private readonly uint[] m_bits = new uint[4];

//    public DecimalParts(decimal value) => Decimal = value;
//#if NET7_0_OR_GREATER
//    public DecimalParts(System.Int128 integerNumber, int scalingFactor, int signBit)
//    {
//      SignBit = signBit;
//      IntegerNumber = integerNumber;
//      ScalingFactor = scalingFactor;
//    }
//#else
//    public DecimalParts(long integerNumber, int scalingFactor, int signBit)
//    {
//      SignBit = signBit;
//      IntegerNumber = integerNumber;
//      ScalingFactor = scalingFactor;
//    }
//#endif

//    public System.Span<int> AsSpan() => (int[])(object)m_bits;

//    public decimal Decimal { get => new(AsSpan()); set => decimal.GetBits(value, AsSpan()); }
//#if NET7_0_OR_GREATER
//    public System.Int128 IntegerNumber
//    {
//      get => (System.Int128)((((System.UInt128)m_bits[2]) << 64) | (((System.UInt128)m_bits[1]) << 32) | ((System.UInt128)m_bits[0]));
//      set
//      {
//        m_bits[2] = (uint)(value >> 64);
//        m_bits[1] = (uint)(value >> 32);
//        m_bits[0] = (uint)(value);
//      }
//    }
//#else
//    public long IntegerNumber
//    {
//      get => (long)((((ulong)m_bits[1]) << 32) | ((ulong)m_bits[0]));
//      set
//      {
//        m_bits[1] = (uint)(value >> 32);
//        m_bits[0] = (uint)(value);
//      }
//    }
//#endif
//    public int ScalingFactor { get => (int)((m_bits[3] >> 16) & 0x1F); set => m_bits[3] = (m_bits[3] & 0xFFE0_FFFF) | ((uint)AssertScalingFactor(value)) << 16; }
//    public int SignBit { get => (int)(m_bits[3] >> 31); set => m_bits[3] = (m_bits[3] & 0x7FFF_FFFF) | ((uint)AssertSignBit(value)) << 31; }

//#if NET7_0_OR_GREATER
//    public static System.Int128 AssertIntegerNumber(System.Int128 integerNumber) => integerNumber >= 0 && integerNumber <= ((System.Int128.One << 96) - 1) ? integerNumber : throw new System.ArgumentOutOfRangeException(nameof(integerNumber), "An absolute 96-bit value is required.");
//#endif
//    public static int AssertScalingFactor(int scalingFactor) => scalingFactor >= 0 && scalingFactor <= 28 ? scalingFactor : throw new System.ArgumentOutOfRangeException(nameof(scalingFactor), "A value between 0 and 28 is required.");
//    public static int AssertSignBit(int signBit) => signBit >= 0 && signBit <= 1 ? signBit : throw new System.ArgumentOutOfRangeException(nameof(signBit), "A value of 0 or 1 is required.");
//  }
//}
