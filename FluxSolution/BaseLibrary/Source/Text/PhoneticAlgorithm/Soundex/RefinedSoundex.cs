namespace Flux
{
  public static partial class TextPhoneticAlgorithmEm
  {
    /// <summary>Refined soundex is a phonetic algorithm for indexing names by sound, as pronounced in English. It is an 'refined' version of the basic soundex.</summary>
    /// <returns>Returns a variable length refined soundex code.</returns>
    /// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/> 
    public static string EncodeRefinedSoundex(this System.ReadOnlySpan<char> source)
      => new Text.PhoneticAlgorithm.RefinedSoundex().EncodePhonetic(source);
  }

  namespace Text.PhoneticAlgorithm
  {
    /// <summary>Refined soundex is a phonetic algorithm for indexing names by sound, as pronounced in English. It is an 'refined' version of the basic soundex.</summary>
    /// <returns>Returns a variable length refined soundex code.</returns>
    /// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/> 
    public class RefinedSoundex
      : IPhoneticEncoder
    {
      public const string LetterCodeMap = @"01360240043788015936020505";

      public int CodeLength { get; set; } = 10;

      public string EncodePhonetic(System.ReadOnlySpan<char> name)
      {
        var refinedSoundex = new System.Text.StringBuilder(20);

        var previousCode = '\0';

        for (var index = 0; index < name.Length; index++)
        {
          if (char.ToUpper(name[index], System.Globalization.CultureInfo.CurrentCulture) is var letter && letter >= 'A' && letter <= 'Z')
          {
            if (refinedSoundex.Length == 0) refinedSoundex.Append(letter);

            var letterCode = LetterCodeMap[letter - 'A'];

            if (letterCode != previousCode)
            {
              refinedSoundex.Append(letterCode);

              previousCode = letterCode;
            }
          }
        }

        return refinedSoundex.ToString(0, System.Math.Min(CodeLength, refinedSoundex.Length));
      }
    }
  }
}
