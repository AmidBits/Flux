//namespace Flux
//{
//  public static partial class StringBuilders
//  {
//    /// <summary>
//    /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) characters of <paramref name="value"/> are found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
//    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <param name="sourceIndex"></param>
//    /// <param name="maxLength"></param>
//    /// <param name="value"></param>
//    /// <param name="equalityComparer"></param>
//    /// <returns></returns>
//    public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
//      => source.MatchLength(sourceIndex, value, equalityComparer) == value.Length;
//  }
//}
