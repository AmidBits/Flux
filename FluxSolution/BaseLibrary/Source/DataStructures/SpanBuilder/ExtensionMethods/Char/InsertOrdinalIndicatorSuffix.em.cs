namespace Flux
{
  public static partial class Em
  {
    /// <summary>Appends ordinal extensions (e.g. rd, th, etc.) to any digit numerals (e.g. 3, 12, etc.) in the <paramref name="source"/> that satisfies the <paramref name="predicate"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static SpanBuilder<char> InsertOrdinalIndicatorSuffix(this SpanBuilder<char> source, System.Func<string, string, string, bool>? predicate = null)
    {
      predicate ??= (textOnLeft, suffix, textOnRight) => { System.Diagnostics.Debug.WriteLine($"{textOnLeft}>{suffix}<{textOnRight}"); return true; };

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

          if (predicate(source.AsReadOnlySpan()[..(index + 1)].ToString(), suffix, source.AsReadOnlySpan()[(index + 1)..].ToString()))
            source.Insert(index + 1, suffix.AsSpan(), 1);
        }

        wasDigit = isDigit;
      }

      return source;
    }
  }
}
