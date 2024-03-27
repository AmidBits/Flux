namespace Flux.IO.Hashing.Special
{
  /// <summary>An NT hash algorithm, which is basically MD4. This implementation is based on RFC1320.</summary>
  /// <remarks>This is the algorithm used for NTLM (a.k.a. NT hash).</remarks>
  /// <see href="https://en.wikipedia.org/wiki/NT_LAN_Manager"/>
  /// <remarks>
  /// reg save HKLM\SAM C:\sam
  /// reg save HKLM\SYSTEM C:\system
  /// </remarks>
  public static class NtHash
  {
    private static uint AuxF(uint x, uint y, uint z) => ((x & y) | ((~x) & z));
    private static uint AuxG(uint x, uint y, uint z) => ((x & y) | (x & z) | (y & z));
    private static uint AuxH(uint x, uint y, uint z) => (x ^ y ^ z);

    private static uint LeftRotate(uint x, int s) => (x << s) | (x >> (32 - s)); // 32-bit value obtained by circularly shifting (rotating) X left by s bit positions.

    private static void RoundF(ref uint a, ref uint b, ref uint c, ref uint d, uint k, int s, uint[] processingBuffer) => a = LeftRotate((a + AuxF(b, c, d) + processingBuffer[k]), s);
    private static void RoundG(ref uint a, ref uint b, ref uint c, ref uint d, uint k, int s, uint[] processingBuffer) => a = LeftRotate(a + AuxG(b, c, d) + processingBuffer[k] + 0x5A827999, s);
    private static void RoundH(ref uint a, ref uint b, ref uint c, ref uint d, uint k, int s, uint[] processingBuffer) => a = LeftRotate(a + AuxH(b, c, d) + processingBuffer[k] + 0x6ED9EBA1, s);

    public static byte[] Compute(byte[] data)
    {
      System.ArgumentNullException.ThrowIfNull(data);

      var dataBitLength = data.Length * 8; // The message is "padded" (extended) so that its length (in bits) is congruent to 448, modulo 512.

      var padBitLength = (dataBitLength / 512) * 512 + 448;
      if (padBitLength <= dataBitLength) padBitLength += 512;

      var paddedData = new byte[(padBitLength + 64) / 8]; // A 64-bit representation of b (the length of the message before the padding bits were added) is appended to the result of the previous step.

      System.Array.Copy(data, 0, paddedData, 0, data.Length);

      paddedData[data.Length] = 0x80; // A single "1" bit is appended to the message, and as the RFC defines, a byte is a sequence of bits with the highest order bit going first.

      var dataLength = System.BitConverter.GetBytes((ulong)dataBitLength);

      System.Array.Copy(dataLength, 0, paddedData, paddedData.Length - dataLength.Length, dataLength.Length);

      var padded16BitBlocks = (paddedData.Length / 4) / 16;

      var regA = 0x67452301U;
      var regB = 0xEFCDAB89U;
      var regC = 0x98BADCFEU;
      var regD = 0x10325476U;

      var processingBuffer = new uint[16];

      for (int i = 0; i < padded16BitBlocks; i++) // Process each 16-word block.
      {
        for (int j = 0; j < 16; j++)
        {
          processingBuffer[j] = System.BitConverter.ToUInt32(paddedData, (i * 16 + j) * 4);
        }

        RoundF(ref regA, ref regB, ref regC, ref regD, 0, 3, processingBuffer);
        RoundF(ref regD, ref regA, ref regB, ref regC, 1, 7, processingBuffer);
        RoundF(ref regC, ref regD, ref regA, ref regB, 2, 11, processingBuffer);
        RoundF(ref regB, ref regC, ref regD, ref regA, 3, 19, processingBuffer);

        RoundF(ref regA, ref regB, ref regC, ref regD, 4, 3, processingBuffer);
        RoundF(ref regD, ref regA, ref regB, ref regC, 5, 7, processingBuffer);
        RoundF(ref regC, ref regD, ref regA, ref regB, 6, 11, processingBuffer);
        RoundF(ref regB, ref regC, ref regD, ref regA, 7, 19, processingBuffer);

        RoundF(ref regA, ref regB, ref regC, ref regD, 8, 3, processingBuffer);
        RoundF(ref regD, ref regA, ref regB, ref regC, 9, 7, processingBuffer);
        RoundF(ref regC, ref regD, ref regA, ref regB, 10, 11, processingBuffer);
        RoundF(ref regB, ref regC, ref regD, ref regA, 11, 19, processingBuffer);

        RoundF(ref regA, ref regB, ref regC, ref regD, 12, 3, processingBuffer);
        RoundF(ref regD, ref regA, ref regB, ref regC, 13, 7, processingBuffer);
        RoundF(ref regC, ref regD, ref regA, ref regB, 14, 11, processingBuffer);
        RoundF(ref regB, ref regC, ref regD, ref regA, 15, 19, processingBuffer);

        RoundG(ref regA, ref regB, ref regC, ref regD, 0, 3, processingBuffer);
        RoundG(ref regD, ref regA, ref regB, ref regC, 4, 5, processingBuffer);
        RoundG(ref regC, ref regD, ref regA, ref regB, 8, 9, processingBuffer);
        RoundG(ref regB, ref regC, ref regD, ref regA, 12, 13, processingBuffer);

        RoundG(ref regA, ref regB, ref regC, ref regD, 1, 3, processingBuffer);
        RoundG(ref regD, ref regA, ref regB, ref regC, 5, 5, processingBuffer);
        RoundG(ref regC, ref regD, ref regA, ref regB, 9, 9, processingBuffer);
        RoundG(ref regB, ref regC, ref regD, ref regA, 13, 13, processingBuffer);

        RoundG(ref regA, ref regB, ref regC, ref regD, 2, 3, processingBuffer);
        RoundG(ref regD, ref regA, ref regB, ref regC, 6, 5, processingBuffer);
        RoundG(ref regC, ref regD, ref regA, ref regB, 10, 9, processingBuffer);
        RoundG(ref regB, ref regC, ref regD, ref regA, 14, 13, processingBuffer);

        RoundG(ref regA, ref regB, ref regC, ref regD, 3, 3, processingBuffer);
        RoundG(ref regD, ref regA, ref regB, ref regC, 7, 5, processingBuffer);
        RoundG(ref regC, ref regD, ref regA, ref regB, 11, 9, processingBuffer);
        RoundG(ref regB, ref regC, ref regD, ref regA, 15, 13, processingBuffer);

        RoundH(ref regA, ref regB, ref regC, ref regD, 0, 3, processingBuffer);
        RoundH(ref regD, ref regA, ref regB, ref regC, 8, 9, processingBuffer);
        RoundH(ref regC, ref regD, ref regA, ref regB, 4, 11, processingBuffer);
        RoundH(ref regB, ref regC, ref regD, ref regA, 12, 15, processingBuffer);

        RoundH(ref regA, ref regB, ref regC, ref regD, 2, 3, processingBuffer);
        RoundH(ref regD, ref regA, ref regB, ref regC, 10, 9, processingBuffer);
        RoundH(ref regC, ref regD, ref regA, ref regB, 6, 11, processingBuffer);
        RoundH(ref regB, ref regC, ref regD, ref regA, 14, 15, processingBuffer);

        RoundH(ref regA, ref regB, ref regC, ref regD, 1, 3, processingBuffer);
        RoundH(ref regD, ref regA, ref regB, ref regC, 9, 9, processingBuffer);
        RoundH(ref regC, ref regD, ref regA, ref regB, 5, 11, processingBuffer);
        RoundH(ref regB, ref regC, ref regD, ref regA, 13, 15, processingBuffer);

        RoundH(ref regA, ref regB, ref regC, ref regD, 3, 3, processingBuffer);
        RoundH(ref regD, ref regA, ref regB, ref regC, 11, 9, processingBuffer);
        RoundH(ref regC, ref regD, ref regA, ref regB, 7, 11, processingBuffer);
        RoundH(ref regB, ref regC, ref regD, ref regA, 15, 15, processingBuffer);

        regA += 0x67452301U;
        regB += 0xEFCDAB89U;
        regC += 0x98BADCFEU;
        regD += 0x10325476U;
      }

      byte[] hash = new byte[16];
      System.Array.Copy(System.BitConverter.GetBytes(regA), 0, hash, 0, 4);
      System.Array.Copy(System.BitConverter.GetBytes(regB), 0, hash, 4, 4);
      System.Array.Copy(System.BitConverter.GetBytes(regC), 0, hash, 8, 4);
      System.Array.Copy(System.BitConverter.GetBytes(regD), 0, hash, 12, 4);
      return hash;
    }
    public static byte[] Compute(string text) => Compute(System.Text.Encoding.Unicode.GetBytes(text));
  }
}
