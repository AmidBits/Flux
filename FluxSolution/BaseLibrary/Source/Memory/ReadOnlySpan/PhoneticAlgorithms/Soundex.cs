using System;
using System.Linq;

namespace Flux
{
  public partial class XtensionsReadOnlySpan
  {
    /// <summary>Returns a score in the range [0, 4] symbolizing a difference of the two specified soundex codes. The larger the difference score the larger the difference.</summary>
    /// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/>
    public static int SoundexDifference(this System.ReadOnlySpan<char> encodedSoundex1, System.ReadOnlySpan<char> encodedSoundex2)
    {
      if (encodedSoundex1 == encodedSoundex2) return 4;

      if (encodedSoundex1.Slice(1, 3) == encodedSoundex2.Slice(1, 3)) return 3;

      var result = encodedSoundex1[0] == encodedSoundex2[0] ? 1 : 0;

      if (encodedSoundex2.IndexOf(encodedSoundex1.Slice(2, 2)) > -1) return 2 + result;
      else if (encodedSoundex2.IndexOf(encodedSoundex1.Slice(1, 2)) > -1) return 2 + result;

      if (encodedSoundex2[1] == encodedSoundex1[1]) result++;
      if (encodedSoundex2[2] == encodedSoundex1[2]) result++;
      if (encodedSoundex2[3] == encodedSoundex1[3]) result++;

      return (result == 0) ? 1 : result;
    }

    /// <summary>Encodes the specified text using the original soundex.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Soundex"/>
    /// <seealso cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/>
    /// <returns>A soundex code consisting of a letter and three digits, though this implementation allows overriding the max length of the code.</returns>
    public static System.ReadOnlySpan<char> SoundexEncode(this System.ReadOnlySpan<char> word, int codeLength = 4)
    {
      const string LetterCodes = @"01230120022455012623010202";

      const string Vowels = @"AEIOUY";

      var soundex = new System.Text.StringBuilder();

      var previousCode = ' ';
      var previousLetter = ' ';

      foreach (var letter in word)
      {
        if (letter < 'A' || letter > 'Z') continue;

        var letterCode = LetterCodes[letter - 'A'];

        if (soundex.Length == 0)
        {
          soundex.Append(letter);
          previousCode = letterCode;
        }
        else if ((letterCode != previousCode || Vowels.Any(c => c == previousLetter)) && letterCode != '0')
        {
          soundex.Append(letterCode);
          previousCode = letterCode;
        }

        if (soundex.Length == codeLength)
          return soundex.ToString();

        previousLetter = letter;
      }

      if (soundex.Length < codeLength)
        soundex.Append(new string('0', codeLength - soundex.Length));

      return soundex.ToString();
    }

    /// <summary>Ensure valid characters for soundex code generation.</summary>
    //public static System.Collections.Generic.IEnumerable<char> GetValidCharacters(string text)
    //{
    //  foreach (var c in text.Where(c => char.IsLetter(c)))
    //  {
    //    if (Flux.Globalization.EnUs.Language.IsEnglishLetterUpper(c))
    //    {
    //      yield return c;
    //    }
    //    else if (Flux.Globalization.EnUs.Language.IsEnglishLetterLower(c))
    //    {
    //      yield return char.ToUpper(c, System.Globalization.CultureInfo.CurrentCulture);
    //    }
    //    else
    //    {
    //      foreach (var cdc in c.ToString(System.Globalization.CultureInfo.CurrentCulture).RemoveDiacriticalMarks(adr => adr.RemoveDiacriticalStroke()))
    //      {
    //        if (Flux.Globalization.EnUs.Language.IsEnglishLetterUpper(cdc))
    //        {
    //          yield return cdc;
    //        }
    //        else if (Flux.Globalization.EnUs.Language.IsEnglishLetterLower(cdc))
    //        {
    //          yield return char.ToUpper(cdc, System.Globalization.CultureInfo.CurrentCulture);
    //        }
    //      }
    //    }
    //  }
    //}
  }

  public class Soundex
  {
    public const string LetterCodes = @"01230120022455012623010202";

    public static void Clean(string characters, out System.Text.StringBuilder letters, out System.Text.StringBuilder codes)
    {
      letters = new System.Text.StringBuilder(characters.Length);
      codes = new System.Text.StringBuilder(characters.Length);

      foreach (var character in characters)
      {

      }
    }

    public static bool CharacterLetter(char character, out char letter)
      => (letter = char.ToUpper(character)) is var l && l >= 'A' && l <= 'Z';

    public static bool CharacterCode(char character, out char code)
    {
      return CharacterCode(character, out code);

      static bool CharacterCode(char character, out char code)
      {
        switch (character)
        {
          case 'b':
          case 'f':
          case 'p':
          case 'v':
            code = '1';
            return true;
          case 'c':
          case 'g':
          case 'j':
          case 'k':
          case 'q':
          case 's':
          case 'x':
          case 'z':
            code = '2';
            return true;
          case 'd':
          case 't':
            code = '3';
            return true;
          case 'l':
            code = '4';
            return true;
          case 'm':
          case 'n':
            code = '5';
            return true;
          case 'r':
            code = '6';
            return true;
          case 'a':
          case 'e':
          case 'i':
          case 'o':
          case 'u':
          case 'y':
            code = '+';
            return true;
          case 'h':
          case 'w':
            code = '-';
            return true;
          default:
            code = ' ';
            return false;
        }
      }
    }

    public static bool Is0(char character)
      => char.ToLower(character) switch
      {
        'a' => true,
        'e' => true,
        'i' => true,
        'o' => true,
        'u' => true,
        'y' => true,
        'h' => true,
        'w' => true,
        _ => false
      };
    public static bool Is1(char character)
      => char.ToLower(character) switch
      {
        'b' => true,
        'f' => true,
        'p' => true,
        'v' => true,
        _ => false;
      };
    public static bool Is2(char character)
      => char.ToLower(character) switch
      {
        'c' => true,
        'g' => true,
        'j' => true,
        'k' => true,
        'q' => true,
        's' => true,
        'x' => true,
        'z' => true,
        _ => false;
      };
    public static bool Is3(char character)
      => char.ToLower(character) switch
      {
        'd' => true,
        't' => true,
        _ => false;
      };
    public static bool Is4(char character)
      => char.ToLower(character) == 'l';
    public static bool Is5(char character)
      => char.ToLower(character) switch
      {
        'm' => true,
        'n' => true,
        _ => false
      };
    public static bool Is6(char character)
      => char.ToLower(character) == 'r';
    public static bool IsHW(char character)
      => char.ToLower(character) switch
      {
        'h' => true,
        'w' => true,
        _ => false
      };
    public static bool IsVowel(char character)
      => char.ToLower(character) switch
      {
        'a' => true,
        'e' => true,
        'i' => true,
        'o' => true,
        'u' => true,
        'y' => true,
        _ => false
      };

    public static TResult TransformCharacter<TResult>(char character, System.Func<char, TResult> selector)
    {
      switch (character)
      {
        case 'b':
        case 'f':
        case 'p':
        case 'v':
          return '1';
        case 'c':
        case 'g':
        case 'j':
        case 'k':
        case 'q':
        case 's':
        case 'x':
        case 'z':
          return '2';
        case 'd':
        case 't':
          return '3';
        case 'l':
          return '4';
        case 'm':
        case 'n':
          return '5';
        case 'r':
          return '6';
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
        case 'y':
          return '+';
        case 'h':
        case 'w':
          return '-';
        default:
          return ' ';
      }
    }
  }
}
