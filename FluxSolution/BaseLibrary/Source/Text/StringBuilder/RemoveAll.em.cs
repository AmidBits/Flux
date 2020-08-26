using System.Linq;

namespace Flux
{
  public static partial class XtendStringBuilder
  {
    /// <summary>Remove all characters satisfying the predicate from the string.</summary>
    /// <example>"".RemoveAll(char.IsWhiteSpace);</example>
    /// <example>"".RemoveAll(c => c == ' ');</example>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var targetIndex = 0;

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
        if (source[sourceIndex] is var c && !predicate(c))
          source[targetIndex++] = c;

      return source.Remove(targetIndex, source.Length - targetIndex);
    }
    /// <summary>Remove all specified characters from the string.</summary>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, params char[] remove)
      => source.RemoveAll(remove.Contains);
  }
}
