namespace Flux.Text
{
  public static class BinaryToText
  {
    /// <summary>Base 2</summary>
    public static char[] Binary => new char[] { '0', '1' };
    /// <summary>Base 3</summary>
    public static char[] Ternary => new char[] { '0', '1', '2' };
    /// <summary>Base 4</summary>
    public static char[] Quaternary => new char[] { '0', '1', '2', '3' };
    /// <summary>Base 5</summary>
    public static char[] Quinary => new char[] { '0', '1', '2', '3', '4' };
    /// <summary>Base 6</summary>
    public static char[] Senary => new char[] { '0', '1', '2', '3', '4', '5' };
    /// <summary>Base 8</summary>
    public static char[] Octal => new char[] { '0', '1', '2', '3', '4', '5', '6', '7' };
    /// <summary>Base 10</summary>
    public static char[] Decimal => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    /// <summary>Base 12</summary>
    public static char[] Duodecimal => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B' };
    /// <summary>Base 16</summary>
    public static char[] Hexadecimal => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
    /// <summary>Base 20</summary>
    public static char[] Vigesimal => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
    /// <summary>Duotrigesimal (https://en.wikipedia.org/wiki/Base32)</summary>
    public static char[] Base32 => new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '2', '3', '4', '5', '6', '7' };
    /// <summary>Duotrigesimal (https://en.wikipedia.org/wiki/Natural_Area_Code)</summary>
    public static char[] NaturalAreaCode => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z' };
    /// <summary>Base 36</summary>
    public static char[] Hexatrigesimal => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
    /// <summary>NumericAlpha (62 characters)</summary>
    public static char[] NumericAlpha => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    /// <summary>Tetrasexagesimal (https://en.wikipedia.org/wiki/Base64)</summary>
    public static char[] Base64 => new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };
    /// <summary>Pentoctogesimal (https://en.wikipedia.org/wiki/Ascii85)</summary>
    public static char[] Ascii85 => new char[] { ' ', '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't' };
    /// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
    public static char[] PrintableAscii => new char[] { ' ', '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~' };
    /// <summary>Base 94 (https://en.wikipedia.org/wiki/ASCII)</summary>
    public static char[] PrintableAsciiNoSpace => new char[] { '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~' };
    /// <summary>Base 94 (https://en.wikipedia.org/wiki/Base64#Radix-64_applications_not_compatible_with_Base64)</summary>
    public static char[] Uuencoding => new char[] { ' ', '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_' };

    public static string QuotedPrintable(byte value) => value >= 33 && value <= 126 && value != 61 ? new string(System.Text.Encoding.UTF8.GetChars(new byte[] { value })) : value.ToString("X2");

    public static string Encode(System.Numerics.BigInteger number, params char[] baseCharacters)
    {
      var sb = new System.Text.StringBuilder(64);

      if (number.IsZero)
      {
        sb.Append('0');
      }
      else if (System.Numerics.BigInteger.Abs(number) is System.Numerics.BigInteger abs)
      {
        for (; abs != 0; abs /= baseCharacters.Length)
        {
          System.Numerics.BigInteger.DivRem(abs, baseCharacters.Length, out var remainder);

          sb.Insert(0, baseCharacters[(int)remainder]);
        }

        if (number.Sign < 0)
        {
          sb.Insert(0, '-');
        }
      }

      return sb.ToString();
    }

    public static System.Numerics.BigInteger Decode(string number, params char[] baseCharacters)
    {
      if (number is null) throw new System.ArgumentNullException(nameof(number));

      var result = System.Numerics.BigInteger.Zero;

      if (!System.Text.RegularExpressions.Regex.IsMatch(number, "^-?0?$") && number.Replace(@"-", string.Empty, System.StringComparison.Ordinal) is string s && s.Length > 0)
      {
        var multiplier = System.Numerics.BigInteger.Pow(baseCharacters.Length, s.Length - 1);

        foreach (var c in s)
        {
          result += System.Array.FindIndex(baseCharacters, bc => bc == c) * multiplier;

          multiplier /= baseCharacters.Length;
        }

        if (number[0] == '-')
        {
          result *= -1;
        }
      }

      return result;
    }
  }
}
