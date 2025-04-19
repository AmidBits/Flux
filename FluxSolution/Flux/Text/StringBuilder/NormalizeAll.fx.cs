namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Normalize all consecutive instances of characters satisfying the <paramref name="predicate"/> to <paramref name="replacementCharacter"/> in the <paramref name="source"/>.</para>
    /// <example><code>"".NormalizeAll(' ', char.IsWhiteSpace);</code></example>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementCharacter"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <remarks>Normalizing means removing leading/trailing and replacing consecutive instances of characters with a single character.</remarks>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacementCharacter, System.Func<char, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var normalizedIndex = 0;

      var isPrevious = true;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        var isCurrent = predicate(c, index);

        if (!(isPrevious && isCurrent))
        {
          source[normalizedIndex++] = isCurrent ? replacementCharacter : c;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return normalizedIndex == source.Length ? source : source.Remove(normalizedIndex, source.Length - normalizedIndex);
    }

    /// <summary>
    /// <para>Normalize all consecutive instances of characters satisfying the <paramref name="predicate"/> to <paramref name="replacementCharacter"/> in the <paramref name="source"/>.</para>
    /// <example><code>"".NormalizeAll(' ', char.IsWhiteSpace);</code></example>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementCharacter"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// <remarks>Normalizing means removing leading/trailing and replacing consecutive instances of characters with a single character.</remarks>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacementCharacter, System.Func<char, bool> predicate) => source.NormalizeAll(replacementCharacter, (e, i) => predicate(e));

    /// <summary>
    /// <para>Normalize all consecutive instances of the specified <paramref name="charactersToNormalize"/> to <paramref name="replacementCharacter"/> in the <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementCharacter"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="charactersToNormalize"></param>
    /// <returns></returns>
    /// <remarks>Normalizing means removing leading/trailing and replacing consecutive instances of characters with a single character.</remarks>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacementCharacter, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToNormalize)
      => source.NormalizeAll(replacementCharacter, t => charactersToNormalize.Contains(t, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default));
  }
}
