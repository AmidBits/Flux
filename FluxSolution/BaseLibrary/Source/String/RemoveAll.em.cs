using System.Linq;

namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Remove all characters satisfying the predicate from the string.</summary>
    /// <example>"".RemoveAll(char.IsWhiteSpace);</example>
    /// <example>"".RemoveAll(c => c == ' ');</example>
    public static string RemoveAll(this string source, System.Func<char, bool> predicate)
    {
      var buffer = source.ToCharArray();

      var index = 0;

      foreach (var c in buffer)
      {
        if (!predicate(c))
        {
          buffer[index++] = c;
        }
      }

      return new string(buffer, 0, index);
    }
    /// <summary>Remove all specified characters from the string.</summary>
    public static string RemoveAll(this string source, params char[] remove)
      => source.RemoveAll(remove.Contains);
  }
}
