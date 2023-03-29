namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static SpanBuilder<System.Text.Rune> CollapseToCamelCase(ref this SpanBuilder<System.Text.Rune> source, System.Text.Rune separator, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && System.Text.Rune.IsLetter(c) && System.Text.Rune.IsLower(c))
        {
          source[index] = System.Text.Rune.ToUpper(c, culture);

          if (index > 0)
            source.Remove(--index, 1);
        }

        wasPredicate = c == separator;
      }

      return source;
    }
    public static SpanBuilder<System.Text.Rune> CollapseToCamelCase(ref this SpanBuilder<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null) => CollapseToCamelCase(ref source, (System.Text.Rune)' ', culture);
  }
}
