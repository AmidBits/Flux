namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns whether <paramref name="count"/> of any <paramref name="values"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool IsCommonSuffixAny<T>(this System.ReadOnlySpan<T> source, int offset, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] values)
      => source.IsCommonSuffix(offset, count, c => values.Contains(c, equalityComparer));

    #region System.Char extension methods

    /// <summary>
    /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>].</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonSuffixAny(this System.ReadOnlySpan<char> source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params string[] values)
    {
      for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
        if (values[valuesIndex] is var value && source.IsCommonSuffix(offset, value.AsSpan().Slice(0, int.Min(value.Length, maxLength)), equalityComparer))
          return true;

      return false;
    }

    /// <summary>
    /// <para>Returns whether any <paramref name="values"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>].</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonSuffixAny(this System.ReadOnlySpan<char> source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      => source.IsCommonSuffixAny(offset, equalityComparer, int.MaxValue, values);

    #endregion // System.Char extension methods
  }
}
