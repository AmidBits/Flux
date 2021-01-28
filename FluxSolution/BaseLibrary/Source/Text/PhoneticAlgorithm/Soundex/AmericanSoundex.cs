namespace Flux
{
  public static partial class TextPhoneticAlgorithmEm
  {
    /// <summary>Soundex is a phonetic algorithm for indexing names by sound, as pronounced in English.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Soundex"/>
    /// <seealso cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/>
    public static string EncodeSoundex(this System.ReadOnlySpan<char> source)
      => new Text.PhoneticAlgorithm.AmericanSoundex().EncodePhoneticAlgorithm(source);
  }

  namespace Text.PhoneticAlgorithm
  {
    /// <summary>Soundex is a phonetic algorithm for indexing names by sound, as pronounced in English.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Soundex"/>
    /// <seealso cref="http://ntz-develop.blogspot.com/2011/03/phonetic-algorithms.html"/>
    public class AmericanSoundex
      : IPhoneticAlgorithmEncoder
    {
      public const string LetterCodeMap = @"01230120022455012623010202";

      public int MaxCodeLength { get; set; } = 4;

      public string EncodePhoneticAlgorithm(System.ReadOnlySpan<char> name)
      {
        var soundex = new System.Text.StringBuilder();

        var lastUsedCode = '\0';
        var wasLastLetterHW = false;

        for (var index = 0; index < name.Length; index++)
        {
          if (char.ToUpper(name[index], System.Globalization.CultureInfo.CurrentCulture) is var letter && letter >= 'A' && letter <= 'Z')
          {
            var code = LetterCodeMap[letter - 'A'];

            if (soundex.Length == 0)
            {
              soundex.Append(letter);
              lastUsedCode = code;
            }
            else if (code != lastUsedCode && !wasLastLetterHW)
            {
              if (code != '0')
                soundex.Append(code);
              lastUsedCode = code;
            }

            wasLastLetterHW = letter == 'H' || letter == 'W';
          }
        }

        if (soundex.Length < MaxCodeLength)
          return soundex.Append('0', MaxCodeLength - soundex.Length).ToString();
        if (soundex.Length > MaxCodeLength)
          return soundex.ToString(0, MaxCodeLength);

        return soundex.ToString();
      }
    }
  }
}
