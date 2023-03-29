namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static SpanBuilder<char> InsertOrdinalIndicatorSuffix(ref this SpanBuilder<char> source, System.Func<string, string, bool>? predicate = null)
    {
      var wasDigit = false;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        var isDigit = char.IsDigit(c);

        if (isDigit && !wasDigit)
        {
          var isBetweenTenAndTwenty = index > 0 && source[index - 1] == '1';

          var suffix = c switch
          {
            '1' when !isBetweenTenAndTwenty => @"st",
            '2' when !isBetweenTenAndTwenty => @"nd",
            '3' when !isBetweenTenAndTwenty => @"rd",
            _ => @"th"
          };

          if (predicate?.Invoke(source.AsReadOnlySpan()[..(index + 1)].ToString(), suffix) ?? true)
            source.Insert(index + 1, suffix.AsSpan());
        }

        wasDigit = isDigit;
      }

      return source;
    }
  }
}
