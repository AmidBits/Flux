namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static SequenceBuilder<System.Text.Rune> InsertOrdinalIndicatorSuffix(this SequenceBuilder<System.Text.Rune> source)
    {
      var wasDigit = false;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var r = source[index];

        var isDigit = System.Text.Rune.IsDigit(r);

        if (isDigit && !wasDigit)
        {
          var isBetweenTenAndTwenty = index > 0 && source[index - 1] == (System.Text.Rune)'1';

          var suffix = (r == (System.Text.Rune)'1' && !isBetweenTenAndTwenty) ? "st".ToSequenceBuilder().ToSequenceBuilderOfRune().AsReadOnlySpan()
            : (r == (System.Text.Rune)'2' && !isBetweenTenAndTwenty) ? "nd".ToSequenceBuilder().ToSequenceBuilderOfRune().AsReadOnlySpan()
            : (r == (System.Text.Rune)'3' && !isBetweenTenAndTwenty) ? "rd".ToSequenceBuilder().ToSequenceBuilderOfRune().AsReadOnlySpan()
            : "th".ToSequenceBuilder().ToSequenceBuilderOfRune().AsReadOnlySpan();

          source.Insert(index + 1, suffix);
        }

        wasDigit = isDigit;
      }

      return source;
    }
  }
}
