namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. Uses the specified culture, or the current culture if null.</summary>
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

    /// <summary>Makes CamelCase of words separated by whitespace. Uses the specified culture, or the invariant culture if null.</summary>
    public static System.Span<char> ToCamelCase(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
      => ToCamelCase(source, char.IsWhiteSpace, culture ?? System.Globalization.CultureInfo.InvariantCulture);
  }
}
