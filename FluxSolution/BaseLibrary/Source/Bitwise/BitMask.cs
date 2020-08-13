namespace Flux
{
  public static partial class Bitwise
  {
    public static int LeastSignificantBitMaskInt32(in int bitCount)
      => unchecked((int)(uint.MaxValue >> (32 - bitCount)));
    public static long LeastSignificantBitMaskInt64(in int bitCount)
      => unchecked((long)(ulong.MaxValue >> (64 - bitCount)));

    [System.CLSCompliant(false)]
    public static ulong LeastSignificantBitMaskUInt32(in int bitCount)
      => uint.MaxValue >> (32 - bitCount);
    [System.CLSCompliant(false)]
    public static ulong LeastSignificantBitMaskUInt64(in int bitCount)
      => ulong.MaxValue >> (64 - bitCount);

    public static int MostSignificantBitMaskInt32(in int bitCount)
      => unchecked((int)(uint.MaxValue << (32 - bitCount)));
    public static long MostSignificantBitMaskInt64(in int bitCount)
      => unchecked((long)(ulong.MaxValue << (64 - bitCount)));

    [System.CLSCompliant(false)]
    public static ulong MostSignificantBitMaskUInt32(in int bitCount)
      => uint.MaxValue << (32 - bitCount);
    [System.CLSCompliant(false)]
    public static ulong MostSignificantBitMaskUInt64(in int bitCount)
      => ulong.MaxValue << (64 - bitCount);
  }
}
