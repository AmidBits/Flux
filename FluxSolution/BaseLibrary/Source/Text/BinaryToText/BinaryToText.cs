using System.Linq;
using System.Runtime.CompilerServices;

namespace Flux
{
  namespace Text
  {
    /// <see href="https://en.wikipedia.org/wiki/Ascii85"/>
    /// <seealso cref="https://github.com/ssg/SimpleBase/blob/main/src/Base85.cs"/>
    public static class BinaryToText
    {
      /// <summary>
      /// Encodes binary data into a plaintext ASCII85 format string
      /// </summary>
      /// <param name="bytes">binary data to encode</param>
      /// <returns>ASCII85 encoded string</returns>
      public static System.ReadOnlySpan<char> EncodeBase85(byte[] bytes)
      {
        var encodedBlock = new byte[5];
        var block = 0U;

        var sb = new System.Text.StringBuilder((int)(bytes.Length * 1.25));

        var count = 0;

        foreach (var b in bytes)
        {
          if (count >= 3)
          {
            block |= b;

            if (block == 0)
              sb.Append('z');
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

        return sb.ToString();

        void EncodeBlock(int blockCount)
        {
          for (var i = 4; i >= 0; i--)
          {
            encodedBlock[i] = (byte)((block % 85) + 33);
            block /= 85;
          }

          for (var i = 0; i < blockCount; i++)
            sb.Append((char)encodedBlock[i]);
        }
      }

      /// <summary>
      /// Decodes an ASCII85 encoded string into the original binary data
      /// </summary>
      /// <param name="characters">ASCII85 encoded string</param>
      /// <returns>byte array of decoded binary data</returns>
      public static byte[] DecodeBase85(System.ReadOnlySpan<char> characters)
      {
        var powersOf85 = new uint[] { 52200625, 614125, 7225, 85, 1 };

        var decodedBlock = new byte[4];
        var count = 0;
        var block = 0U;
        var processChar = false;

        var ms = new System.IO.MemoryStream();

        foreach (var c in characters)
        {
          if (c == 'z')
          {
            if (count != 0) throw new Exception("The character 'z' is invalid inside an ASCII85 block.");

            System.Array.Clear(decodedBlock);

            ms.Write(decodedBlock);

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

              ms.Write(decodedBlock);

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
            ms.WriteByte(decodedBlock[i]);
        }

        return ms.ToArray();

        void DecodeBlock(int blockCount)
        {
          for (var i = 0; i < blockCount; i++)
            decodedBlock[i] = (byte)(block >> 24 - (i * 8));
        }
      }
    }
  }
}

/*
      var sourceText = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.";
      var sourceBytes = System.Text.Encoding.ASCII.GetBytes(sourceText);

      var middleText = Flux.Text.Ascii85.Encode(sourceBytes);

      var targetBytes = Flux.Text.Ascii85.Decode(middleText);
      var targetText = System.Text.Encoding.ASCII.GetString(targetBytes);
 */