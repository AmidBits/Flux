namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static System.Span<char> FromCamelCase(this System.Span<char> source, System.Func<char, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var wasPredicate = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        if (wasPredicate && char.IsLetter(c) && char.IsUpper(c))
          source[index] = char.ToLower(c, culture);

        wasPredicate = predicate(c);
      }

      return source;
    }
    public static System.Span<char> FromCamelCase(this System.Span<char> source) => FromCamelCase(source, char.IsWhiteSpace);
  }
}
