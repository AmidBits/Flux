namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Replace (in-place) all characters with their lower case equivalents. Uses the specified culture.</summary>
    public static void ToLowerCase(this System.Span<char> source, System.Globalization.CultureInfo culture)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = char.ToLower(source[index], culture);
    }
    /// <summary>Replace (in-place) all characters with their lower case equivalents. Uses the current culture.</summary>
    public static void ToLowerCase(this System.Span<char> source)
      => ToLowerCase(source, System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Replace (in-place) all characters with their lower case equivalents. Uses the invariant culture.</summary>
    public static void ToLowerCaseInvariant(this System.Span<char> source)
      => ToLowerCase(source, System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Replace (in-place) all characters with their upper case equivalents. Uses the specified culture.</summary>
    public static void ToUpperCase(this System.Span<char> source, System.Globalization.CultureInfo culture)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = char.ToUpper(source[index], culture);
    }
    /// <summary>Replace (in-place) all characters with their upper case equivalents. Uses the current culture.</summary>
    public static void ToUpperCase(this System.Span<char> source)
      => ToUpperCase(source, System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Replace (in-place) all characters with their upper case equivalents. Uses the invariant culture.</summary>
    public static void ToUpperCaseInvariant(this System.Span<char> source)
      => ToUpperCase(source, System.Globalization.CultureInfo.InvariantCulture);
  }
}
