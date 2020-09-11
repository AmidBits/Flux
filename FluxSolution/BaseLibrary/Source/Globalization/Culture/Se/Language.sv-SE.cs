namespace Flux.Globalization.Se
{
  public static partial class Language
  {
    /// <summary>Indicates whether the char is a Swedish consonant.</summary>
    public static bool IsSwedishConsonant(this char source)
    {
      switch (source)
      {
        case 'b':
        case 'c':
        case 'd':
        case 'f':
        case 'g':
        case 'h':
        case 'j':
        case 'k':
        case 'l':
        case 'm':
        case 'n':
        case 'p':
        case 'q':
        case 'r':
        case 's':
        case 't':
        case 'v':
        case 'w':
        case 'x':
        case 'z':
        case 'B':
        case 'C':
        case 'D':
        case 'F':
        case 'G':
        case 'H':
        case 'J':
        case 'K':
        case 'L':
        case 'M':
        case 'N':
        case 'P':
        case 'Q':
        case 'R':
        case 'S':
        case 'T':
        case 'V':
        case 'W':
        case 'X':
        case 'Z':
          return true;
        default:
          return false;
      }
    }

    /// <summary>Indicates whether the char is a Swedish letter.</summary>
    public static bool IsSwedishLetter(this char source)
      => IsSwedishLetterLower(source) || IsSwedishLetterUpper(source);
    /// <summary>Indicates whether the char is a Swedish lower case letter.</summary>
    public static bool IsSwedishLetterLower(this char source)
      => (source >= 'a' && source <= 'z') || source == '\u00E5' || source == '\u00E4' || source == '\u00F6';
    /// <summary>Indicates whether the char is a Swedish upper case letter.</summary>
    public static bool IsSwedishLetterUpper(this char source)
      => (source >= 'A' && source <= 'Z') || source == '\u00C5' || source == '\u00C4' || source == '\u00D6';

    /// <summary>Indicates whether the char is a Swedish vowel.</summary>
    public static bool IsSwedishVowel(this char source)
    {
      switch (source)
      {
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
        case 'y':
        case '\u00E5': // '�'
        case '\u00E4': // '�'
        case '\u00F6': // '�'
        case 'A':
        case 'E':
        case 'I':
        case 'O':
        case 'U':
        case 'Y':
        case '\u00C5': // '�'
        case '\u00C4': // '�'
        case '\u00D6': // '�'
          return true;
        default:
          return false;
      }
    }
  }
}
