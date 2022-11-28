namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the rune is an English consonant, where y|Y is optional.</summary>
    public static bool IsEnglishConsonant(this System.Text.Rune source, bool includeY)
      => IsEnglishConsonantLower(source, includeY) || IsEnglishConsonantUpper(source, includeY);

    /// <summary>Indicates whether the rune is an English consonant, where y|Y is optional.</summary>
    public static bool IsEnglishConsonantLower(this System.Text.Rune source, bool includeY)
      => IsEnglishLetterLower(source) && !IsEnglishVowelLower(source, !includeY);

    /// <summary>Indicates whether the rune is an English consonant, where y|Y is optional.</summary>
    public static bool IsEnglishConsonantUpper(this System.Text.Rune source, bool includeY)
      => IsEnglishLetterUpper(source) && !IsEnglishVowelUpper(source, !includeY);

    /// <summary>Indicates whether the rune is an English letter.</summary>
    public static bool IsEnglishLetter(this System.Text.Rune source)
      => IsEnglishLetterLower(source) || IsEnglishLetterUpper(source);

    /// <summary>Indicates whether the rune is an English lower case letter.</summary>
    public static bool IsEnglishLetterLower(this System.Text.Rune source)
      => source.Value >= 'a' && source.Value <= 'z';

    /// <summary>Indicates whether the rune is an English upper case letter.</summary>
    public static bool IsEnglishLetterUpper(this System.Text.Rune source)
      => source.Value >= 'A' && source.Value <= 'Z';

    /// <summary>Indicates whether the rune is an English vowel, where y|Y is optional.</summary>
    public static bool IsEnglishVowel(this System.Text.Rune source, bool includeY)
      => IsEnglishVowelLower(source, includeY) || IsEnglishVowelUpper(source, includeY);

    /// <summary>Indicates whether the rune is an English lower case vowel, where y is optional.</summary>
    public static bool IsEnglishVowelLower(this System.Text.Rune source, bool includeY)
      => source.Value switch
      {
        'a' or 'e' or 'i' or 'o' or 'u' => true,
        'y' => includeY,
        _ => false,
      };

    /// <summary>Indicates whether the rune is an English upper case vowel, where Y is optional.</summary>
    public static bool IsEnglishVowelUpper(this System.Text.Rune source, bool includeY)
      => source.Value switch
      {
        'A' or 'E' or 'I' or 'O' or 'U' => true,
        'Y' => includeY,
        _ => false,
      };

    #region System.Char version
    //  /// <summary>Indicates whether the char is an English consonant, where y|Y is optional.</summary>
    //public static bool IsEnglishConsonant(this char source, bool includeY)
    //  {
    //    switch (source)
    //    {
    //      case 'b':
    //      case 'c':
    //      case 'd':
    //      case 'f':
    //      case 'g':
    //      case 'h':
    //      case 'j':
    //      case 'k':
    //      case 'l':
    //      case 'm':
    //      case 'n':
    //      case 'p':
    //      case 'q':
    //      case 'r':
    //      case 's':
    //      case 't':
    //      case 'v':
    //      case 'w':
    //      case 'x':
    //      case 'z':
    //      case 'B':
    //      case 'C':
    //      case 'D':
    //      case 'F':
    //      case 'G':
    //      case 'H':
    //      case 'J':
    //      case 'K':
    //      case 'L':
    //      case 'M':
    //      case 'N':
    //      case 'P':
    //      case 'Q':
    //      case 'R':
    //      case 'S':
    //      case 'T':
    //      case 'V':
    //      case 'W':
    //      case 'X':
    //      case 'Z':
    //        return true;
    //      case 'y':
    //      case 'Y':
    //        return includeY;
    //      default:
    //        return false;
    //    }
    //  }

    //  /// <summary>Indicates whether the char is an English letter.</summary>
    //  public static bool IsEnglishLetter(this char source)
    //    => IsEnglishLowerCaseLetter(source) || IsEnglishUpperCaseLetter(source);
    //  /// <summary>Indicates whether the char is an English lower case letter.</summary>
    //  public static bool IsEnglishLowerCaseLetter(this char source)
    //    => source >= 'a' && source <= 'z';
    //  /// <summary>Indicates whether the char is an English upper case letter.</summary>
    //  public static bool IsEnglishUpperCaseLetter(this char source)
    //    => source >= 'A' && source <= 'Z';

    //  /// <summary>Indicates whether the char is an English vowel, where y|Y is optional.</summary>
    //  public static bool IsEnglishVowel(this char source, bool includeY)
    //  {
    //    switch (source)
    //    {
    //      case 'a':
    //      case 'e':
    //      case 'i':
    //      case 'o':
    //      case 'u':
    //      case 'A':
    //      case 'E':
    //      case 'I':
    //      case 'O':
    //      case 'U':
    //        return true;
    //      case 'y':
    //      case 'Y':
    //        return includeY;
    //      default:
    //        return false;
    //    }
    //  }
    #endregion System.Char version
  }
}
