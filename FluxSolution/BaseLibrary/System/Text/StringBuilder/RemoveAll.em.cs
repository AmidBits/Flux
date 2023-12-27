namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Remove all characters satisfying the predicate from the string.</summary>
    /// <example>"".RemoveAll(char.IsWhiteSpace);</example>
    /// <example>"".RemoveAll(c => c == ' ');</example>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var sourceLength = source.Length;

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
        if (source[sourceIndex] is var character && !predicate(character))
          source[removedIndex++] = character;

      return source.Remove(removedIndex, source.Length - removedIndex);
    }
    /// <summary>Remove the specified characters. Uses the specified comparer.</summary>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> equalityComparer, params char[] remove)
      => RemoveAll(source, t => remove.Contains(t, equalityComparer));
    /// <summary>Remove the specified characters. Uses the default comparer.</summary>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, params char[] remove)
      => RemoveAll(source, remove.Contains);
  }
}
