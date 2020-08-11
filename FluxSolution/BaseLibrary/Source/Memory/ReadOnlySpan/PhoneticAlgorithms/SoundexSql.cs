using System;
using System.Linq;

namespace Flux
{
  public partial class XtensionsReadOnlySpan
  {
    /// <summary>Encodes the specified text using the original soundex.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Soundex"/>
    /// <seealso cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/>
    /// <returns>A soundex code consisting of a letter and three digits, though this implementation allows overriding the max length of the code.</returns>
    public static System.ReadOnlySpan<char> SoundexSqlEncode(this System.ReadOnlySpan<char> word, int codeLength = 4)
    {
      const string LetterCodes = @"01230120022455012623010202";

      const string Vowels = @"AIOUY";

      var soundex = new System.Text.StringBuilder();

      var previousDigit = '\0';

      foreach (var letter in word)
      {
        //var codePoint = (letter >= 'a' && letter <= 'z') ? letter - 'a' : (letter >= 'A' && letter <= 'Z') ? letter - 'A' : -1;

        //if (codePoint < 0 || codePoint > LetterCodes.Length) continue;

        //var digit = LetterCodes[codePoint];

        var digit = Flux.Soundextrous.ToSoundexCode(letter);

        if (digit == ' ') continue;

        if (soundex.Length == 0)
        {
          soundex.Append(letter);
        }
        else if (digit != '0' && digit != previousDigit)
        {
          soundex.Append(digit);
        }

        previousDigit = digit;
      }

      //if (soundex.Length < codeLength)
      //  soundex.Append(new string('0', codeLength - soundex.Length));
      //if (soundex.Length > codeLength)
      //  return soundex.ToString(0, codeLength);

      return soundex.ToString();
    }
    public static string SoundexSqlEncode(this string word, int codeLength = 4)
      => word.ToUpper(System.Globalization.CultureInfo.CurrentCulture).AsSpan().SoundexSqlEncode(codeLength).ToString();
  }
}
