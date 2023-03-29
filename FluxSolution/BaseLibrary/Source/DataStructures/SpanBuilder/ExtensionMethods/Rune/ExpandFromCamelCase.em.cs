namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static SpanBuilder<System.Text.Rune> ExpandFromCamelCase(ref this SpanBuilder<System.Text.Rune> source, System.Text.Rune separator, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && System.Text.Rune.IsLetter(c) && System.Text.Rune.IsUpper(c))
        {
          source[index] = System.Text.Rune.ToLower(c, culture);

          if (index > 0)
            source.Insert(index++, separator);
        }

        wasPredicate = System.Text.Rune.IsLetter(c) && System.Text.Rune.IsLower(c);
      }

      return source;
    }
    public static SpanBuilder<System.Text.Rune> ExpandFromCamelCase(ref this SpanBuilder<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null) => ExpandFromCamelCase(ref source, (System.Text.Rune)' ', culture);
  }
}
