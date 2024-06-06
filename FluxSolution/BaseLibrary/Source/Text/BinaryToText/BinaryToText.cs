namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Encodes <paramref name="text"/> (using the specified <paramref name="encoding"/>) into Base85/ASCII85 <paramref name="encodedBase85"/>.</para>
    /// </summary>
    /// <param name="text">A string of text to encode.</param>
    /// <param name="encodedBase85">A Base85/ASCII85 encoded string.</param>
    /// <param name="encoding">The encoding is only used to convert the string <paramref name="text"/> into binary data for Base85/ASCII85 encoding.</param>
    /// <returns>Whether the operation was succesful.</returns>
    public static bool TryEncodeBase85(this string text, out string encodedBase85, System.Text.Encoding? encoding = null)
    {
      encoding ??= System.Text.Encoding.UTF8;

      return Text.BinaryToText.TryEncodeBase85(encoding.GetBytes(text), out encodedBase85);
    }

    /// <summary>
    /// <para>Decodes <paramref name="encodedBase85"/> into <paramref name="decodedText"/> (using the specified <paramref name="encoding"/>).</para>
    /// </summary>
    /// <param name="encodedBase85">A Base85/ASCII85 encoded string.</param>
    /// <param name="decodedText">A string of decoded text.</param>
    /// <param name="encoding">The encoding is only used to convert the decoded Base85/ASCII85 binary data into the string <paramref name="decodedText"/>.</param>
    /// <returns>Whether the operation was succesful.</returns>
    public static bool TryDecodeBase85(this string encodedBase85, out string decodedText, System.Text.Encoding? encoding = null)
    {
      encoding ??= System.Text.Encoding.UTF8;

      var rv = Text.BinaryToText.TryDecodeBase85(encodedBase85, out byte[] decodedBase85);

      decodedText = rv ? encoding.GetString(decodedBase85) : string.Empty;

      return rv;
    }
  }

  namespace Text
  {
    /// <see href="https://en.wikipedia.org/wiki/Ascii85"/>
    /// <seealso cref="https://github.com/ssg/SimpleBase/blob/main/src/Base85.cs"/>
    public static partial class BinaryToText
    {
      /// <summary>
      /// <para>Encodes <paramref name="sourceBinaryData"/> into <paramref name="targetEncodedBase85"/>.</para>
      /// </summary>
      /// <param name="sourceBinaryData">Binary data to encode.</param>
      /// <param name="targetEncodedBase85">Encoded Base85/ASCII85 text.</param>
      public static void EncodeBase85(System.IO.Stream sourceBinaryData, System.IO.TextWriter targetEncodedBase85)
      {
        var encodedBlock = new byte[5];
        var block = 0U;

        //var sb = new System.Text.StringBuilder();

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

      /// <summary>
      /// <para>Encodes <paramref name="bytes"/> into Base85/ASCII85 text.</para>
      /// </summary>
      /// <param name="bytes">A byte array of binary data.</param>
      /// <returns>A Base85/ASCII85 encoded string.</returns>
      public static string EncodeBase85(byte[] bytes)
      {
        using var ms = new System.IO.MemoryStream(bytes);

        var sb = new System.Text.StringBuilder();

        using var sw = new System.IO.StringWriter(sb);

        EncodeBase85(ms, sw);

        return sb.ToString();
      }

      /// <summary>
      /// <para>Encodes <paramref name="bytes"/> into <paramref name="encodedBase85"/>.</para>
      /// </summary>
      /// <param name="bytes">A byte array of binary data.</param>
      /// <param name="encodedBase85">A Base85/ASCII85 encoded string.</param>
      /// <returns>Whether the operation was succesful.</returns>
      public static bool TryEncodeBase85(byte[] bytes, out string encodedBase85)
      {
        try
        {
          encodedBase85 = EncodeBase85(bytes);
          return true;
        }
        catch { }

        encodedBase85 = string.Empty;
        return false;
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
      /// <para>Decodes <paramref name="encodedBase85"/> into binary data.</para>
      /// </summary>
      /// <param name="encodedBase85">A Base85/ASCII85 encoded string.</param>
      /// <returns>A byte array of decoded binary data.</returns>
      public static byte[] DecodeBase85(string encodedBase85)
      {
        using var sr = new System.IO.StringReader(encodedBase85);

        using var ms = new System.IO.MemoryStream();

        DecodeBase85(sr, ms);

        return ms.ToArray();
      }

      /// <summary>
      /// <para>Decodes <paramref name="encodedBase85"/> into <paramref name="decodedBytes"/>.</para>
      /// </summary>
      /// <param name="encodedBase85">A string of Base85/ASCII85 encoded text.</param>
      /// <param name="decodedBytes">A byte array of decoded binary data.</param>
      /// <returns>Whether the operation was succesful.</returns>
      public static bool TryDecodeBase85(string encodedBase85, out byte[] decodedBytes)
      {
        try
        {
          decodedBytes = DecodeBase85(encodedBase85);
          return true;
        }
        catch { }

        decodedBytes = System.Array.Empty<byte>();
        return false;
      }
    }
  }
}

/*
      var sourceText = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.";
      var sourceBytes = System.Text.Encoding.ASCII.GetBytes(sourceText);

      var middleText = Flux.Text.BinaryToText.EncodeBase85(sourceBytes);

      var targetBytes = Flux.Text.BinaryToText.DecodeBase85(middleText);
      var targetText = System.Text.Encoding.ASCII.GetString(targetBytes);
 */

/*
      var text = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.";

      Flux.Text.BinaryToText.TryEncodeBase85(text, out string encodedBase85);
      Flux.Text.BinaryToText.TryDecodeBase85(encodedBase85, out string decodedText);
 */
