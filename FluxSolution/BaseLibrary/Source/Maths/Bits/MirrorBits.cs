//namespace Flux
//{
//  public static partial class Bits
//  {
//    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</summary>
//    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public static void ReverseBits(ref this byte value)
//      => value = (byte)((value * 0x0202020202UL & 0x010884422010UL) % 1023);

//    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</summary>
//    [System.CLSCompliant(false)]
//    public static void ReverseBits(ref this ushort value)
//    {
//      value = (ushort)(((value & 0xAAAA) >> 0x01) | ((value & 0x5555) << 0x01));
//      value = (ushort)(((value & 0xCCCC) >> 0x02) | ((value & 0x3333) << 0x02));
//      value = (ushort)(((value & 0xF0F0) >> 0x04) | ((value & 0x0F0F) << 0x04));
//      value = (ushort)(((value & 0xFF00) >> 0x08) | ((value & 0x00FF) << 0x08));
//    }

//    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</summary>
//    [System.CLSCompliant(false)]
//    public static void ReverseBits(ref this uint value)
//    {
//      value = ((value & 0xAAAAAAAA) >> 0x01) | ((value & 0x55555555) << 0x01);
//      value = ((value & 0xCCCCCCCC) >> 0x02) | ((value & 0x33333333) << 0x02);
//      value = ((value & 0xF0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F) << 0x04);
//      value = ((value & 0xFF00FF00) >> 0x08) | ((value & 0x00FF00FF) << 0x08);
//      value = ((value & 0xFFFF0000) >> 0x10) | ((value & 0x0000FFFF) << 0x10);
//    }

//    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</summary>
//    [System.CLSCompliant(false)]
//    public static void ReverseBits(ref this ulong value)
//    {
//      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);
//      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
//      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
//      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
//      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
//      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
//    }
//  }
//}
