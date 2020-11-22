namespace Flux
{
  public static partial class BitOps
  {
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger RotateLeft(System.Numerics.BigInteger value, int count)
      => (value << 1) | (value >> (BitLength(value) - count));

    [System.CLSCompliant(false)]
    public static uint RotateLeft(uint value, int count)
      => (value << count) | (value >> (32 - count));

    [System.CLSCompliant(false)]
    public static ulong RotateLeft(ulong value, int count)
      => (value << count) | (value >> (64 - count));

    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger RotateRight(System.Numerics.BigInteger value, int count)
      => (value << (BitLength(value) - count)) | (value >> count);

    [System.CLSCompliant(false)]
    public static uint RotateRight(uint value, int count)
      => (value << (32 - count)) | (value >> count);

    [System.CLSCompliant(false)]
    public static ulong RotateRight(ulong value, int count)
      => (value << (64 - count)) | (value >> count);
  }
}
