using System.Linq;

namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Ascii85"/>
  public static class Ascii85
  {
    private static readonly uint[] _powersOf85 = new uint[] { 52200625U, 614125U, 7225U, 85U, 1U };

    public static readonly char[] Characters = System.Linq.Enumerable.Range(33, 85).Select(i => System.Convert.ToChar(i)).ToArray();

    public static byte[] Decode(string characters)
    {
      if (characters == null) throw new System.ArgumentNullException(nameof(characters));

      using (var stream = new System.IO.MemoryStream((int)System.Math.Ceiling(characters.Length * 0.8))) // Output will be 80% of the encoded string.
      {
        var count = 0;
        var value = 0U;

        foreach (char c in characters)
        {
          if (c == 'z' && count == 0) stream.Write(DecodeValue(value, 5), 0, 4);
          else if (c < Characters.First() || c > Characters.Last()) throw new System.FormatException($"Invalid character '{c}' in Ascii85 block.");
          else
          {
            try { checked { value += (uint)(_powersOf85[count] * (c - Characters.First())); } }
            catch (System.OverflowException ex) { throw new System.FormatException("The character block decodes to a value greater than 32-bits.", ex); }

            count++;

            if (count == 5) // On a 5 character boundary, decode value and write bytes.
            {
              stream.Write(DecodeValue(value, count), 0, 4);

              count = 0;
              value = 0;
            }
          }
        }

        if (count > 1)
        {
          for (int padding = count; padding < 5; padding++)
          {
            try { checked { value += 84 * _powersOf85[padding]; } }
            catch (System.OverflowException ex) { throw new System.FormatException("The final character block decodes to a value greater than 32-bits.", ex); }
          }

          if (DecodeValue(value, count) is var decoded) stream.Write(decoded, 0, decoded.Length);
        }
        else if (count == 0) throw new System.FormatException("The final character block must contain more than one character .");

        return stream.ToArray();
      }
    }

    private static byte[] DecodeValue(uint value, int count)
    {
      var decoded = new byte[count - 1];

      decoded[0] = (byte)(value >> 24);
      if (decoded.Length == 1) return decoded;
      decoded[1] = (byte)((value >> 16) & 0xFF);
      if (decoded.Length == 2) return decoded;
      decoded[2] = (byte)((value >> 8) & 0xFF);
      if (decoded.Length == 3) return decoded;
      decoded[3] = (byte)(value & 0xFF);

      return decoded;
    }

    public static string Encode(byte[] bytes)
    {
      if (bytes == null) throw new System.ArgumentNullException(nameof(bytes));

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
          else sb.Append(EncodeValue(value, count));

          count = 0;
          value = 0;
        }
      }

      if (count > 0) sb.Append(EncodeValue(value, count));

      return sb.ToString();
    }

    private static char[] EncodeValue(uint value, int count)
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
