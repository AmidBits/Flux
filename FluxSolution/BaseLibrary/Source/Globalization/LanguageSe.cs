//namespace Flux
//{
//  public record class LanguageSe
//    : ILanguageLetterable, ILanguageSpeakable
//  {
//    public static LanguageSe Default = new();

//    // ILanguageLettering
//    public bool IsLetter(char source) => IsLowerCase(source) || IsUpperCase(source);
//    public bool IsLowerCase(char source) => (source >= 'a' && source <= 'z') || source == '\u00E5' || source == '\u00E4' || source == '\u00F6';
//    public bool IsUpperCase(char source) => (source >= 'A' && source <= 'Z') || source == '\u00C5' || source == '\u00C4' || source == '\u00D6';
//    public bool IsLetter(System.Text.Rune source) => IsLetter((char)source.Value);
//    public bool IsLowerCase(System.Text.Rune source) => IsLowerCase((char)source.Value);
//    public bool IsUpperCase(System.Text.Rune source) => IsUpperCase((char)source.Value);

//    // ILanguageSpeakable
//    public bool IsConsonant(char source)
//      => source switch
//      {
//        'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' or 'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
//        _ => false,
//      };
//    public bool IsConsonant(System.Text.Rune source) => IsConsonant((char)source.Value);
//    public bool IsVowel(char source)
//      => source switch
//      {
//        'a' or 'e' or 'i' or 'o' or 'u' or 'y' or '\u00E5' or '\u00E4' or '\u00F6' or 'A' or 'E' or 'I' or 'O' or 'U' or 'Y' or '\u00C5' or '\u00C4' or '\u00D6' => true,
//        _ => false,
//      };
//    public bool IsVowel(System.Text.Rune source) => IsVowel((char)source.Value);
//  }
//}
