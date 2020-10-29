using System.Linq;

namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
  public class PositionalNotation
  {
    public static readonly PositionalNotation Base2 = new PositionalNotation("0", "1");
    public static readonly PositionalNotation Base8 = new PositionalNotation("0", "1", "2", "3", "4", "5", "6", "7");
    public static readonly PositionalNotation Base10 = new PositionalNotation("0", "1", "2", "3", "4", "5", "6", "7", "8", "9");
    public static readonly PositionalNotation Base16 = new PositionalNotation("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F");

    /// <summary>Tetrasexagesimal (https://en.wikipedia.org/wiki/Base64)</summary>
    public static readonly PositionalNotation Base64 = new PositionalNotation("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "/");

    /// <summary>The Mayan numeral system was the system to represent numbers and calendar dates in the Maya civilization. It was a vigesimal (base-20) positional numeral system.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Maya_numerals"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Vigesimal"/>
    public static readonly PositionalNotation MayanNumeralSystem = new PositionalNotation("\U0001D2E0", "\U0001D2E1", "\U0001D2E2", "\U0001D2E3", "\U0001D2E4", "\U0001D2E5", "\U0001D2E6", "\U0001D2E7", "\U0001D2E8", "\U0001D2E9", "\U0001D2EA", "\U0001D2EB", "\U0001D2EC", "\U0001D2ED", "\U0001D2EE", "\U0001D2EF", "\U0001D2F0", "\U0001D2F1", "\U0001D2F2", "\U0001D2F3");

    /// <summary>Decimal unicode subscript</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    public static string[] Base10UnicodeSubscript()
      => new string[] { "\u2080", "\u2081", "\u2082", "\u2083", "\u2084", "\u2085", "\u2086", "\u2087", "\u2088", "\u2089" };

    /// <summary>Decimal unicode superscript</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    public static string[] Base10UnicodeSuperscript()
      => new string[] { "\u2070", "\u00B9", "\u00B2", "\u00B3", "\u2074", "\u2075", "\u2076", "\u2077", "\u2078", "\u2079" };

    public static char[] Base62()
      => new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    /// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
    public static string[] PrintableAscii()
      => new string[] { " ", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~" };

    public System.Collections.Generic.IList<string> Symbols { get; }

    public PositionalNotation(System.Collections.Generic.IEnumerable<string> symbols)
      => Symbols = symbols.ToList();
    public PositionalNotation(params string[] symbols)
      => Symbols = symbols;

    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    /// System.Collections.Generic.IList<string>
    public string NumberToText(System.Numerics.BigInteger number)
    {
      var sb = new System.Text.StringBuilder(128);

      if (number.IsZero)
      {
        sb.Append('0');
      }
      else while (number != 0)
        {
          number = System.Numerics.BigInteger.DivRem(number, Symbols.Count, out var remainder);

          sb.Insert(0, Symbols[(int)System.Numerics.BigInteger.Abs(remainder)]);
        }

      return sb.ToString();
    }
    public bool TryNumberToText(System.Numerics.BigInteger number, out string? result)
    {
      try
      {
        result = NumberToText(number);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default;
      return false;
    }

    /// <summary>Convert a positional notation text string into a number.</summary>
    /// <param name="number">Must consist of only TextElements (i.e. graphemes).</param>
    /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    public System.Numerics.BigInteger TextToNumber(string number)
    {
      var bi = System.Numerics.BigInteger.Zero;

      foreach (var textElement in number.GetTextElements())
      {
        bi *= Symbols.Count;

        var position = Symbols.IndexOf(textElement);

        bi += position > -1 ? position : throw new System.InvalidOperationException();
      }

      return bi;
    }
    /// <summary>Convert a positional notation text string into a number.</summary>
    public bool TryTextToNumber(string number, out System.Numerics.BigInteger result)
    {
      try
      {
        result = TextToNumber(number);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default;
      return false;
    }

    /// <summary>Custom instance based on Base62 which results in traditional radix conversions.</summary>
    public static PositionalNotation GetFromRadix(int radix)
      => radix switch
      {
        2 => Base2,
        8 => Base8,
        10 => Base10,
        16 => Base16,
        var r when r >= 2 && r <= 62 => new PositionalNotation(Base62().Take(radix).Select(c => c.ToString())),
        _ => throw new System.ArgumentOutOfRangeException(nameof(radix))
      };

    public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
    {
      var dictionary = new System.Collections.Generic.Dictionary<int, string>();
      for (var radix = 2; radix <= 62; radix++)
        dictionary.Add(radix, GetFromRadix(radix).NumberToText(number));
      return dictionary;
    }
  }
}
