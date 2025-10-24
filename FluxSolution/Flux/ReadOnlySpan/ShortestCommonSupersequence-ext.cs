namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>Find the length of the shortest common supersequence (SCS) between two sequences, by creating a dynamic programming matrix.</para>
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
      public int ShortestCommonSupersequenceCount(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var sourceLength = source.Length;
        var targetLength = target.Length;

        matrix = new int[sourceLength + 1, targetLength + 1];

        for (int si = sourceLength - 1; si >= 0; si--)
          matrix[si, 0] = si;
        for (int ti = targetLength - 1; ti >= 0; ti--)
          matrix[0, ti] = ti;

        for (var si = 0; si < sourceLength; si++)
          for (var ti = 0; ti < targetLength; ti++)
            matrix[si + 1, ti + 1] = equalityComparer.Equals(source[si], target[ti]) ? matrix[si, ti] + 1 : int.Min(matrix[si, ti + 1], matrix[si + 1, ti]) + 1;

        var length = matrix[source.Length, target.Length];

        return length;
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
      public System.Collections.Generic.List<T> ShortestCommonSupersequenceValues(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        ShortestCommonSupersequenceCount(source, target, out matrix, equalityComparer);

        return GetSupersequence(matrix, source, target, source.Length, target.Length, equalityComparer);
      }
    }

    private static System.Collections.Generic.List<T> GetSupersequence<T>(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int si, int ti, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
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

/*
  var scs = new Flux.Metrical.ShortestCommonSupersequence<char>();

  var a = @"abcbdab";
  var b = @"bdcaba";

  var fm = scs.GetFullMatrix(a, b);
  var l = scs.GetList(a, b); // Now contains: "abdcabdab"
*/
