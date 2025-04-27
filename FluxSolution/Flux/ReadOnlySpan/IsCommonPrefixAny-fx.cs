namespace Flux
{
  public static partial class ReadOnlySpans
  {
    #region IsCommonPrefixAny

    /// <summary>
    /// <para>Returns whether <paramref name="source"/> starts with <paramref name="count"/> of any <paramref name="values"/>. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsCommonPrefixAny<T>(this System.ReadOnlySpan<T> source, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
      => source.IsCommonPrefix(count, c => values.Contains(c, equalityComparer));

    #region System.Char extension methods

    /// <summary>
    /// <para>Returns whether <paramref name="source"/> starts with <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonPrefixAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params string[] values)
    {
      for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
        if (values[valuesIndex] is var value && source.IsCommonPrefix(value.AsSpan()[..int.Min(value.Length, maxLength)], equalityComparer))
          return true;

      return false;
    }

    /// <summary>
    /// <para>Returns whether <paramref name="source"/> starts with any <paramref name="values"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonPrefixAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      => source.IsCommonPrefixAny(equalityComparer, int.MaxValue, values);

    #endregion // System.Char extension methods

    #endregion // IsCommonPrefixAny
  }
}
