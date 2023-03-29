namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static SpanBuilder<char> ExpandFromCamelCase(ref this SpanBuilder<char> source, char separator = ' ', System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && char.IsLetter(c) && char.IsUpper(c))
        {
          source[index] = char.ToLower(c, culture);

          if (index > 0)
            source.Insert(index++, separator);
        }

        wasPredicate = char.IsLetter(c) && char.IsLower(c);
      }

      return source;
    }
    public static SpanBuilder<char> ExpandFromCamelCase(ref this SpanBuilder<char> source) => ExpandFromCamelCase(ref source, ' ');
  }
}
