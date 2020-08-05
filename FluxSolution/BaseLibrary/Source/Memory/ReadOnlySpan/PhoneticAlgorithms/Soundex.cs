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

      const string Vowels = @"AIOUY";

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
}
