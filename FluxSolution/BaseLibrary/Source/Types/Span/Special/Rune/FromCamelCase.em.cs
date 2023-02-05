namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
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
    public static System.Span<System.Text.Rune> FromCamelCase(this System.Span<System.Text.Rune> source) => FromCamelCase(source, System.Text.Rune.IsWhiteSpace);
  }
}
