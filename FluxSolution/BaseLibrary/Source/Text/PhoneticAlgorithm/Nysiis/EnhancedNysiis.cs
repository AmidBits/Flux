namespace Flux.Text.PhoneticAlgorithm
{
  public sealed partial class EnhancedNysiis
    : IPhoneticAlgorithmEncoder
  {
    public int MaxCodeLength { get; set; } = 8;

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"[SZ]+$")] private static partial System.Text.RegularExpressions.Regex RegexStep2();
#else
    private static System.Text.RegularExpressions.Regex RegexStep2() => new(@"[SZ]+$");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"^MAC")] private static partial System.Text.RegularExpressions.Regex RegexStep31();
#else
    private static System.Text.RegularExpressions.Regex RegexStep31() => new(@"^MAC");
#endif
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"^PF")] private static partial System.Text.RegularExpressions.Regex RegexStep32();
#else
    private static System.Text.RegularExpressions.Regex RegexStep32() => new(@"^PF");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"IX$")] private static partial System.Text.RegularExpressions.Regex RegexStep41();
#else
    private static System.Text.RegularExpressions.Regex RegexStep41() => new(@"IX$");
#endif
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"EX$")] private static partial System.Text.RegularExpressions.Regex RegexStep42();
#else
    private static System.Text.RegularExpressions.Regex RegexStep42() => new(@"EX$");
#endif
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(YE|EE|IE)$")] private static partial System.Text.RegularExpressions.Regex RegexStep43();
#else
    private static System.Text.RegularExpressions.Regex RegexStep43() => new(@"(YE|EE|IE)$");
#endif
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(DT|RT|RD|NT|ND)$")] private static partial System.Text.RegularExpressions.Regex RegexStep44();
#else
    private static System.Text.RegularExpressions.Regex RegexStep44() => new(@"(DT|RT|RD|NT|ND)$");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?!^)EV")] private static partial System.Text.RegularExpressions.Regex RegexStep5();
#else
    private static System.Text.RegularExpressions.Regex RegexStep5() => new(@"(?<=[AEIOU])W");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<=[AEIOU])W")] private static partial System.Text.RegularExpressions.Regex RegexStep7();
#else
    private static System.Text.RegularExpressions.Regex RegexStep7() => new(@"(?<=[AEIOU])W");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"[AEIOU]+")] private static partial System.Text.RegularExpressions.Regex RegexStep8();
#else
    private static System.Text.RegularExpressions.Regex RegexStep8() => new(@"[AEIOU]+");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?!^)(AH|HA)")] private static partial System.Text.RegularExpressions.Regex RegexStep12();
#else
    private static System.Text.RegularExpressions.Regex RegexStep12() => new(@"(?!^)(AH|HA)");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?!^)M")] private static partial System.Text.RegularExpressions.Regex RegexStep14();
#else
    private static System.Text.RegularExpressions.Regex RegexStep14() => new(@"(?!^)M");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?!^)Q")] private static partial System.Text.RegularExpressions.Regex RegexStep15();
#else
    private static System.Text.RegularExpressions.Regex RegexStep15() => new(@"(?!^)Q");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?!^)Y(?!$)")] private static partial System.Text.RegularExpressions.Regex RegexStep19();
#else
    private static System.Text.RegularExpressions.Regex RegexStep19() => new(@"(?!^)Y(?!$)");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?!^)Z")] private static partial System.Text.RegularExpressions.Regex RegexStep21();
#else
    private static System.Text.RegularExpressions.Regex RegexStep21() => new(@"(?!^)Z");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"AY$")] private static partial System.Text.RegularExpressions.Regex RegexStep22();
#else
    private static System.Text.RegularExpressions.Regex RegexStep22() => new(@"AY$");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"[AEIOU]+$")] private static partial System.Text.RegularExpressions.Regex RegexStep23();
#else
    private static System.Text.RegularExpressions.Regex RegexStep23() => new(@"[AEIOU]+$");
#endif

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"([A-Z])\1+")] private static partial System.Text.RegularExpressions.Regex RegexStep24();
#else
    private static System.Text.RegularExpressions.Regex RegexStep24() => new(@"([A-Z])\1+");
#endif

    /// <summary>Nysiis is similar in nature to the SOUNDEX phonetic encoder, but does of course produce different results. New York State Identification and Intelligence System (NYSIIS) Phonetic Encoder.</summary>
    /// <see cref="https://xlinux.nist.gov/dads/HTML/nysiis.html"/>
    /// <summary>Returns a NYSIIS phonetically coded string.</summary>
    public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> expression)
    {
      var code = GetValidCharacters(expression.ToString());

      if (code.Length > 0)
      {
        // Modified NYSIIS implementation as follows:

        var firstCharVowel = !code[0].IsBasicLatinLetterY() && System.Globalization.CultureInfo.CurrentCulture.IsVowelOf(code[0]) ? code[0] : '\0'; // 1, if the first character of the name is a vowel, remember it.

        code = RegexStep2().Replace(code, string.Empty); // 2, remove all 'S' and 'Z' chars from the end of the name.

        code = RegexStep31().Replace(code, @"MC"); // 3.1, transcode first characters of name MAC to MC.
        code = RegexStep32().Replace(code, @"F"); // 3.2, transcode first characters of name PF to F.

        code = RegexStep41().Replace(code, @"IC"); // 4.1, transcode trailing strings IX to IC.
        code = RegexStep42().Replace(code, @"EC"); // 4.2, transcode trailing strings EX to EC.
        code = RegexStep43().Replace(code, @"Y"); // 4.3, transcode trailing strings YE,EE,IE to Y.
        code = RegexStep44().Replace(code, @"D"); // 4.4, transcode trailing strings DT,RT,RD,NT,ND to D; repeat this last step as necessary.

        code = RegexStep5().Replace(code, @"EF"); // // 5, transcode 'EV' to 'EF' if not at start of name.

        //var firstCharOfKey = code[0].ToString(); // 6, use first character of name as first character of key.

        code = RegexStep7().Replace(code, string.Empty); // 7, remove any 'W' that follows a vowel.

        code = RegexStep8().Replace(code, @"A"); // 8, replace all vowels with 'A' and collapse all strings of repeated 'A' to one.

        code = code.Replace(@"GHT", @"GT"); // 9, transcode 'GHT' to 'GT'.

        code = code.Replace(@"DG", @"G"); // 10, transcode 'DG' to 'G'.

        code = code.Replace(@"PH", @"F"); // 11, transcode 'PH' to 'F'

        code = RegexStep12().Replace(code, @"A"); // 12, if not first character, eliminate all 'H' preceded or followed by a vowel

        code = code.Replace(@"KN", @"N"); // 13, change 'KN' to 'N'.
        code = code.Replace(@"K", @"C"); // 13, change 'K' to 'C'.

        code = RegexStep14().Replace(code, @"N"); // 14, if not first character, change 'M' to 'N'.

        code = RegexStep15().Replace(code, @"G"); // 15, if not first character, change 'Q' to 'G'.

        code = code.Replace(@"SH", @"S"); // 16, transcode 'SH' to 'S'.

        code = code.Replace(@"SCH", @"S"); // 17, transcode 'SCH' to 'S'.

        code = code.Replace(@"YW", @"Y"); // 18, transcode 'YW' to 'Y'.

        code = RegexStep19().Replace(code, @"A"); // 19, if not first or last character, change 'Y' to 'A'.

        code = code.Replace(@"WR", @"R"); // 20, transcode 'WR' to 'R'.

        code = RegexStep21().Replace(code, @"S"); // 21, if not first character, change 'Z' to 'S'.

        code = RegexStep22().Replace(code, @"Y"); // 22, transcode terminal 'AY' to 'Y'.

        code = RegexStep23().Replace(code, string.Empty); // 23, remove trailing vowels.

        code = RegexStep24().Replace(code, @"$1"); // 24. collapse all strings of repeated characters.

        // 25. if first character of original name is a vowel, prepend to code(or replace first transcoded 'A')
        if (!firstCharVowel.IsBasicLatinLetterY() && System.Globalization.CultureInfo.CurrentCulture.IsVowelOf(firstCharVowel))
          code = firstCharVowel + code;

        if (MaxCodeLength < code.Length)
          code = code[..MaxCodeLength];
      }

      return code;
    }

    /// <summary>Ensure valid characters for nysiis code generation.</summary>
    public static string GetValidCharacters(string text)
    {
      var sb = text.RemoveUnicodeMarks();
      sb.ReplaceUnicodeLatinStrokes();
      sb.RemoveAll(char.IsLetter);
      return sb.AsSpan().ToUpperCase(System.Globalization.CultureInfo.CurrentCulture).ToString();
    }
  }
}
