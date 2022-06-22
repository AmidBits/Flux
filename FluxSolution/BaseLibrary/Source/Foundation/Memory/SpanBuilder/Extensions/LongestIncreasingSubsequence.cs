namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(ref this SpanBuilder<T> source, System.Collections.Generic.IComparer<T> comparer)
      => new Metrical.LongestIncreasingSubsequence<T>(comparer).GetSubsequence(source, out var _);
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static T[] LongestIncreasingSubsequence<T>(ref this SpanBuilder<T> source)
      => LongestIncreasingSubsequence(ref source, System.Collections.Generic.Comparer<T>.Default);
  }
}
