namespace Flux
{
  public static partial class GlobalizationEnUsLanguage
  {   /// <summary>Indicates whether the char is an English consonant, where y|Y is optional.</summary>
		public static bool IsEnglishConsonant(this char source, bool includeY)
      => source switch
      {
        'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' or 'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
        'y' or 'Y' => includeY,
        _ => false,
      };

    /// <summary>Indicates whether the char is an English letter.</summary>
    public static bool IsEnglishLetter(this char source)
      => IsEnglishLowerCaseLetter(source) || IsEnglishUpperCaseLetter(source);
    /// <summary>Indicates whether the char is an English lower case letter.</summary>
    public static bool IsEnglishLowerCaseLetter(this char source)
      => source >= 'a' && source <= 'z';
    /// <summary>Indicates whether the char is an English upper case letter.</summary>
    public static bool IsEnglishUpperCaseLetter(this char source)
      => source >= 'A' && source <= 'Z';

    /// <summary>Indicates whether the char is an English vowel, where y|Y is optional.</summary>
    public static bool IsEnglishVowel(this char source, bool includeY)
      => source switch
      {
        'a' or 'e' or 'i' or 'o' or 'u' or 'A' or 'E' or 'I' or 'O' or 'U' => true,
        'y' or 'Y' => includeY,
        _ => false,
      };

    #region System.Text.rune version
    /// <summary>Indicates whether the rune is an English consonant, where y|Y is optional.</summary>
    public static bool IsEnglishConsonant(this System.Text.Rune source, bool includeY)
      => source.Value switch
      {
        'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' or 'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
        'y' or 'Y' => includeY,
        _ => false,
      };

    /// <summary>Indicates whether the rune is an English letter.</summary>
    public static bool IsEnglishLetter(this System.Text.Rune source)
      => IsEnglishLowerCaseLetter(source) || IsEnglishUpperCaseLetter(source);
    /// <summary>Indicates whether the rune is an English lower case letter.</summary>
    public static bool IsEnglishLowerCaseLetter(this System.Text.Rune source)
      => source.Value >= 'a' && source.Value <= 'z';
    /// <summary>Indicates whether the rune is an English upper case letter.</summary>
    public static bool IsEnglishUpperCaseLetter(this System.Text.Rune source)
      => source.Value >= 'A' && source.Value <= 'Z';

    /// <summary>Indicates whether the rune is an English vowel, where y|Y is optional.</summary>
    public static bool IsEnglishVowel(this System.Text.Rune source, bool includeY)
      => source.Value switch
      {
        'a' or 'e' or 'i' or 'o' or 'u' or 'A' or 'E' or 'I' or 'O' or 'U' => true,
        'y' or 'Y' => includeY,
        _ => false,
      };
    #endregion System.Text.rune version
  }
}
