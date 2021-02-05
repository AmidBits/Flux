using System.Linq;

namespace Flux.Text.PhoneticAlgorithm
{
  public class EnhancedNysiis
    : IPhoneticAlgorithmEncoder
  {
    public int MaxCodeLength { get; set; } = 6;

    /// <summary>Nysiis is similar in nature to the SOUNDEX phonetic encoder, but does of course produce different results. New York State Identification and Intelligence System (NYSIIS) Phonetic Encoder.</summary>
    /// <see cref="https://xlinux.nist.gov/dads/HTML/nysiis.html"/>
    /// <summary>Returns a NYSIIS phonetically coded string.</summary>
    public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> expression)
    {
      var name = string.Concat(GetValidCharacters(expression.ToString()));

      name = System.Text.RegularExpressions.Regex.Replace(name, @"^MAC", @"MCC");
      name = System.Text.RegularExpressions.Regex.Replace(name, @"^KN", @"NN");
      name = System.Text.RegularExpressions.Regex.Replace(name, @"^K", @"C");
      name = System.Text.RegularExpressions.Regex.Replace(name, @"^PH", @"FF");
      name = System.Text.RegularExpressions.Regex.Replace(name, @"^SCH", @"SSS");

      name = System.Text.RegularExpressions.Regex.Replace(name, @"EE$", @"Y ");
      name = System.Text.RegularExpressions.Regex.Replace(name, @"IE$", @"Y ");
      name = System.Text.RegularExpressions.Regex.Replace(name, @"(DT|RT|RD|NT|ND)$", @"D ");

      var code1 = new System.Text.StringBuilder(name[0]);

      var index = 1;


      while (index < name.Length)
      {
        var currentLetter = name[index];

        if (currentLetter != ' ') // 5.1 inverse.
        {
          //if(Flux.Globalization.EnUs.Language.IsEnglishVowel(currentLetter, false))
          //{ currentLetter = 'E' && name[index+1]=='V'}
        }

        index++;
      }


      // 7. If the last character of the NYSIIS code is the letter 'S' then remove it.
      if (code1.Length > 0 && code1[code1.Length - 1] == 'S')
        code1.Remove(code1.Length - 1, 1);

      // 8. If the last two characters of the NYSIIS code are the letters 'AY' then replace them with the single character 'Y'.
      if (code1.EndsWith(@"AY"))
        code1.Remove(code1.Length - 2, 1);

      // 9. If the last character of the NYSIIS code is the letter 'A' then remove this letter.
      if (code1.Length > 0 && code1[code1.Length - 1] == 'A')
        code1.Remove(code1.Length - 1, 1);

      var code = string.Concat(GetValidCharacters(expression.ToString()));

      if (code.Length > 0)
      {


        if (code.Length > MaxCodeLength)
          code = code.Substring(0, MaxCodeLength);
      }

      return code;
    }

    /// <summary>Ensure valid characters for nysiis code generation.</summary>
    public static string GetValidCharacters(string text)
      => string.Concat(text.RemoveDiacriticalMarks(c => SystemCharEm.RemoveDiacriticalLatinStroke(c)).Where(c => Flux.Globalization.EnUs.Language.IsEnglishLetter(c)).Select(c => char.ToUpper(c, System.Globalization.CultureInfo.CurrentCulture)));
  }
}
