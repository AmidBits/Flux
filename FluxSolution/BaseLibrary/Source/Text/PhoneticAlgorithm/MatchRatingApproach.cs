namespace Flux.Text.PhoneticAlgorithm
{
  /// <summary>The match rating approach (MRA) is a phonetic algorithm developed by Western Airlines in 1977 for the indexation and comparison of homophonous names.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Match_rating_approach"/>
  public class MatchRatingApproach
    : IPhoneticEncoder
  {
    public string Encode(System.ReadOnlySpan<char> expression)
    {
      var soundex = new System.Text.StringBuilder();

      for (var index = 0; index < expression.Length; index++)
      {
        var character = expression[index];

        if (index > 0)
        {
          if (Flux.Globalization.EnUs.Language.IsEnglishVowel(character, false)) continue;

          if (Flux.Globalization.EnUs.Language.IsEnglishVowel(character, true) && character == expression[index - 1]) continue;
        }

        soundex.Append(char.ToUpper(character, System.Globalization.CultureInfo.InvariantCulture));
      }

      if (soundex.Length > 6) return soundex.ToString(0, 3) + soundex.ToString(soundex.Length - 3, 3);

      return soundex.ToString();
    }
  }
}
