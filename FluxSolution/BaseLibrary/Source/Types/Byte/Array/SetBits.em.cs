namespace Flux
{
  public static partial class ExtensionMethodsByte
  {
    public static byte[] SetBits(this byte[] source, long startBitIndex, long bitCount, bool state)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceBitLength = source.Length * 8;

      if (startBitIndex < 0 || startBitIndex > sourceBitLength) throw new System.ArgumentOutOfRangeException(nameof(startBitIndex));
      if (bitCount < 0 || (startBitIndex + bitCount is var endBitIndex && endBitIndex > sourceBitLength)) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      var bitState = (byte)(state ? 0xFF : 0x00);

      var startQuotient = startBitIndex / 8;
      var startRemainder = startBitIndex % 8;
      var startMask = (byte)((1 << (8 - (int)startRemainder)) - 1);

      var endQuotient = endBitIndex / 8;
      var endRemainder = endBitIndex % 8;
      var endMask = (byte)(~((0x80 >> ((int)endRemainder - 1)) - 1) & 0xFF);

      if (startQuotient == endQuotient && (startMask & endMask) is var mixedMask) // All in the same byte?
      {
        source[startQuotient] = state ? (byte)(source[startQuotient] | mixedMask) : (byte)(source[startQuotient] & ~mixedMask);
      }
      else // Or in 2 or more bytes?
      {
        if (startRemainder > 0)
          source[startQuotient] = (byte)((source[startQuotient] & (~startMask & 0xFF)) | (bitState & startMask));
        if (endRemainder > 0)
          source[endQuotient] = (byte)((source[endQuotient] & (~endMask & 0xFF)) | (bitState & endMask));

        for (var index = startRemainder > 0 ? startQuotient + 1 : startQuotient; index < endQuotient; index++)
          source[index] = bitState; // Set whole bytes if needed.
      }

      return source;
    }
  }
}
