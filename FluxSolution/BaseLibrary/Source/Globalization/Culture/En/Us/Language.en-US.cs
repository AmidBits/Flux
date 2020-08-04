namespace Flux.Globalization.EnUs
{
  public static partial class Language
  {
    /// <summary>Indicates whether the char is an English consonant, where y|Y is optional.</summary>
    public static bool IsEnglishConsonant(this char source, bool includeY)
    {
      switch (source)
      {
        case 'b':
        case 'B':
        case 'c':
        case 'C':
        case 'd':
        case 'D':
        case 'f':
        case 'F':
        case 'g':
        case 'G':
        case 'h':
        case 'H':
        case 'j':
        case 'J':
        case 'k':
        case 'K':
        case 'l':
        case 'L':
        case 'm':
        case 'M':
        case 'n':
        case 'N':
        case 'p':
        case 'P':
        case 'q':
        case 'Q':
        case 'r':
        case 'R':
        case 's':
        case 'S':
        case 't':
        case 'T':
        case 'v':
        case 'V':
        case 'w':
        case 'W':
        case 'x':
        case 'X':
        case 'z':
        case 'Z':
          return true;
        case 'y':
        case 'Y':
          return includeY;
        default:
          return false;
      }
    }

    /// <summary>Indicates whether the char is an English letter.</summary>
    public static bool IsEnglishLetter(this char source)
      => Globalization.EnUs.Language.IsEnglishLetterLower(source) || Globalization.EnUs.Language.IsEnglishLetterUpper(source);
    /// <summary>Indicates whether the char is an English lower case letter.</summary>
    public static bool IsEnglishLetterLower(this char source)
      => source >= 'a' && source <= 'z';
    /// <summary>Indicates whether the char is an English upper case letter.</summary>
    public static bool IsEnglishLetterUpper(this char source)
      => source >= 'A' && source <= 'Z';

    /// <summary>Indicates whether the char is an English vowel, where y|Y is optional.</summary>
    public static bool IsEnglishVowel(this char source, bool includeY)
    {
      switch (source)
      {
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
        case 'A':
        case 'E':
        case 'I':
        case 'O':
        case 'U':
          return true;
        case 'y':
        case 'Y':
          return includeY;
        default:
          return false;
      }
    }
  }
}
