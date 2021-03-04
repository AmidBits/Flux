using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
  public class EnhancedNysiis
    : IPhoneticAlgorithmEncoder
  {
    public int MaxCodeLength { get; set; } = 8;

    /// <summary>Nysiis is similar in nature to the SOUNDEX phonetic encoder, but does of course produce different results. New York State Identification and Intelligence System (NYSIIS) Phonetic Encoder.</summary>
    /// <see cref="https://xlinux.nist.gov/dads/HTML/nysiis.html"/>
    /// <summary>Returns a NYSIIS phonetically coded string.</summary>
    public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> expression)
    {
      var code = GetValidCharacters(expression.ToString());

      if (code.Length > 0)
      {
        // Modified NYSIIS implementation as follows:

        var firstCharVowel = GlobalizationEnUsLanguage.IsEnglishVowel(code[0], false) ? code[0] : '\0'; // 1, if the first character of the name is a vowel, remember it.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"[SZ]+$", string.Empty); // 2, remove all 'S' and 'Z' chars from the end of the name.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"^MAC", @"MC"); // 3.1, transcode first characters of name MAC to MC.
        code = System.Text.RegularExpressions.Regex.Replace(code, @"^PF", @"F"); // 3.2, transcode first characters of name PF to F.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"IX$", @"IC"); // 4.1, transcode trailing strings IX to IC.
        code = System.Text.RegularExpressions.Regex.Replace(code, @"EX$", @"EC"); // 4.2, transcode trailing strings EX to EC.
        code = System.Text.RegularExpressions.Regex.Replace(code, @"(YE|EE|IE)$", @"Y"); // 4.3, transcode trailing strings YE,EE,IE to Y.
        code = System.Text.RegularExpressions.Regex.Replace(code, @"(DT|RT|RD|NT|ND)$", @"D"); // 4.4, transcode trailing strings DT,RT,RD,NT,ND to D; repeat this last step as necessary.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?!^)EV", @"EF"); // // 5, transcode 'EV' to 'EF' if not at start of name.

        //var firstCharOfKey = code[0].ToString(); // 6, use first character of name as first character of key.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?<=[AEIOU])W", string.Empty); // 7, remove any 'W' that follows a vowel.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"[AEIOU]+", @"A"); // 8, replace all vowels with 'A' and collapse all strings of repeated 'A' to one.

        code = code.Replace(@"GHT", @"GT"); // 9, transcode 'GHT' to 'GT'.

        code = code.Replace(@"DG", @"G"); // 10, transcode 'DG' to 'G'.

        code = code.Replace(@"PH", @"F"); // 11, transcode 'PH' to 'F'

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?!^)(AH|HA)", @"A"); // 12, if not first character, eliminate all 'H' preceded or followed by a vowel

        code = code.Replace(@"KN", @"N"); // 13, change 'KN' to 'N'.
        code = code.Replace(@"K", @"C"); // 13, change 'K' to 'C'.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?!^)M", @"N"); // 14, if not first character, change 'M' to 'N'.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?!^)Q", @"G"); // 15, if not first character, change 'Q' to 'G'.

        code = code.Replace(@"SH", @"S"); // 16, transcode 'SH' to 'S'.

        code = code.Replace(@"SCH", @"S"); // 17, transcode 'SCH' to 'S'.

        code = code.Replace(@"YW", @"Y"); // 18, transcode 'YW' to 'Y'.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?!^)Y(?!$)", @"A"); // 19, if not first or last character, change 'Y' to 'A'.

        code = code.Replace(@"WR", @"R"); // 20, transcode 'WR' to 'R'.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"(?!^)Z", @"S"); // 21, if not first character, change 'Z' to 'S'.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"AY$", @"Y"); // 22, transcode terminal 'AY' to 'Y'.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"[AEIOU]+$", string.Empty); // 23, remove trailing vowels.

        code = System.Text.RegularExpressions.Regex.Replace(code, @"([A-Z])\1+", @"$1"); // 24. collapse all strings of repeated characters.

        // 25. if first character of original name is a vowel, prepend to code(or replace first transcoded 'A')
        if (GlobalizationEnUsLanguage.IsEnglishVowel(firstCharVowel, false))
          code = firstCharVowel + code;

        if (MaxCodeLength < code.Length)
          code = code.Substring(0, MaxCodeLength);
      }

      return code;
    }

    /// <summary>Ensure valid characters for nysiis code generation.</summary>
    public static string GetValidCharacters(string text)
      => string.Concat(text.RemoveDiacriticalMarks(c => SystemCharEm.RemoveDiacriticalLatinStroke(c)).Where(c => GlobalizationEnUsLanguage.IsEnglishLetter(c)).Select(c => char.ToUpper(c, System.Globalization.CultureInfo.CurrentCulture)));
  }
}
