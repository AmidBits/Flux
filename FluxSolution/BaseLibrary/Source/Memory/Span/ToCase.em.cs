using System;

namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    public static void ToLowerCase(this System.Span<char> source, System.Globalization.CultureInfo culture)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = char.ToLower(source[index], culture);
    }
    public static void ToLowerInvariant(this System.Span<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = char.ToLowerInvariant(source[index]);
    }
    public static void ToUpperCase(this System.Span<char> source, System.Globalization.CultureInfo culture)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = char.ToUpper(source[index], culture);
    }
    public static void ToUpperInvariant(this System.Span<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = char.ToUpperInvariant(source[index]);
    }
  }
}
