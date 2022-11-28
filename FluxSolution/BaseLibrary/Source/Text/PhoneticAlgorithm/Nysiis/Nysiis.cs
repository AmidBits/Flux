using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
  // https://rosettacode.org/wiki/NYSIIS?source=post_page-----50165e684526----------------------#Java
  public sealed class Nysiis
    : IPhoneticAlgorithmEncoder
  {
    public int MaxCodeLength { get; set; } = 8;

    //private static System.Text.StringBuilder ReplaceEqualAt(System.Text.StringBuilder text, int startAt, System.ReadOnlySpan<char> key, System.ReadOnlySpan<char> value)
    //{
    //  if (text.EqualsAt(startAt, key))
    //  {
    //    text.Remove(startAt, key.Length);
    //    text.Insert(startAt, value);
    //  }

    //  return text;
    //}

    /// <summary>Nysiis is similar in nature to the SOUNDEX phonetic encoder, but does of course produce different results. New York State Identification and Intelligence System (NYSIIS) Phonetic Encoder.</summary>
    /// <see cref="https://xlinux.nist.gov/dads/HTML/nysiis.html"/>
    /// <summary>Returns a NYSIIS phonetically coded string.</summary>
    public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> expression)
    {
      var sb = new System.Text.StringBuilder(GetValidCharacters(expression.ToString()));

      sb.ReplaceIfEqualAt(0, @"MAC", @"MCC");
      sb.ReplaceIfEqualAt(0, @"KN", @"N");
      sb.ReplaceIfEqualAt(0, @"K", @"C");
      sb.ReplaceIfEqualAt(0, @"PH", @"FF");
      sb.ReplaceIfEqualAt(0, @"PF", @"FF");
      sb.ReplaceIfEqualAt(0, @"SCH", @"SSS");

      // Replace end...

      sb.ReplaceIfEqualAt(sb.Length-3, @"SCH", @"SSS");

      if (sb.Length > MaxCodeLength)
        sb.Remove(0, MaxCodeLength);

      return sb.ToString();
    }

    /// <summary>Ensure valid characters for nysiis code generation.</summary>
    public static string GetValidCharacters(string text)
    {
      return string.Concat(text.RemoveDiacriticalMarks(c => (char)((System.Text.Rune)c).ReplaceDiacriticalLatinStroke().Value).Where(c => ExtensionMethods.IsEnglishLetter((System.Text.Rune)c)).Select(c => char.ToUpper(c, System.Globalization.CultureInfo.CurrentCulture)));
    }
  }
}
