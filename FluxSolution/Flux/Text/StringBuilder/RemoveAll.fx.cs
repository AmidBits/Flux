namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Remove all characters satisfying the <paramref name="predicate"/> from the <paramref name="source"/>.</para>
    /// <para><example><code>"".RemoveAll(char.IsWhiteSpace);</code></example></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, System.Func<char, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var removedIndex = 0;

      for (var index = 0; index < source.Length; index++)
        if (source[index] is var c && !predicate(c, index))
          source[removedIndex++] = c;

      return source.Remove(removedIndex, source.Length - removedIndex);
    }

    /// <summary>
    /// <para>Remove all characters satisfying the <paramref name="predicate"/> from the <paramref name="source"/>.</para>
    /// <para><example><code>"".RemoveAll(char.IsWhiteSpace);</code></example></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, System.Func<char, bool> predicate) => source.RemoveAll((e, i) => predicate(e));

    /// <summary>
    /// <para>Remove the specified <paramref name="charactersToRemove"/> from the <paramref name="source"/>. Uses a specified <paramref name="equalityComparer"/> (or default if null).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="charactersToRemove"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder RemoveAll(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToRemove)
      => RemoveAll(source, c => charactersToRemove.Contains(c, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default));
  }
}
