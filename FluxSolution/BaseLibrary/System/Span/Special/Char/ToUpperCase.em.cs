namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the specified culture, or the current culture if null.</summary>
    public static System.Span<char> ToUpperCase(this System.Span<char> source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = char.ToUpper(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }

    /// <summary>Convert all characters to upper case. Uses the specified culture, or the invariant culture if null.</summary>
    public static System.Span<char> ToUpperCase(this System.Span<char> source, System.Globalization.CultureInfo? culture = null)
      => ToUpperCase(source, 0, source.Length, culture ?? System.Globalization.CultureInfo.InvariantCulture);
  }
}
