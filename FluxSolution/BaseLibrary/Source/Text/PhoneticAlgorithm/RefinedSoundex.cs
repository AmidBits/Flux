namespace Flux.Text.PhoneticAlgorithm
{
  /// <summary>Encodes the text using an implementation of the refined soundex.</summary>
  /// <returns>Returns a variable length refined soundex code.</returns>
  /// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/> 
  public class RefinedSoundex
    : IPhoneticEncoder
  {
    public const string LetterCodeMap = @"01360240043788015936020505";

    public string Encode(string name)
    {
      var refinedSoundex = new System.Text.StringBuilder(20);

      var previousCode = '\0';

      for (var index = 0; index < name.Length; index++)
      {
        if (char.ToUpper(name[index]) is var letter && letter >= 'A' && letter <= 'Z')
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

      return refinedSoundex.ToString();
    }
  }
}
