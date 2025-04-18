namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>Finding the longest consecutive sequence of elements common to two or more sequences.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    public static int LongestCommonSubstringLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var maxLength = 0;

      var v1 = new int[target.Length + 1];
      var v0 = new int[target.Length + 1];

      for (var i = source.Length - 1; i >= 0; i--)
      {
        (v0, v1) = (v1, v0);

        for (var j = target.Length - 1; j >= 0; j--)
        {
          if (equalityComparer.Equals(source[i], target[j]))
          {
            v0[j] = v1[j + 1] + 1;

            maxLength = int.Max(maxLength, v0[j]);
          }
          else
          {
            v0[j] = 0;
          }
        }
      }

      return maxLength;
    }

    /// <summary>Finding the longest consecutive sequence of elements common to two or more sequences.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
    /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
    private static int[,] LongestCommonSubstringMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int length, out int sourceMaxIndex, out int targetMaxIndex, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var lcsg = new int[source.Length + 1, target.Length + 1];

      length = 0;

      sourceMaxIndex = 0;
      targetMaxIndex = 0;

      for (var si = 0; si <= source.Length; si++)
      {
        for (var ti = 0; ti <= target.Length; ti++)
        {
          if (si > 0 && ti > 0 && equalityComparer.Equals(source[si - 1], target[ti - 1]))
          {
            var temporaryLength = lcsg[si, ti] = lcsg[si - 1, ti - 1] + 1;

            if (temporaryLength > length)
            {
              length = temporaryLength;

              sourceMaxIndex = si;
              targetMaxIndex = ti;
            }
          }
          else
            lcsg[si, ti] = 0;
        }
      }

      return lcsg;
    }

    /// <summary>
    /// <para>Finding the longest consecutive sequence of elements common to two or more sequences.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longest_common_substring_problem"/></para>
    /// <para><seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="matrix"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static T[] LongestCommonSubstringValues<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      matrix = LongestCommonSubstringMatrix(source, target, out var length, out var sourceIndex, out var targetIndex, equalityComparer);

      if (length > 0)
      {
        var lcsv = new T[length];

        while (matrix[sourceIndex, targetIndex] != 0)
        {
          lcsv.InsertToCopy(0, source[sourceIndex - 1]); // Can also use target[targetIndex - 1].

          sourceIndex--;
          targetIndex--;
        }

        return lcsv;
      }

      return System.Array.Empty<T>();
    }
  }
}
