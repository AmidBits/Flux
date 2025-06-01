namespace Flux
{
  public static partial class ReadOnlySpans
  {
    #region IsCommonPrefixAny

    /// <summary>
    /// <para>Returns whether <paramref name="source"/> starts with any <paramref name="length"/> combination of <paramref name="values"/>. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="length"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsCommonPrefixAny<T>(this System.ReadOnlySpan<T> source, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
      => source.IsCommonPrefix(length, c => values.Contains(c, equalityComparer));

    #region System.Char extensions

    /// <summary>
    /// <para>Returns whether <paramref name="source"/> starts with <paramref name="maxLength"/> (or the actual length if less) of any string <paramref name="values"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsCommonPrefixAny(this System.ReadOnlySpan<char> source, int maxLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
    {
      for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
        if (values[valuesIndex] is var value && source.IsCommonPrefix(value.AsSpan()[..int.Min(value.Length, maxLength)], equalityComparer))
          return true;

      return false;
    }

    /// <summary>
    /// <para>Returns whether <paramref name="source"/> starts with any of the string <paramref name="values"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsCommonPrefixAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      => source.IsCommonPrefixAny(int.MaxValue, equalityComparer, values);

    #endregion // System.Char extensions

    #endregion // IsCommonPrefixAny
  }
}
