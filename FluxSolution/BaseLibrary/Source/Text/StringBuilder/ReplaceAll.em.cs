using System.Linq;

namespace Flux
{
  public static partial class XtendStringBuilder
  {
    /// <summary>Replace all characters using the specified replacement selector function. If the replacement selector returns null/default or empty, no replacement is made.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, string> replacementSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = 0; index < source.Length; index++)
      {
        var replacement = replacementSelector(source[index]);

        if (!string.IsNullOrEmpty(replacement))
        {
          source.Remove(index, 1);
          source.Insert(index, replacement);

          index += replacement.Length - 1;
        }
      }

      return source;
    }
    /// <summary>Replace all characters using the specified replacement selector function.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, char> replacementSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = replacementSelector(source[index]);

      return source;
    }

    /// <summary>Replace all characters satisfying the predicate with the specified character.</summary>
    /// <example>"".ReplaceAll(replacement, char.IsWhiteSpace);</example>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, char replacement, System.Func<char, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index]))
          source[index] = replacement;

      return source;
    }
    /// <summary>Replace all specified characters with the specified character.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, char replacement, params char[] replace)
      => source.ReplaceAll(replacement, replace.Contains);
  }
}
