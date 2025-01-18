namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns whether <paramref name="count"/> of any <paramref name="values"/> are found at <paramref name="offset"/> in the <paramref name="source"/>. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsCommonPrefixAny<T>(this System.ReadOnlySpan<T> source, int offset, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
      => source.IsCommonPrefix(offset, count, c => values.Contains(c, equalityComparer));

    #region System.Char extension methods

    /// <summary>
    /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/> are found at the <paramref name="offset"/> in the <paramref name="source"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonPrefixAny(this System.ReadOnlySpan<char> source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params string[] values)
    {
      for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
        if (values[valuesIndex] is var value && source.IsCommonPrefix(offset, value.AsSpan().Slice(0, int.Min(value.Length, maxLength)), equalityComparer))
          return true;

      return false;
    }

    /// <summary>
    /// <para>Returns whether any <paramref name="values"/> are found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonPrefixAny(this System.ReadOnlySpan<char> source, int sourceIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      => source.IsCommonPrefixAny(sourceIndex, equalityComparer, int.MaxValue, values);

    #endregion // System.Char extension methods
  }
}
