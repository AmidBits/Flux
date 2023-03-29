namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Makes CamelCase of <paramref name="source"/> words separated by <paramref name="separator"/>. Uses the specified <paramref name="culture"/>.</summary>
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
  }
}
