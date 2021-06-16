namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Generates a random name of specified length based on simple statistical properties of the English language.</summary>
    public static string NextRandomNameEnUs(this System.Random source, int length)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sb = new System.Text.StringBuilder();

      var consonantRepeats = 0;
      var vowelRepeats = 0;

      while (sb.Length < (2 * length))
      {
        var chance = source.NextDouble();

        if (vowelRepeats >= 2 || (consonantRepeats < 2 && chance <= 0.6)) // Typical consonants to vowel ratio is about 3/2 (3/5?)).
        {
          consonantRepeats++;
          vowelRepeats = 0;

          var consonant = source.NextProbabilityRuneEnUsConsonant(chance <= 0.36).ToString();
          if (!sb.EndsWith(consonant) || "bdfglmnprst".Contains(consonant))
            sb.Append(consonant);
        }

        if (consonantRepeats >= 2 || (vowelRepeats < 2 && chance <= 0.4)) // Typical vowel to consonant ratio is about 2/3 (2/5?).
        {
          consonantRepeats = 0;
          vowelRepeats++;

          var vowel = source.NextProbabilityRuneEnUsVowel(chance <= 0.16).ToString();
          if (!sb.EndsWith(vowel) || "eo".Contains(vowel))
            sb.Append(vowel);
        }
      }

      return sb.ToUpperCaseInvariant(0, 1).LeftMost(length);
    }
  }
}
