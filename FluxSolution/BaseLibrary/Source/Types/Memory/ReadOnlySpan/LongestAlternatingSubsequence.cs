namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>The longest increasing subsequence (LIS) is to find a subsequence of a given sequence where the elements of the subsequence are in sorted order, lowest to highest, and in which the subsequence is as long as possible.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Longest_increasing_subsequence"/>
    public static int LongestAlternatingSubsequence<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      throw new System.NotImplementedException();

      var sourceLength = source.Length;

      var las = 0;

      //var p = source[0];
      //var pc = 0;

      //var lac = 0;

      //for (var i = 1; i < sourceLength; i++)
      //{
      //  var c = source[i];
      //  var cc = comparer.Compare(p, c);

      //  if (pc == cc || pc == 0 || cc == 0)
      //  {
      //    if (lac > las)
      //      las = lac;

      //    lac = 0;
      //  }
      //}

      return las;
    }
  }
}
