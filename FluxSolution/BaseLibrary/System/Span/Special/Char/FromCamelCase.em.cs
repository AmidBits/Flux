namespace Flux
{
  public static partial class Em
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string. Uses the specified culture, or the current culture if null.</summary>
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

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string. Uses the specified culture, or the invariant culture if null.</summary>
    public static System.Span<char> FromCamelCase(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
      => FromCamelCase(source, char.IsWhiteSpace, culture ?? System.Globalization.CultureInfo.InvariantCulture);
  }
}
