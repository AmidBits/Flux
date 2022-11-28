namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the rune is a Swedish consonant.</summary>
    public static bool IsSwedishConsonant(this System.Text.Rune source)
      => IsSwedishConsonantLower(source) || IsSwedishConsonantUpper(source);
    /// <summary>Indicates whether the rune is a Swedish lower case consonant.</summary>
    public static bool IsSwedishConsonantLower(this System.Text.Rune source)
      => source.Value switch
      {
        'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' => true,
        _ => false,
      };
    /// <summary>Indicates whether the rune is a Swedish upper case consonant.</summary>
    public static bool IsSwedishConsonantUpper(this System.Text.Rune source)
      => source.Value switch
      {
        'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
        _ => false,
      };

    /// <summary>Indicates whether the rune is a Swedish letter.</summary>
    public static bool IsSwedishLetter(this System.Text.Rune source)
      => IsSwedishLetterLower(source) || IsSwedishLetterUpper(source);
    /// <summary>Indicates whether the rune is a Swedish lower case letter.</summary>
    public static bool IsSwedishLetterLower(this System.Text.Rune source)
      => source.Value is var value && (value >= 'a' && value <= 'z') || value == '\u00E5' || value == '\u00E4' || value == '\u00F6';
    /// <summary>Indicates whether the rune is a Swedish upper case letter.</summary>
    public static bool IsSwedishLetterUpper(this System.Text.Rune source)
      => source.Value is var value && (value >= 'A' && value <= 'Z') || value == '\u00C5' || value == '\u00C4' || value == '\u00D6';

    /// <summary>Indicates whether the rune is a Swedish vowel.</summary>
    public static bool IsSwedishVowel(this System.Text.Rune source)
      => IsSwedishVowelLower(source) || IsSwedishVowelUpper(source);
    /// <summary>Indicates whether the rune is a Swedish lower case vowel.</summary>
    public static bool IsSwedishVowelLower(this System.Text.Rune source)
      => source.Value switch
      {
        'a' or 'e' or 'i' or 'o' or 'u' or 'y' or '\u00E5' or '\u00E4' or '\u00F6' => true,
        _ => false,
      };
    /// <summary>Indicates whether the rune is a Swedish upper case vowel.</summary>
    public static bool IsSwedishVowelUpper(this System.Text.Rune source)
      => source.Value switch
      {
        'A' or 'E' or 'I' or 'O' or 'U' or 'Y' or '\u00C5' or '\u00C4' or '\u00D6' => true,
        _ => false,
      };

    #region System.Char version
    ///// <summary>Indicates whether the char is a Swedish consonant.</summary>
    //public static bool IsSwedishConsonant(this char source)
    //{
    //  switch (source)
    //  {
    //    case 'b':
    //    case 'c':
    //    case 'd':
    //    case 'f':
    //    case 'g':
    //    case 'h':
    //    case 'j':
    //    case 'k':
    //    case 'l':
    //    case 'm':
    //    case 'n':
    //    case 'p':
    //    case 'q':
    //    case 'r':
    //    case 's':
    //    case 't':
    //    case 'v':
    //    case 'w':
    //    case 'x':
    //    case 'z':
    //    case 'B':
    //    case 'C':
    //    case 'D':
    //    case 'F':
    //    case 'G':
    //    case 'H':
    //    case 'J':
    //    case 'K':
    //    case 'L':
    //    case 'M':
    //    case 'N':
    //    case 'P':
    //    case 'Q':
    //    case 'R':
    //    case 'S':
    //    case 'T':
    //    case 'V':
    //    case 'W':
    //    case 'X':
    //    case 'Z':
    //      return true;
    //    default:
    //      return false;
    //  }
    //}

    ///// <summary>Indicates whether the char is a Swedish letter.</summary>
    //public static bool IsSwedishLetter(this char source)
    //  => IsSwedishLetterLower(source) || IsSwedishLetterUpper(source);
    ///// <summary>Indicates whether the char is a Swedish lower case letter.</summary>
    //public static bool IsSwedishLetterLower(this char source)
    //  => (source >= 'a' && source <= 'z') || source == '\u00E5' || source == '\u00E4' || source == '\u00F6';
    ///// <summary>Indicates whether the char is a Swedish upper case letter.</summary>
    //public static bool IsSwedishLetterUpper(this char source)
    //  => (source >= 'A' && source <= 'Z') || source == '\u00C5' || source == '\u00C4' || source == '\u00D6';

    ///// <summary>Indicates whether the char is a Swedish vowel.</summary>
    //public static bool IsSwedishVowel(this char source)
    //{
    //  switch (source)
    //  {
    //    case 'a':
    //    case 'e':
    //    case 'i':
    //    case 'o':
    //    case 'u':
    //    case 'y':
    //    case '\u00E5': // 'å'
    //    case '\u00E4': // 'ä'
    //    case '\u00F6': // 'ö'
    //    case 'A':
    //    case 'E':
    //    case 'I':
    //    case 'O':
    //    case 'U':
    //    case 'Y':
    //    case '\u00C5': // 'Å'
    //    case '\u00C4': // 'Ä'
    //    case '\u00D6': // 'Ö'
    //      return true;
    //    default:
    //      return false;
    //  }
    //}
    #endregion System.Char version
  }
}
