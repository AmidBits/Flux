namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public T[] LongestAlternatingSubsequence(System.Collections.Generic.IComparer<T> comparer)
      => new Metrical.LongestAlternatingSubsequence<T>(comparer).GetSubsequence(AsReadOnlySpan(), out var _);

    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public T[] LongestAlternatingSubsequence()
      => LongestAlternatingSubsequence(System.Collections.Generic.Comparer<T>.Default);
  }
}
