//namespace Flux
//{
//  public static partial class BitOps
//  {
//    /// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
//    public static System.Numerics.BigInteger CreateMaskRightBigInteger(int leadingOneCount)
//      => leadingOneCount > 0
//      ? (System.Numerics.BigInteger.One << leadingOneCount) - 1
//      : throw new System.ArgumentOutOfRangeException(nameof(leadingOneCount));

//    /// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
//    public static int CreateMaskRightInt32(int bit1Count)
//      => unchecked((int)CreateMaskRightUInt32(bit1Count));
//    /// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
//    public static long CreateMaskRightInt64(int bit1Count)
//      => unchecked((long)CreateMaskRightUInt64(bit1Count));

//    /// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
//    [System.CLSCompliant(false)]
//    public static uint CreateMaskRightUInt32(int bit1Count)
//      => (bit1Count >= 0 && bit1Count <= 32)
//      ? uint.MaxValue >> (32 - bit1Count)
//      : throw new System.ArgumentOutOfRangeException(nameof(bit1Count));
//    /// <summary>Create a bit mask with the specified number of LSBs (Least Significant Bits) set to 1.</summary>
//    [System.CLSCompliant(false)]
//    public static ulong CreateMaskRightUInt64(int bit1Count)
//      => bit1Count >= 0 && bit1Count <= 64
//      ? ulong.MaxValue >> (64 - bit1Count)
//      : throw new System.ArgumentOutOfRangeException(nameof(bit1Count));
//  }
//}
