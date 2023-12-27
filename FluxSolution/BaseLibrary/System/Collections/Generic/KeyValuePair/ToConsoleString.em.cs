namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into a single composite string.</summary>
    public static string ToConsoleString<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, System.Func<TKey, string> keySelector, System.Func<TValue, string> valueSelector, ConsoleStringOptions? options = null)
      where TKey : notnull
    {
      options ??= new ConsoleStringOptions() { HorizontalSeparator = '=' };

      return string.Join(System.Environment.NewLine, ToConsoleStrings(source, keySelector, valueSelector, options));
    }

    public static string ToConsoleString<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, ConsoleStringOptions? options = null)
      where TKey : notnull
      => ToConsoleString(source, k => $"{k}", v => $"{v}", options);

    /// <summary>Converts a sequence of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> into strings.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings<TKey, TValue>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> source, System.Func<TKey, string> keySelector, System.Func<TValue, string> valueSelector, ConsoleStringOptions? options = null)
      where TKey : notnull
    {
      options ??= new ConsoleStringOptions() { HorizontalSeparator = '=' };

      return System.Linq.Enumerable.Select(source, kvp => $"{keySelector(kvp.Key)}{options.HorizontalSeparator}{valueSelector(kvp.Value)}");
    }
  }
}
