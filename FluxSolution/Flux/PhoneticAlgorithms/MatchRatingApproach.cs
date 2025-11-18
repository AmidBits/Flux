namespace Flux.PhoneticAlgorithm
{
  /// <summary>The match rating approach (MRA) is a phonetic algorithm developed by Western Airlines in 1977 for the indexation and comparison of homophonous names.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Match_rating_approach"/>
  public sealed class MatchRatingApproach
    : IPhoneticallyEncodable
  {
    public string EncodePhonetic(System.ReadOnlySpan<char> name)
    {
      var sb = new System.Text.StringBuilder();

      var index = -1;
      var previousRune = '\0';

      foreach (var currentRune in name)
      {
        index++;

        if (index > 0)
          if (char.IsVowel(currentRune) && (!char.IsBasicLatinLetterY(currentRune) || currentRune == previousRune))
            continue;

        sb.Append(char.ToUpper(currentRune, System.Globalization.CultureInfo.InvariantCulture));

        previousRune = currentRune;
      }

      if (sb.Length > 6)
        sb.Remove(3, sb.Length - 6);

      return sb.ToString();
    }

    /// <summary>Compare two MRA encoded names.</summary>
    /// <param name="code1">The first encoded name.</param>
    /// <param name="code2">The second encoded name.</param>
    /// <param name="minimumRating">Output variable returning the minimum rating.</param>
    /// <param name="similarityRating">Output variable returning the similarity rating.</param>
    /// <returns>Whether the name comparison is considered successful.</returns>
    public static bool TryCompareEncoded(System.ReadOnlySpan<char> code1, System.ReadOnlySpan<char> code2, out int minimumRating, out int similarityRating)
    {
      minimumRating = ComputeMinimumRating(code1.Length, code2.Length);
      similarityRating = ComputeSimilarityRating(code1, code2);

      return similarityRating >= minimumRating;
    }

    /// <summary>Returns the minimum rating which is based on the code lengths from two encoded names.</summary>
    /// <param name="codeLength1">The first encoded length.</param>
    /// <param name="codeLength2">The second encoded length.</param>
    public static int ComputeMinimumRating(int codeLength1, int codeLength2)
    {
      if (int.Abs(codeLength1 - codeLength2) >= 3)
        return 0;

      return (codeLength1 + codeLength2) switch
      {
        var le4 when le4 <= 4 => 5,
        var le7 when le7 <= 7 => 4,
        var le11 when le11 <= 11 => 3,
        var e12 when e12 == 12 => 2,
        _ => 0,
      };
      ;
    }
    /// <summary>Returns the similarity rating for two encoded names.</summary>
    /// <param name="code1">The first encoded name.</param>
    /// <param name="code2">The second encoded name.</param>
    /// <returns></returns>
    public static int ComputeSimilarityRating(System.ReadOnlySpan<char> code1, System.ReadOnlySpan<char> code2)
    {
      var large = code1.ToStringBuilder();
      var small = code2.ToStringBuilder();

      if (large.Length < small.Length)
      {
        var tmp = large;
        large = small;
        small = tmp;
      }

      for (var i = 0; i < small.Length;)
      {
        bool found = false;

        for (var j = 0; j < large.Length; j++)
          if (small[i] == large[j])
          {
            small = small.Remove(i, 1);
            large = large.Remove(j, 1);
            found = true;
          }

        if (!found)
          i++;
      }

      for (var i = small.Length - 1; i >= 0;)
      {
        var found = false;

        for (var j = large.Length - 1; j >= 0; j--)
        {
          if (small[i] == large[j])
          {
            small = small.Remove(i, 1);
            large = large.Remove(j, 1);
            found = true;
          }
        }

        if (!found)
          i--;
      }

      return 6 - large.Length;
    }
  }
}
