namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first character in the string.</summary>
    public static SpanBuilder<char> SplitFromCamelCase(ref this SpanBuilder<char> source, char separator = ' ', System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.InvariantCulture;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var left = index > 0 ? source[index - 1] : default;
        var current = source[index];
        var right = index < source.Length - 1 ? source[index + 1] : default;

        if (char.IsUpper(current))
        {
          if (char.IsLower(right))
            source[index] = char.ToLower(current, culture);

          if (index > 0 && (char.IsLower(left) || char.IsLower(right)))
            source.Insert(index, separator, 1);
        }
      }

      return source;
    }
  }
}
