using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary></summary>
    public static string ToConsoleString<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D')
      where TKey : notnull
      => string.Join(verticalSeparator, source.Select(kvp => $"{kvp.Key}{horizontalSeparator}{kvp.Value}"));
  }
}
