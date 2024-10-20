namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether there are <paramref name="count"/> elements satisfying the <paramref name="predicate"/> that ends <paramref name="source"/>[end - <paramref name="offset"/>].</summary>
    public static bool IsCommonSuffix(this System.Text.StringBuilder source, int offset, int count, System.Func<char, bool> predicate)
      => source.CommonSuffixLength(offset, predicate, count) == count;

    /// <summary>Indicates whether there are <paramref name="count"/> elements equal to <paramref name="value"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool IsCommonSuffix(this System.Text.StringBuilder source, int offset, int count, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonSuffixLength(offset, value, equalityComparer, count) == count;

    /// <summary>Indicates whether <paramref name="value"/> exists and ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool IsCommonSuffix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonSuffixLength(offset, value, equalityComparer) == value.Length;
  }
}
