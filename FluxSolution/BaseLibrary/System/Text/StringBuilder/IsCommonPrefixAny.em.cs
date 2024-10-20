namespace Flux
{
  public static partial class Fx
  {
    public static bool IsCommonPrefixAny(this System.Text.StringBuilder source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params char[] values)
      => source.IsCommonPrefix(offset, maxLength, c => values.Contains(c, equalityComparer));

    /// <summary>
    /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/> are found at the <paramref name="offset"/> in the <paramref name="source"/>.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static bool IsCommonPrefixAny(this System.Text.StringBuilder source, int offset, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, int maxLength, params string[] values)
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
    public static bool IsCommonPrefixAny(this System.Text.StringBuilder source, int sourceIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
      => source.IsCommonPrefixAny(sourceIndex, equalityComparer, int.MaxValue, values);
  }
}
