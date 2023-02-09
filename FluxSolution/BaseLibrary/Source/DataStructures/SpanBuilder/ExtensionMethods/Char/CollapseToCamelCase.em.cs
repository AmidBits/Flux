namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static SpanBuilder<char> CollapseToCamelCase(ref this SpanBuilder<char> source, char separator = ' ', System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && char.IsLetter(c) && char.IsLower(c))
        {
          source[index] = char.ToUpper(c, culture);

          if (index > 0)
            source.Remove(--index, 1);
        }

        wasPredicate = c == separator;
      }

      return source;
    }
    public static SpanBuilder<char> CollapseToCamelCase(ref this SpanBuilder<char> source) => CollapseToCamelCase(ref source, ' ');
  }
}
