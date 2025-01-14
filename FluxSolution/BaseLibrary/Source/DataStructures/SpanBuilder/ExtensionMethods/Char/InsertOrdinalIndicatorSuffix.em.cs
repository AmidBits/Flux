namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Appends ordinal extensions (e.g. rd, th, etc.) to any digit numerals (e.g. 3, 12, etc.) in the <paramref name="source"/> that satisfies the <paramref name="predicate"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static void InsertOrdinalIndicatorSuffix(this ref SpanMaker<char> source, System.Func<string, string, string, bool>? predicate = null)
    {
      predicate ??= (textOnLeft, suffix, textOnRight) => { System.Diagnostics.Debug.WriteLine($"{textOnLeft}>{suffix}<{textOnRight}"); return true; };

      var wasDigit = false;

      var sm = source;

      for (var index = sm.Count - 1; index >= 0; index--)
      {
        var c = sm[index];

        var isDigit = char.IsDigit(c);

        if (isDigit && !wasDigit)
        {
          var isBetweenTenAndTwenty = index > 0 && sm[index - 1] == '1';

          var suffix = c switch
          {
            '1' when !isBetweenTenAndTwenty => @"st",
            '2' when !isBetweenTenAndTwenty => @"nd",
            '3' when !isBetweenTenAndTwenty => @"rd",
            _ => @"th"
          };

          if (predicate(sm.AsReadOnlySpan()[..(index + 1)].ToString(), suffix, sm.AsReadOnlySpan()[(index + 1)..].ToString()))
            sm = sm.Insert(index + 1, 1, suffix.AsSpan());
        }

        wasDigit = isDigit;
      }

      source = sm;
    }
  }
}
