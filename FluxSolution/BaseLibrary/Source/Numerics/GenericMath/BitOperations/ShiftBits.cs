namespace Flux
{
  public static partial class BitOps
  {
    public static void ShiftBitsRight(this byte[] source, int bitCount)
    {
      var (byteCount, bitRemainder) = int.DivRem(bitCount, 8);
      var byteRemainder = 8 - bitRemainder;

      //if (shiftRight > 0)
      for (var i = 0; i < byteCount; i++)
        source[i] = (byte)((source[i + 1] << bitRemainder >> bitRemainder) | (source[i] >> byteRemainder));

      source[byteCount] >>= byteRemainder;
    }
  }
}
