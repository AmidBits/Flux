//namespace Flux
//{
//  public record class LanguageEn
//    : ILanguageLetterable, ILanguageSpeakable
//  {
//    public static ILanguageLetterable Letter => new LanguageEn(true);
//    public static ILanguageSpeakable SpokenYconsonant => new LanguageEn(false);
//    public static ILanguageSpeakable SpokenYvowel => new LanguageEn(true);

//    private bool m_treatAsVowelY;

//    public LanguageEn(bool treatAsVowelY) => m_treatAsVowelY = treatAsVowelY;

//    public bool TreatAsVowelY { get => m_treatAsVowelY; init => m_treatAsVowelY = value; }

//    // ILanguageLettering
//    public bool IsLetter(char source) => IsLowerCase(source) || IsUpperCase(source);
//    public bool IsLowerCase(char source) => source >= 'a' && source <= 'z';
//    public bool IsUpperCase(char source) => source >= 'A' && source <= 'Z';
//    public bool IsLetter(System.Text.Rune source) => IsLetter((char)source.Value);
//    public bool IsLowerCase(System.Text.Rune source) => IsLowerCase((char)source.Value);
//    public bool IsUpperCase(System.Text.Rune source) => IsUpperCase((char)source.Value);

//    // ILanguageSpeakable
//    public bool IsConsonant(char source)
//      => source switch
//      {
//        'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' => true,
//        'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
//        'y' => !m_treatAsVowelY,
//        'Y' => !m_treatAsVowelY,
//        _ => false,
//      };
//    public bool IsConsonant(System.Text.Rune source) => IsConsonant((char)source.Value);
//    public bool IsVowel(char source)
//      => source switch
//      {
//        'a' or 'e' or 'i' or 'o' or 'u' => true,
//        'A' or 'E' or 'I' or 'O' or 'U' => true,
//        'y' => m_treatAsVowelY,
//        'Y' => m_treatAsVowelY,
//        _ => false,
//      };
//    public bool IsVowel(System.Text.Rune source) => IsVowel((char)source.Value);
//  }

//  //public record class LanguageEnLettering
//  //  : ILanguageLettering
//  //{
//  //  public static LanguageEnLettering Default = new();

//  //  // ILanguageLettering
//  //  public bool IsLanguageLetter(char source) => (source >= 'A' && source <= 'Z') || (source >= 'a' && source <= 'z');
//  //  public bool IsLanguageLetter(System.Text.Rune source) => IsLanguageLetter((char)source.Value);
//  //}

//  //public record class LanguageEnSpoken
//  //  : ILanguageSpeakable
//  //{
//  //  public static LanguageEnSpoken PresetConsonantY => new(false);
//  //  public static LanguageEnSpoken PresetVowelY => new(true);

//  //  private bool m_treatAsVowelY;

//  //  public LanguageEnSpoken(bool treatAsVowelY) => m_treatAsVowelY = treatAsVowelY;

//  //  public bool TreatAsVowelY { get => m_treatAsVowelY; init => m_treatAsVowelY = value; }

//  //  // ILanguageSpeakable
//  //  public bool IsConsonant(char source)
//  //    => source switch
//  //    {
//  //      'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' => true,
//  //      'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
//  //      'y' => !m_treatAsVowelY,
//  //      'Y' => !m_treatAsVowelY,
//  //      _ => false,
//  //    };
//  //  public bool IsConsonant(System.Text.Rune source) => IsConsonant((char)source.Value);
//  //  public bool IsVowel(char source)
//  //    => source switch
//  //    {
//  //      'a' or 'e' or 'i' or 'o' or 'u' => true,
//  //      'A' or 'E' or 'I' or 'O' or 'U' => true,
//  //      'y' => m_treatAsVowelY,
//  //      'Y' => m_treatAsVowelY,
//  //      _ => false,
//  //    };
//  //  public bool IsVowel(System.Text.Rune source) => IsVowel((char)source.Value);
//  //}
//}
