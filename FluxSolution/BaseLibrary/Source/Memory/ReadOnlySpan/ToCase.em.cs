using System;

namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    public static System.ReadOnlySpan<char> ToLowerCase(this System.ReadOnlySpan<char> source)
    {
      var buffer = new char[source.Length];

      source.ToLower(buffer, System.Globalization.CultureInfo.CurrentCulture);

      return buffer;
    }
    public static System.ReadOnlySpan<char> ToUpperCase(this System.ReadOnlySpan<char> source)
    {
      var buffer = new char[source.Length];

      source.ToUpper(buffer, System.Globalization.CultureInfo.CurrentCulture);

      return buffer;
    }

    public static System.ReadOnlySpan<string> ToLowerCase(this System.ReadOnlySpan<string> source)
    {
      var buffer = new string[source.Length];

      for (var index = source.Length - 1; index >= 0; index--)
        buffer[index] = source[index].ToLower(System.Globalization.CultureInfo.CurrentCulture);

      return buffer;
    }
    public static System.ReadOnlySpan<string> ToUpperCase(this System.ReadOnlySpan<string> source)
    {
      var buffer = new string[source.Length];

      for (var index = source.Length - 1; index >= 0; index--)
        buffer[index] = source[index].ToLower(System.Globalization.CultureInfo.CurrentCulture);

      return buffer;
    }
  }
}
