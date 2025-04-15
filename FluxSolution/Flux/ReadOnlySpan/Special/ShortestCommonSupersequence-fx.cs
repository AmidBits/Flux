namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Find the length of the shortest common supersequence (SCS) between two sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Shortest_common_supersequence_problem"/></para>
    /// <para><seealso cref="http://rosettacode.org/wiki/Shortest_common_supersequence#C"/></para>
    /// <para><see href="https://www.techiedelight.com/shortest-common-supersequence-finding-scs/"/></para>
    /// <remarks>This is the same routine as longest common subsequence (LCS).</remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="matrix"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int ShortestCommonSupersequenceLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      matrix = ShortestCommonSupersequenceMatrix(source, target, out var length, equalityComparer);

      return length;
    }

    /// <summary>
    /// <para>Create a dynamic programming matrix of shortest common supersequence (SCS) between two sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Shortest_common_supersequence_problem"/></para>
    /// <para><seealso cref="http://rosettacode.org/wiki/Shortest_common_supersequence#C"/></para>
    /// <para><see href="https://www.techiedelight.com/shortest-common-supersequence-finding-scs/"/></para>
    /// <remarks>This is the same routine as longest common subsequence (LCS).</remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="lengthScs"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int[,] ShortestCommonSupersequenceMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int lengthScs, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      var scsg = new int[sourceLength + 1, targetLength + 1];

      for (int si = sourceLength - 1; si >= 0; si--)
        scsg[si, 0] = si;
      for (int ti = targetLength - 1; ti >= 0; ti--)
        scsg[0, ti] = ti;

      for (var si = 0; si < sourceLength; si++)
        for (var ti = 0; ti < targetLength; ti++)
          scsg[si + 1, ti + 1] = equalityComparer.Equals(source[si], target[ti]) ? scsg[si, ti] + 1 : int.Min(scsg[si, ti + 1], scsg[si + 1, ti]) + 1;

      lengthScs = scsg[source.Length, target.Length];

      return scsg;
    }

    /// <summary>
    /// <para>Finding the shortest common supersequence (SCS) of two sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Shortest_common_supersequence_problem"/></para>
    /// <para><seealso cref="http://rosettacode.org/wiki/Shortest_common_supersequence#C"/></para>
    /// <para><see href="https://www.techiedelight.com/shortest-common-supersequence-finding-scs/"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="matrix"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<T> ShortestCommonSupersequenceValues<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      matrix = ShortestCommonSupersequenceMatrix(source, target, out var _, equalityComparer);

      return GetSupersequence(matrix, source, target, source.Length, target.Length, equalityComparer);

      static System.Collections.Generic.List<T> GetSupersequence(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int si, int ti, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      {
        if (si == 0) // If the end of the first string is reached, return the second string.
          return target[..ti].ToArray().ToList();
        else if (ti == 0) // If the end of the second string is reached, return the first string.
          return source[..si].ToArray().ToList();

        if (equalityComparer.Equals(source[si - 1], target[ti - 1])) // If the last character of si and ti matches, include it and recur to find SCS of substring.
        {
          var list = GetSupersequence(matrix, source, target, si - 1, ti - 1, equalityComparer);
          list.Add(source[si - 1]);
          return list;
        }
        else
        {
          if (matrix[si - 1, ti] <= matrix[si, ti - 1]) // If the top cell has a value less or equal to that in the left cell, then include the current source element and find SCS of substring less the one added.
          {
            var list = GetSupersequence(matrix, source, target, si - 1, ti, equalityComparer);
            list.Add(source[si - 1]);
            return list;
          }
          else // If the left cell has a value greater than that in the top cell, then include the current target element find SCS of substring less the one added.
          {
            var list = GetSupersequence(matrix, source, target, si, ti - 1, equalityComparer);
            list.Add(target[ti - 1]);
            return list;
          }
        }
      }
    }
  }
}

/*
  var scs = new Flux.Metrical.ShortestCommonSupersequence<char>();

  var a = @"abcbdab";
  var b = @"bdcaba";

  var fm = scs.GetFullMatrix(a, b);
  var l = scs.GetList(a, b); // Now contains: "abdcabdab"
*/
