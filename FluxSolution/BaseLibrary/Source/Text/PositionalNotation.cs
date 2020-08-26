namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
  public static class PositionalNotation
  {
    public const string CsGraphemesMustBeUnique = @"Graphemes must be unique.";

    /// <summary>Decimal unicode subscript</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    public static System.Collections.Generic.IList<string> Base10UnicodeSubscript
      => new string[] { "\u2080", "\u2081", "\u2082", "\u2083", "\u2084", "\u2085", "\u2086", "\u2087", "\u2088", "\u2089" };
    /// <summary>Decimal unicode superscript</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    public static System.Collections.Generic.IList<string> Base10UnicodeSuperscript
      => new string[] { "\u2070", "\u00B9", "\u00B2", "\u00B3", "\u2074", "\u2075", "\u2076", "\u2077", "\u2078", "\u2079" };
    /// <summary>The Mayan numeral system was the system to represent numbers and calendar dates in the Maya civilization. It was a vigesimal (base-20) positional numeral system.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Maya_numerals"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Vigesimal"/>
    public static System.Collections.Generic.IList<string> Base20UnicodeMayaNumerals
      => new string[] { "\U0001D2E0", "\U0001D2E1", "\U0001D2E2", "\U0001D2E3", "\U0001D2E4", "\U0001D2E5", "\U0001D2E6", "\U0001D2E7", "\U0001D2E8", "\U0001D2E9", "\U0001D2EA", "\U0001D2EB", "\U0001D2EC", "\U0001D2ED", "\U0001D2EE", "\U0001D2EF", "\U0001D2F0", "\U0001D2F1", "\U0001D2F2", "\U0001D2F3" };
    public static System.Collections.Generic.IList<string> Base2
      => new string[] { "0", "1" };
    public static System.Collections.Generic.IList<string> Base8
      => new string[] { "0", "1", "2", "3", "4", "5", "6", "7" };
    public static System.Collections.Generic.IList<string> Base10
      => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    public static System.Collections.Generic.IList<string> Base16
      => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
    /// <summary>Hexatrigesimal, a.k.a. Base36.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Base36"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Vigesimal"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Hexadecimal"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Duodecimal"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Octal"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Binary_number"/>
    /// <summary>Custom Base62 with decimal single digit numbers, english upper alpha and english lower alpha.</summary>
    public static System.Collections.Generic.IList<string> Base62
      => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    /// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
    public static System.Collections.Generic.IList<string> PrintableAscii
      => new string[] { " ", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~" };

    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    /// System.Collections.Generic.IList<string>
    public static string NumberToText(System.Numerics.BigInteger number, System.Collections.Generic.IList<string> symbols)
    {
      if (symbols is null) throw new System.ArgumentNullException(nameof(symbols));

      var sb = new System.Text.StringBuilder(128);

      while (number != 0)
      {
        number = System.Numerics.BigInteger.DivRem(number, symbols.Count, out var remainder);

        sb.Insert(0, symbols[(int)System.Numerics.BigInteger.Abs(remainder)]);
      }

      return sb.ToString();
    }
    public static string NumberToText(System.Numerics.BigInteger number, params string[] symbols)
      => NumberToText(number, (System.Collections.Generic.IList<string>)symbols);
    public static bool TryNumberToText(System.Numerics.BigInteger number, out string? result, params string[] symbols)
    {
      try
      {
        result = NumberToText(number, symbols);
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
    public static System.Numerics.BigInteger TextToNumber(string number, System.Collections.Generic.IList<string> symbols)
    {
      if (symbols is null) throw new System.ArgumentNullException(nameof(symbols));

      var bi = System.Numerics.BigInteger.Zero;

      foreach (var textElement in number.GetTextElements())
      {
        bi *= symbols.Count;

        var position = symbols.IndexOf(textElement);

        bi += position > -1 ? position : throw new System.ArgumentOutOfRangeException(nameof(symbols));
      }

      return bi;
    }
    public static System.Numerics.BigInteger TextToNumber(string number, params string[] symbols)
      => TextToNumber(number, (System.Collections.Generic.IList<string>)symbols);
    /// <summary>Convert a positional notation text string into a number.</summary>
    public static bool TryTextToNumber(string number, out System.Numerics.BigInteger result, params string[] symbols)
    {
      try
      {
        result = TextToNumber(number, symbols);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default;
      return false;
    }

    public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
    {
      var dictionary = new System.Collections.Generic.Dictionary<int, string>();

      for (var radix = 2; radix <= 62; radix++)
      {
        dictionary.Add(radix, NumberToText(number, Base62.ToArray(0, radix)));
      }

      return dictionary;
    }
  }
}

// using System.Linq;

// namespace Flux
// {
//   public static partial class Convert
//   {
//     /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
//     public static class PositionalNotation
//     {
//       public const string CsGraphemesMustBeUnique = @"Graphemes must be unique.";

//       /// <summary>Binary</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Binary_number"/>
//       public static string[] Base2 => new string[] { "0", "1" };
//       /// <summary>Octal</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Octal"/>
//       public static string[] Base8 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7" };
//       /// <summary>Decimal</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Decimal"/>
//       public static string[] Base10 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
//       /// <summary>Decimal subscript</summary>
//       public static string[] Base10Subscript => new string[] { "\u2080", "\u2081", "\u2082", "\u2083", "\u2084", "\u2085", "\u2086", "\u2087", "\u2088", "\u2089" };
//       /// <summary>Decimal superscript</summary>
//       public static string[] Base10Superscript => new string[] { "\u2070", "\u00B9", "\u00B2", "\u00B3", "\u2074", "\u2075", "\u2076", "\u2077", "\u2078", "\u2079" };
//       /// <summary>Duodecimal</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Duodecimal"/>
//       public static string[] Base12 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B" };
//       /// <summary>Hexadecimal</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Hexadecimal"/>
//       public static string[] Base16 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
//       /// <summary>Vigesimal</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Vigesimal"/>
//       public static string[] Base20 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
//       /// <summary>The Mayan numeral system was the system to represent numbers and calendar dates in the Maya civilization. It was a vigesimal (base-20) positional numeral system.</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Maya_numerals"/>
//       public static string[] Base20UnicodeMayaNumerals => new string[] { "\U0001D2E0", "\U0001D2E1", "\U0001D2E2", "\U0001D2E3", "\U0001D2E4", "\U0001D2E5", "\U0001D2E6", "\U0001D2E7", "\U0001D2E8", "\U0001D2E9", "\U0001D2EA", "\U0001D2EB", "\U0001D2EC", "\U0001D2ED", "\U0001D2EE", "\U0001D2EF", "\U0001D2F0", "\U0001D2F1", "\U0001D2F2", "\U0001D2F3" };
//       /// <summary>Decimal</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Decimal"/>
//       public static string[] Base30 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
//       /// <summary>Hexatrigesimal, a.k.a. Base36.</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Base36"/>
//       public static string[] Base36 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
//       /// <summary>Custom Base62 with decimal single digit numbers, english upper alpha and english lower alpha.</summary>
//       public static string[] Base62 => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

//       /// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
//       public static string[] PrintableAscii => System.Linq.Enumerable.Range(32, 95).Select(i => System.Convert.ToChar(i).ToString()).ToArray();

//       /// <summary>Convert a number into a positional notation text string.</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
//       /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
//       public static string NumberToText(System.Numerics.BigInteger number, params string[] symbols) => string.Concat(NumberToTextElementsReversed(number, symbols).Reverse());
//       public static string NumberToText(System.Numerics.BigInteger number, int radix) => radix >= 2 && radix <= 62 ? string.Concat(NumberToTextElementsReversed(number, Base62.SubArray(0, radix))) : throw new System.ArgumentOutOfRangeException(nameof(radix));
//       /// <summary>Convert a number to a positional notation text string, in the form of a sequence of text elements.</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
//       /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
//       public static System.Collections.Generic.IEnumerable<string> NumberToTextElementsReversed(System.Numerics.BigInteger number, params string[] symbols)
//       {
//         var radices = symbols.Length == symbols.Distinct().Count() ? symbols : throw new System.ArgumentException(CsGraphemesMustBeUnique);

//         while (number != 0)
//         {
//           number = System.Numerics.BigInteger.DivRem(number, radices.Length, out var remainder);

//           if (remainder < radices.Length && remainder > -radices.Length)
//           {
//             yield return radices[Math.Abs((int)remainder)];
//           }
//           else
//           {
//             throw new System.ArithmeticException($"Invalid remainder <{remainder}> for the specified radix {radices.Length} ({string.Join(@", ", radices)}).");
//           }
//         }
//       }
//       /// <summary>Convert a positional notation text string, in the form of a sequence ot text elements, to a number.</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
//       /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
//       public static System.Numerics.BigInteger TextElementsToNumber(System.Collections.Generic.IEnumerable<string> textElementsOfNumber, params string[] symbols)
//       {
//         var radices = symbols.Length == symbols.Distinct().Count() ? symbols : throw new System.ArgumentException(CsGraphemesMustBeUnique);

//         var bi = System.Numerics.BigInteger.Zero;

//         foreach (var textElement in textElementsOfNumber)
//         {
//           bi *= radices.Length;

//           if (radices.Contains(textElement))
//           {
//             bi += System.Array.FindIndex(radices, rg => rg.Equals(textElement));
//           }
//           else
//           {
//             throw new System.ArgumentOutOfRangeException($"Unrecognized text element <{textElement}> for the specified radix {radices.Length} ({string.Join(@", ", radices)}).");
//           }
//         }

//         return bi;
//       }
//       /// <summary>Convert a positional notation text string into a number.</summary>
//       /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
//       /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
//       public static System.Numerics.BigInteger TextToNumber(string number, params string[] symbols) => TextElementsToNumber(number.GetTextElements(), symbols);
//       public static System.Numerics.BigInteger TextToNumber(string number, int radix) => radix >= 2 && radix <= 62 ? TextElementsToNumber(number.GetTextElements(), Base62.SubArray(0, radix)) : throw new System.ArgumentOutOfRangeException(nameof(radix));

//       public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
//       {
//         var dictionary = new System.Collections.Generic.Dictionary<int, string>();

//         for (var radix = 2; radix <= 62; radix++)
//         {
//           dictionary.Add(radix, NumberToText(number, Base62.SubArray(0, radix)));
//         }

//         return dictionary;
//       }
//     }
//   }
// }
