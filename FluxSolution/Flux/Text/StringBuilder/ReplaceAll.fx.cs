namespace Flux
{
  public static partial class StringBuilders
  {
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, int index, int length, System.Func<char, int, bool> predicate, System.Func<char, int, string?> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegative(index);
      System.ArgumentOutOfRangeException.ThrowIfNegative(length);
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var i = index + length - 1; i >= index; i--)
        if (source[i] is var c && predicate(c, i) && replacementSelector(c, i) is var r && r is not null)
          source.Remove(i, 1).Insert(i, r);

      return source;
    }

    /// <summary>
    /// <para>Replace all characters satisfying the <paramref name="predicate"/> using the <paramref name="replacementSelector"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    /// <remarks>In both the <paramref name="predicate"/>(e, i, bool) and the <paramref name="replacementSelector"/>(e, i, string): e = the character, i = the index of the character.</remarks>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, int, bool> predicate, System.Func<char, int, string?> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        if (predicate(c, index) && replacementSelector(c, index) is var r && r is not null)
          source.Remove(index, 1).Insert(index, r);
      }

      return source;
    }

    /// <summary>
    /// <para>Replace all characters satisfying the <paramref name="predicate"/> using the <paramref name="replacementSelector"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    /// <remarks>In both the <paramref name="predicate"/>(e, bool) and the <paramref name="replacementSelector"/>(e, string): e = the character.</remarks>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, bool> predicate, System.Func<char, string?> replacementSelector)
      => source.ReplaceAll((c, i) => predicate(c), (c, i) => replacementSelector(c));

    /// <summary>
    /// <para>Replace all characters using the <paramref name="replacementSelector"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder ReplaceAll(this System.Text.StringBuilder source, System.Func<char, char> replacementSelector)
      => source.ReplaceAll(c => replacementSelector(c) != c, c => replacementSelector(c).ToString());
  }
}
