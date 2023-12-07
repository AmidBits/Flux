namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Replace all characters using the specified replacement selector function. If the replacement selector returns null/default or empty, no replacement is made.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, string> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = source.Length - 1; index >= 0; index--)
        if (replacementSelector(source[index]) is var replacement && !string.IsNullOrEmpty(replacement))
          source.Remove(index, 1).Insert(index, replacement);

      return source;
    }

    /// <summary>Replace all characters using the specified replacement selector function.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, char> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = replacementSelector(source[index]);

      return source;
    }

    /// <summary>Replace all characters satisfying the predicate with the specified character.</summary>
    /// <example>"".ReplaceAll(replacement, char.IsWhiteSpace);</example>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, char replacement, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index]))
          source[index] = replacement;

      return source;
    }
    /// <summary>Replace all specified characters with the specified character.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, char replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> equalityComparer, params char[] replace)
      => source.ReplaceAll(replacement, t => replace.Contains(t, equalityComparer));
    /// <summary>Replace all specified characters with the specified character.</summary>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, char replacement, params char[] replace)
      => source.ReplaceAll(replacement, replace.Contains);
  }
}
