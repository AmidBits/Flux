namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string. Uses the specified culture, or the current culture if null.</summary>
    public static System.Span<System.Text.Rune> FromCamelCase(this System.Span<System.Text.Rune> source, System.Func<System.Text.Rune, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && System.Text.Rune.IsLetter(c) && System.Text.Rune.IsUpper(c))
          source[index] = System.Text.Rune.ToLower(c, culture);

        wasPredicate = predicate(c);
      }

      return source;
    }

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string. Uses the specified culture, or the invariant culture if null.</summary>
    public static System.Span<System.Text.Rune> FromCamelCase(this System.Span<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
      => source.FromCamelCase(System.Text.Rune.IsWhiteSpace, culture ?? System.Globalization.CultureInfo.InvariantCulture);
  }
}
