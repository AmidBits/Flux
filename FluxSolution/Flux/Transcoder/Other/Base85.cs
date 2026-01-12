namespace Flux.Transcode
{
  /// <summary>Base85 encoding.</summary>
  public class Base85
    : IStringTranscoder
  {
    public static IStringTranscoder Default { get; } = new Base85();

    public byte[] DecodeString(string input)
    {
      using var sr = new System.IO.StringReader(input);
      using var ms = new System.IO.MemoryStream();

      DecodeBase85(sr, ms);

      return ms.ToArray();
    }

    public string EncodeString(byte[] input)
    {
      using var ms = new System.IO.MemoryStream(input);
      var sb = new System.Text.StringBuilder();
      using var sw = new System.IO.StringWriter(sb);

      EncodeBase85(ms, sw);

      return sb.ToString();
    }

    /// <summary>
    /// <para>Decodes <paramref name="sourceEncodedBase85"/> into <paramref name="targetDecodedBinaryData"/>.</para>
    /// </summary>
    /// <param name="sourceEncodedBase85">Base85/ASCII85 text to decode.</param>
    /// <param name="targetDecodedBinaryData">Decoded binary data.</param>
    public static void DecodeBase85(System.IO.TextReader sourceEncodedBase85, System.IO.Stream targetDecodedBinaryData)
    {
      var powersOf85 = new uint[] { 52200625, 614125, 7225, 85, 1 };

      var decodedBlock = new byte[4];
      var count = 0;
      var block = 0U;
      var processChar = false;

      while (sourceEncodedBase85.Read() is var read && read > -1 && (char)read is var c)
      {
        if (c == 'z')
        {
          if (count != 0) throw new Exception("The character 'z' is invalid inside an ASCII85 block.");

          System.Array.Clear(decodedBlock);

          targetDecodedBinaryData.Write(decodedBlock);

          processChar = false;
        }
        //else if (c == '\n' || c == '\r' || c == '\t' || c == '\0' || c == '\f' || c == '\b')
        //  processChar = false;
        else
        {
          if (c < '!' || c > 'u') throw new Exception("Invalid character.");

          processChar = true;
        }

        if (processChar)
        {
          block += (uint)(c - 33) * powersOf85[count];
          count++;

          if (count == 5)
          {
            DecodeBlock(4);

            targetDecodedBinaryData.Write(decodedBlock);

            block = 0;
            count = 0;
          }
        }
      }

      if (count != 0) // We've got residue.
      {
        if (count == 1) throw new Exception("The last block of ASCII85 data cannot be a single byte.");

        count--;
        block += powersOf85[count];

        DecodeBlock(count);

        for (var i = 0; i < count; i++)
          targetDecodedBinaryData.WriteByte(decodedBlock[i]);
      }

      targetDecodedBinaryData.Flush();

      void DecodeBlock(int blockCount)
      {
        for (var i = 0; i < blockCount; i++)
          decodedBlock[i] = (byte)(block >> 24 - (i * 8));
      }
    }

    /// <summary>
    /// <para>Encodes <paramref name="sourceBinaryData"/> into <paramref name="targetEncodedBase85"/>.</para>
    /// </summary>
    /// <param name="sourceBinaryData">Binary data to encode.</param>
    /// <param name="targetEncodedBase85">Encoded Base85/ASCII85 text.</param>
    public static void EncodeBase85(System.IO.Stream sourceBinaryData, System.IO.TextWriter targetEncodedBase85)
    {
      var encodedBlock = new byte[5];
      var block = 0U;
      var count = 0;

      while (sourceBinaryData.ReadByte() is var b && b > -1)
      {
        if (count >= 3)
        {
          block |= (byte)b;

          if (block == 0)
            targetEncodedBase85.Write('z');
          else
            EncodeBlock(5);

          block = 0;
          count = 0;
        }
        else
        {
          block |= (uint)(b << (24 - (count * 8)));
          count++;
        }
      }

      if (count > 0) // We've got residue.
        EncodeBlock(count + 1);

      targetEncodedBase85.Flush();

      void EncodeBlock(int blockCount)
      {
        for (var i = 4; i >= 0; i--)
        {
          encodedBlock[i] = (byte)((block % 85) + 33);
          block /= 85;
        }

        for (var i = 0; i < blockCount; i++)
          targetEncodedBase85.Write((char)encodedBlock[i]);
      }
    }
  }
}
