namespace Flux
{
  public static partial class IDictionaryEm
  {
    /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into a single composite string.</summary>
    public static string ToConsoleString<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, char horizontalSeparator = '\u003D', string verticalSeparator = "\u000d\u000a")
      where TKey : notnull
      => string.Join(verticalSeparator, ToConsoleStrings(source, horizontalSeparator));

    /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into strings.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, char horizontalSeparator = '\u003D')
      where TKey : notnull
      => System.Linq.Enumerable.Select(source, kvp => $"{kvp.Key}{horizontalSeparator}{kvp.Value}");
  }
}
