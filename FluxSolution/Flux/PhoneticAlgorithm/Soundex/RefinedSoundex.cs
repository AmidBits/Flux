namespace Flux.PhoneticAlgorithm.Soundex
{
  /// <summary>Refined soundex is a phonetic algorithm for indexing names by sound, as pronounced in English. It is an 'refined' version of the basic soundex.</summary>
  /// <returns>Returns a variable length refined soundex code.</returns>
  /// <see cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/> 
  public sealed class RefinedSoundex
    : IPhoneticAlgorithmEncodable
  {
    public const string LetterCodeMap = @"01360240043788015936020505";

    public int MaxCodeLength { get; set; } = 10;

    public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> name)
    {
      var refinedSoundex = new SpanMaker<char>(20);

      var previousCode = '\0';

      for (var index = 0; index < name.Length; index++)
        if (char.ToUpper(name[index]) is var c && c >= 'A' && c <= 'Z')
        {
          if (refinedSoundex.Length == 0)
            refinedSoundex = refinedSoundex.Append(c);

          var letterCode = LetterCodeMap[c - 'A'];

          if (letterCode != previousCode)
          {
            refinedSoundex = refinedSoundex.Append(letterCode);

            previousCode = letterCode;
          }
        }

      return refinedSoundex.AsReadOnlySpan()[..int.Min(MaxCodeLength, refinedSoundex.Length)].ToString();
    }
  }
}
