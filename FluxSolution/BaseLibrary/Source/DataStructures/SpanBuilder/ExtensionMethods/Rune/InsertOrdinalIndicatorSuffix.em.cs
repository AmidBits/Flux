namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static void InsertOrdinalIndicatorSuffix(this ref SpanBuilder<System.Text.Rune> source, System.Func<System.Text.Rune[], System.Text.Rune[], System.Text.Rune[], bool>? predicate = null)
    {
      predicate ??= (textOnLeft, suffix, textOnRight) => { System.Diagnostics.Debug.WriteLine($"{textOnLeft}>{suffix}<{textOnRight}"); return true; };

      var wasDigit = false;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var r = source[index];

        var isDigit = System.Text.Rune.IsDigit(r);

        if (isDigit && !wasDigit)
        {
          var isBetweenTenAndTwenty = index > 0 && source[index - 1] == (System.Text.Rune)'1';

          var suffix = (r == (System.Text.Rune)'1' && !isBetweenTenAndTwenty) ? new System.Text.Rune[] { (System.Text.Rune)'s', (System.Text.Rune)'t' }
            : (r == (System.Text.Rune)'2' && !isBetweenTenAndTwenty) ? new System.Text.Rune[] { (System.Text.Rune)'n', (System.Text.Rune)'d' }
            : (r == (System.Text.Rune)'3' && !isBetweenTenAndTwenty) ? new System.Text.Rune[] { (System.Text.Rune)'r', (System.Text.Rune)'d' }
            : new System.Text.Rune[] { (System.Text.Rune)'t', (System.Text.Rune)'h' };

          if (predicate(source.AsReadOnlySpan()[..(index + 1)].ToArray(), suffix, source.AsReadOnlySpan()[(index + 1)..].ToArray()))
            source.Insert(index + 1, suffix.AsSpan(), 1);
        }

        wasDigit = isDigit;
      }
    }
  }
}
