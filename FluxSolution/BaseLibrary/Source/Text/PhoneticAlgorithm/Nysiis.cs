using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
  public class Nysiis
    : IPhoneticEncoder
  {
    public int MaxCodeLength { get; set; } = 6;

    #region Regular Expressions for the Nysiis encoder.
    //private static readonly System.Text.RegularExpressions.Regex m_nysiis004 = new System.Text.RegularExpressions.Regex(@"[^A-Z]+");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis01 = new System.Text.RegularExpressions.Regex(@"[^AEIOU]");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis02 = new System.Text.RegularExpressions.Regex(@"[SZ]+$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis03a = new System.Text.RegularExpressions.Regex(@"^MAC");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis03b = new System.Text.RegularExpressions.Regex(@"^PF");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis04a = new System.Text.RegularExpressions.Regex(@"IX$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis04b = new System.Text.RegularExpressions.Regex(@"EX$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis04c = new System.Text.RegularExpressions.Regex(@"(YE|EE|IE)$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis04d = new System.Text.RegularExpressions.Regex(@"(DT|RT|RD|NT|ND)$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis05 = new System.Text.RegularExpressions.Regex(@"(?!^)EV");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis07 = new System.Text.RegularExpressions.Regex(@"(?<=[AEIOU])W");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis08 = new System.Text.RegularExpressions.Regex(@"[AEIOU]+");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis09 = new System.Text.RegularExpressions.Regex(@"GHT");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis10 = new System.Text.RegularExpressions.Regex(@"DG");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis11 = new System.Text.RegularExpressions.Regex(@"PH");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis12 = new System.Text.RegularExpressions.Regex(@"(?!^)(AH|HA)");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis13a = new System.Text.RegularExpressions.Regex(@"KN");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis13b = new System.Text.RegularExpressions.Regex(@"K");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis14 = new System.Text.RegularExpressions.Regex(@"(?!^)M");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis15 = new System.Text.RegularExpressions.Regex(@"(?!^)Q");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis16 = new System.Text.RegularExpressions.Regex(@"SH");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis17 = new System.Text.RegularExpressions.Regex(@"SCH");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis18 = new System.Text.RegularExpressions.Regex(@"YW");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis19 = new System.Text.RegularExpressions.Regex(@"(?!^)Y(?!$)");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis20 = new System.Text.RegularExpressions.Regex(@"WR");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis21 = new System.Text.RegularExpressions.Regex(@"(?!^)Z");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis22 = new System.Text.RegularExpressions.Regex(@"AY$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis23 = new System.Text.RegularExpressions.Regex(@"[AEIOU]+$");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis24 = new System.Text.RegularExpressions.Regex(@"([A-Z])\1+");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis25a = new System.Text.RegularExpressions.Regex(@"[AEIOU]");
    private static readonly System.Text.RegularExpressions.Regex m_nysiis25b = new System.Text.RegularExpressions.Regex(@"^A*");
    #endregion

    /// <summary>Nysiis is similar in nature to the SOUNDEX phonetic encoder, but does of course produce different results. New York State Identification and Intelligence System (NYSIIS) Phonetic Encoder.</summary>
    /// <see cref="https://xlinux.nist.gov/dads/HTML/nysiis.html"/>
    /// <summary>Returns a NYSIIS phonetically coded string.</summary>
    public string EncodePhonetic(System.ReadOnlySpan<char> text)
    {
      var code = string.Concat(GetValidCharacters(text.ToString()));

      if (code.Length > 0)
      {
        // Modified NYSIIS implementation as follows:

        // 1. if the first character of the name is a vowel, remember it;
        var firstCharVowel = m_nysiis01.Replace(code[0].ToString(), string.Empty);
        // 2. remove all 'S' and 'Z' chars from the end of the name
        code = m_nysiis02.Replace(code, string.Empty);
        // 3. transcode first characters of name MAC to MC; PF to F
        code = m_nysiis03a.Replace(code, @"MC");
        code = m_nysiis03b.Replace(code, @"F");
        // 4. transcode trailing strings as follows; IX to IC; EX to EC; YE,EE,IE to Y; DT,RT,RD,NT,ND to D; repeat this last step as necessary
        code = m_nysiis04a.Replace(code, @"IC");
        code = m_nysiis04b.Replace(code, @"EC");
        code = m_nysiis04c.Replace(code, @"Y");
        code = m_nysiis04d.Replace(code, @"D");
        // 5. transcode 'EV' to 'EF' if not at start of name
        code = m_nysiis05.Replace(code, @"EF");
        // 6. use first character of name as first character of key
        //var firstCharOfKey = code[0].ToString();
        // 7. remove any 'W' that follows a vowel
        code = m_nysiis07.Replace(code, string.Empty);
        // 8. replace all vowels with 'A' and collapse all strings of repeated 'A' to one
        code = m_nysiis08.Replace(code, @"A");
        // 9. transcode 'GHT' to 'GT'
        code = m_nysiis09.Replace(code, @"GT");
        // 10. transcode 'DG' to 'G'
        code = m_nysiis10.Replace(code, @"G");
        // 11. transcode 'PH' to 'F'
        code = m_nysiis11.Replace(code, @"F");
        // 12. if not first character, eliminate all 'H' preceded or followed by a vowel
        code = m_nysiis12.Replace(code, @"A");
        // 13. change 'KN' to 'N', else 'K' to 'C'
        code = m_nysiis13a.Replace(code, @"N");
        code = m_nysiis13b.Replace(code, @"C");
        // 14. if not first character, change 'M' to 'N'
        code = m_nysiis14.Replace(code, @"N");
        // 15. if not first character, change 'Q' to 'G'
        code = m_nysiis15.Replace(code, @"G");
        // 16. transcode 'SH' to 'S'
        code = m_nysiis16.Replace(code, @"S");
        // 17. transcode 'SCH' to 'S'
        code = m_nysiis17.Replace(code, @"S");
        // 18. transcode 'YW' to 'Y'
        code = m_nysiis18.Replace(code, @"Y");
        // 19. if not first or last character, change 'Y' to 'A'
        code = m_nysiis19.Replace(code, @"A");
        // 20. transcode 'WR' to 'R'
        code = m_nysiis20.Replace(code, @"R");
        // 21. if not first character, change 'Z' to 'S'
        code = m_nysiis21.Replace(code, @"S");
        // 22. transcode terminal 'AY' to 'Y'
        code = m_nysiis22.Replace(code, @"Y");
        // 23. remove trailing vowels
        code = m_nysiis23.Replace(code, string.Empty);
        // 24. collapse all strings of repeated characters
        code = m_nysiis24.Replace(code, @"$1");
        // 25. if first character of original name is a vowel, prepend to code(or replace first transcoded 'A')
        if (m_nysiis25a.IsMatch(firstCharVowel))
          code = m_nysiis25b.Replace(code, firstCharVowel);

        if (MaxCodeLength < code.Length)
          code = code.Substring(0, MaxCodeLength);
      }

      return code;
    }

    /// <summary>Ensure valid characters for nysiis code generation.</summary>
    public static System.Collections.Generic.IEnumerable<char> GetValidCharacters(string text)
    {
      return string.Concat(text.ToStringBuilder().RemoveDiacriticalMarks(c => c.RemoveDiacriticalLatinStroke()).ToString().Where(c => Flux.Globalization.EnUs.Language.IsEnglishLetter(c)).Select(c => char.ToUpper(c, System.Globalization.CultureInfo.CurrentCulture)));
    }
  }
}
