namespace Flux
{
  public static class RelativeFrequencyOfLetters
  {
    /// <summary>
    /// <para>The relative frequency of letters for "en" (English).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Letter_frequency"/></para>
    /// </summary>
    private static readonly System.Collections.Generic.Dictionary<char, double> m_relativeFrequencyOfLetters_en = new(new StringComparerEx(System.Globalization.CultureInfo.GetCultureInfo("en"), System.Globalization.CompareOptions.IgnoreCase))
    {
      { 'a', 0.08167 },
      { 'b', 0.01492 },
      { 'c', 0.02782 },
      { 'd', 0.04253 },
      { 'e', 0.12702 },
      { 'f', 0.02228 },
      { 'g', 0.02015 },
      { 'h', 0.06094 },
      { 'i', 0.06966 },
      { 'j', 0.00153 },
      { 'k', 0.00772 },
      { 'l', 0.04025 },
      { 'm', 0.02406 },
      { 'n', 0.06749 },
      { 'o', 0.07507 },
      { 'p', 0.01929 },
      { 'q', 0.00095 },
      { 'r', 0.05987 },
      { 's', 0.06327 },
      { 't', 0.09056 },
      { 'u', 0.02758 },
      { 'v', 0.00978 },
      { 'w', 0.02360 },
      { 'x', 0.00150 },
      { 'y', 0.01974 },
      { 'z', 0.00074 },
    };

    extension(System.Globalization.CultureInfo source)
    {
      public System.Collections.Generic.IReadOnlyDictionary<char, double> GetRelativeFrequencyOfLetters()
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          "en" => m_relativeFrequencyOfLetters_en,
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }

      public double GetRelativeFrequencyOfLetter(char character)
      {
        source ??= System.Globalization.CultureInfo.CurrentCulture;

        return source.TwoLetterISOLanguageName switch
        {
          "en" => m_relativeFrequencyOfLetters_en.TryGetValue(character, out var relativeFrequencyOfLetter) ? relativeFrequencyOfLetter : throw new System.NotImplementedException($"Culture \"{source.TwoLetterISOLanguageName}\", letter '{character}'."),
          _ => throw new System.NotImplementedException(nameof(source))
        };
      }
    }
  }
}
/*
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'e', 12.7),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'t', 9.06),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'a', 8.17),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'i', 6.97),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'o', 7.51),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'n', 6.7),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'s', 6.33),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'h', 6.09),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'r', 5.99),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'b', 1.5),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'c', 2.8),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'d', 4.25),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'l', 4.03),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'u', 2.76),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'m', 2.41),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'w', 2.36),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'f', 2.23),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'g', 2.02),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'y', 1.97),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'p', 1.93),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'v', 0.98),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'k', 0.77),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'j', 0.15),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'x', 0.15),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'q', 0.1),
				new System.Collections.Generic.KeyValuePair<System.Text.Rune, double>((System.Text.Rune)'z', 0.07),

 */
