using System.Linq;
using System.Runtime.CompilerServices;

namespace Flux
{
  namespace Text
  {
    /// <see href="https://en.wikipedia.org/wiki/Ascii85"/>
    /// <seealso cref="https://github.com/ssg/SimpleBase/blob/main/src/Base85.cs"/>
    public static class Ascii85
    {
      private static readonly uint[] m_powersOf85 = new uint[] { 52200625U, 614125U, 7225U, 85U, 1U };

      public static readonly char[] Characters = new char[85] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u' };

      public static char[] EncodeCharacters(byte[] bytes)
      {
        var padding = bytes.Length % 4 > 0 ? 4 - (bytes.Length % 4) : 0;

        var s = new byte[bytes.Length + padding];
        var t = new char[s.Length + s.Length / 4];

        System.Array.Copy(bytes, s, bytes.Length);

        var si = 0;
        var ti = 0;

        while (si < s.Length)
        {
          var block = 0U;

          for (var i = 0; i < 4 && (si + i) is var sii && sii < s.Length; i++)
          {
            block |= (uint)s[sii] << ((3 - i) * 8);
          }

          si += 4;

          for (var i = 4; i >= 0 && (ti + i) is var tii && tii < t.Length; i--)
          {
            block = (uint)Math.DivRem(block, 85, out long remainder);

            t[tii] = Characters[(int)remainder];
          }

          ti += 5;
        }

        if (padding > 0)
        {
          System.Array.Clear(t, t.Length - padding, padding);

          System.Array.Resize(ref t, t.Length - padding);
        }

        return t;
      }

      public static byte[] DecodeCharacters(char[] characters)
      {
        var padding = characters.Length % 5 > 0 ? 5 - (characters.Length % 5) : 0;

        var s = new char[characters.Length + padding];
        var t = new byte[s.Length - s.Length / 5];

        System.Array.Copy(characters, s, characters.Length);

        var si = 0;
        var ti = 0;

        var value = 0;

        while (si < s.Length)
        {
          for (var i = 0; i < 5 && (si + i) is var sii && sii < s.Length; i++)
          {
            var c = s[sii];

            var index = System.Array.IndexOf(Characters, c);

            value = (value * 85) + index;
          }

          si += 5;

          for (var i = 4; i >= 0 && (ti + i) is var tii && tii < t.Length; i--)
          {
            var b = (byte)((value >> (i * 8)) & 0xFF);

            t[tii] = b;
          }

          ti += 4;
        }

        if (padding > 0)
        {
          System.Array.Clear(t, t.Length - padding, padding);

          System.Array.Resize(ref t, t.Length - padding);
        }

        return t;
      }

      public static byte[] Encode(System.ReadOnlySpan<char> characters)
      {
        if (characters == null) throw new System.ArgumentNullException(nameof(characters));

        using var stream = new System.IO.MemoryStream((int)System.Math.Ceiling(characters.Length * 0.8));

        var count = 0;
        var value = 0U;

        foreach (var character in characters)
        {
          if (character == 'z' && count == 0) stream.Write(EncodeValue(value, 5), 0, 4);
          else if (character < Characters[0] || character > Characters[^1]) throw new System.FormatException($"Invalid character '{character}' in Ascii85 block.");
          else
          {
            try { checked { value += (uint)(m_powersOf85[count] * (character - Characters.First())); } }
            catch (System.OverflowException ex) { throw new System.FormatException("The character block decodes to a value greater than 32-bits.", ex); }

            count++;

            if (count == 5) // On a 5 character boundary, decode value and write bytes.
            {
              stream.Write(EncodeValue(value, count), 0, 4);

              count = 0;
              value = 0;
            }
          }
        }

        if (count > 1)
        {
          for (int padding = count; padding < 5; padding++)
          {
            try { checked { value += 84 * m_powersOf85[padding]; } }
            catch (System.OverflowException ex) { throw new System.FormatException("The final character block decodes to a value greater than 32-bits.", ex); }
          }

          if (EncodeValue(value, count) is var encoded) stream.Write(encoded, 0, encoded.Length);
        }
        else if (count == 0) throw new System.FormatException("The final character block must contain more than one character .");

        return stream.ToArray();
      }

      private static byte[] EncodeValue(uint value, int count)
      {
        var encoded = new byte[count - 1];

        encoded[0] = (byte)(value >> 24);
        if (encoded.Length == 1) return encoded;
        encoded[1] = (byte)((value >> 16) & 0xFF);
        if (encoded.Length == 2) return encoded;
        encoded[2] = (byte)((value >> 8) & 0xFF);
        if (encoded.Length == 3) return encoded;
        encoded[3] = (byte)(value & 0xFF);

        return encoded;
      }

      public static string Decode(byte[] bytes)
      {
        System.ArgumentNullException.ThrowIfNull(bytes);

        var sb = new System.Text.StringBuilder((int)System.Math.Ceiling(bytes.Length * 1.25)); // Output will be 125% of the decoded (original) bytes.

        var value = 0U;
        var count = 0;

        foreach (byte b in bytes)
        {
          value |= ((uint)b) << (24 - (count * 8));
          count++;

          if (count == 4) // On a 4 byte (32-bits) boundary, encode value and append characters.
          {
            if (value == 0) sb.Append('z');
            else sb.Append(DecodeValue(value, count));

            count = 0;
            value = 0;
          }
        }

        if (count > 0) sb.Append(DecodeValue(value, count));

        return sb.ToString();
      }

      private static char[] DecodeValue(uint value, int count)
      {
        var encoded = new char[5];

        for (var index = 4; index >= 0; index--)
        {
          encoded[index] = (char)((value % 85) + Characters.First());

          value /= 85;
        }

        if (count < 4) System.Array.Resize(ref encoded, count + 1);

        return encoded;
      }
    }
  }
}
