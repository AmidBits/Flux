namespace Flux
{
  public static partial class Maths
  {
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger RotateLeft(System.Numerics.BigInteger value, int bits)
      => (value << 1) | (value >> (BitLength(value) - bits));
    [System.CLSCompliant(false)]
    public static void RotateLeft(ref this System.Numerics.BigInteger value, int bits)
      => value = (value << 1) | (value >> (BitLength(value) - bits));

    public static int RotateLeft(int value)
      => unchecked((int)RotateLeft((uint)value));
    public static int RotateLeft(int value, int bits)
      => unchecked((int)RotateLeft((uint)value, bits));

    public static long RotateLeft(long value)
      => unchecked((long)RotateLeft((ulong)value));
    public static long RotateLeft(long value, int bits)
      => unchecked((long)RotateLeft((ulong)value, bits));

    public static byte RotateLeft(byte value)
      => (byte)(((value << 1) | (value >> 7)) & 0xFF);
    public static byte RotateLeft(byte value, int bits)
      => (byte)(((value << (bits % 8)) | (value >> (8 - (bits % 8)))) & 0xFF);

    [System.CLSCompliant(false)]
    public static ushort RotateLeft(ushort value)
      => (ushort)(((value << 1) | (value >> 15)) & 0xFFFF);
    [System.CLSCompliant(false)]
    public static ushort RotateLeft(ushort value, int bits)
      => (ushort)(((value << (bits % 16)) | (value >> (16 - (bits % 16)))) & 0xFFFF);

    [System.CLSCompliant(false)]
    public static uint RotateLeft(uint value)
      => (value << 1) | (value >> 31);
    [System.CLSCompliant(false)]
    public static void RotateLeft(ref this uint value)
      => value = (value << 1) | (value >> 31);
    [System.CLSCompliant(false)]
    public static uint RotateLeft(uint value, int bits)
      => (value << bits) | (value >> (32 - bits));
    [System.CLSCompliant(false)]
    public static void RotateLeft(ref this uint value, int bits)
      => value = (value << bits) | (value >> (32 - bits));

    [System.CLSCompliant(false)]
    public static ulong RotateLeft(ulong value)
      => (value << 1) | (value >> 63);
    [System.CLSCompliant(false)]
    public static void RotateLeft(ref this ulong value)
      => value = (value << 1) | (value >> 63);
    [System.CLSCompliant(false)]
    public static ulong RotateLeft(ulong value, int bits)
      => (value << bits) | (value >> (64 - bits));
    [System.CLSCompliant(false)]
    public static void RotateLeft(ref this ulong value, int bits)
      => value = (value << bits) | (value >> (64 - bits));
  }
}
