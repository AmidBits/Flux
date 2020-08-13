namespace Flux
{
  public static partial class Bitwise
  {
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger RotateRight(System.Numerics.BigInteger value, int bits)
      => (value << (BitLength(value) - bits)) | (value >> bits);
    [System.CLSCompliant(false)]
    public static void RotateRight(ref this System.Numerics.BigInteger value, int bits)
      => value = (value << (BitLength(value) - bits)) | (value >> bits);

    public static int RotateRight(int value)
      => unchecked((int)RotateRight((uint)value));
    public static int RotateRight(int value, int bits)
      => unchecked((int)RotateRight((uint)value, bits));

    public static long RotateRight(long value)
      => unchecked((long)RotateRight((ulong)value));
    public static long RotateRight(long value, int bits)
      => unchecked((long)RotateRight((ulong)value, bits));

    public static byte RotateRight(byte value)
      => (byte)(((value << 7) | (value >> 1)) & 0xFF);
    public static byte RotateRight(byte value, int bits)
      => (byte)(((value << (8 - (bits % 8))) | (value >> (bits % 8))) & 0xFF);

    [System.CLSCompliant(false)]
    public static ushort RotateRight(ushort value)
      => (ushort)(((value << 15) | (value >> 1)) & 0xFFFF);
    [System.CLSCompliant(false)]
    public static ushort RotateRight(ushort value, int bits)
      => (ushort)(((value << (16 - (bits % 16))) | (value >> (bits % 16))) & 0xFFFF);

    [System.CLSCompliant(false)]
    public static uint RotateRight(uint value)
      => (value << 31) | (value >> 1);
    [System.CLSCompliant(false)]
    public static void RotateRight(ref this uint value)
      => value = (value << 31) | (value >> 1);
    [System.CLSCompliant(false)]
    public static uint RotateRight(uint value, int bits)
      => (value << (32 - bits)) | (value >> bits);
    [System.CLSCompliant(false)]
    public static void RotateRight(ref this uint value, int bits)
      => value = (value << (32 - bits)) | (value >> bits);

    [System.CLSCompliant(false)]
    public static ulong RotateRight(ulong value)
      => (value << 63) | (value >> 1);
    [System.CLSCompliant(false)]
    public static void RotateRight(ref this ulong value)
      => value = (value << 63) | (value >> 1);
    [System.CLSCompliant(false)]
    public static ulong RotateRight(ulong value, int bits)
      => (value << (64 - bits)) | (value >> bits);
    [System.CLSCompliant(false)]
    public static void RotateRight(ref this ulong value, int bits)
      => value = (value << (64 - bits)) | (value >> bits);
  }
}
