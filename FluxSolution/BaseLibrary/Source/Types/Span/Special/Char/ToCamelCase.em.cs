namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static System.Span<char> ToCamelCase(this System.Span<char> source, System.Func<char, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && char.IsLetter(c) && char.IsLower(c))
          source[index] = char.ToUpper(c, culture);

        wasPredicate = predicate(c);
      }

      return source;
    }
    public static System.Span<char> ToCamelCase(this System.Span<char> source) => ToCamelCase(source, char.IsWhiteSpace);
  }
}
