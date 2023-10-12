namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. Uses the specified culture, or the current culture if null.</summary>
    public static System.Span<System.Text.Rune> ToCamelCase(this System.Span<System.Text.Rune> source, System.Func<System.Text.Rune, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && System.Text.Rune.IsLetter(c) && System.Text.Rune.IsLower(c))
          source[index] = System.Text.Rune.ToUpper(c, culture);

        wasPredicate = predicate(c);
      }

      return source;
    }

    /// <summary>Makes CamelCase of words separated by the specified predicate. Uses the specified culture, or the invariant culture if null.</summary>
    public static System.Span<System.Text.Rune> ToCamelCase(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
      => ToCamelCase(source, System.Text.Rune.IsWhiteSpace, culture ?? System.Globalization.CultureInfo.InvariantCulture);
  }
}
