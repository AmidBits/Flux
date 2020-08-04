namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Encodes the text using an implementation of the refined soundex.</summary>
    /// <returns>Returns a variable length refined soundex code.</returns>
    /// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/> 
    public static System.ReadOnlySpan<char> RefinedSoundexEncode(this System.ReadOnlySpan<char> word, int maxCodeLength = 10)
    {
      const string LetterCodes = @"01360240043788015936020505";

      var refinedSoundex = new System.Text.StringBuilder(maxCodeLength);

      var previousCode = ' ';

      foreach (var letter in word)
      {
        if (letter < 'A' || letter > 'Z') continue;

        if (refinedSoundex.Length == 0) refinedSoundex.Append(letter);

        if (refinedSoundex.Length == maxCodeLength) return refinedSoundex.ToString();

        var letterCode = LetterCodes[letter - 'A'];

        if (letterCode != previousCode)
        {
          refinedSoundex.Append(letterCode);

          previousCode = letterCode;
        }
      }

      return refinedSoundex.ToString();
    }
  }
}
