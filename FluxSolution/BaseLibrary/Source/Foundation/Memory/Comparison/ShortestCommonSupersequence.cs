using System.Linq;

namespace Flux.Metrical
{
  /// <summary>Finding the shortest common supersequence (SCS) of two sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Shortest_common_supersequence_problem"/> 
  /// <seealso cref="http://rosettacode.org/wiki/Shortest_common_supersequence#C"/>
  /// <see cref="https://www.techiedelight.com/shortest-common-supersequence-finding-scs/"/>
  public sealed class ShortestCommonSupersequence<T>
    : IEditDistance<T>, IMetricLengthEquatable<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public ShortestCommonSupersequence(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public ShortestCommonSupersequence()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    /// <summary>This is the same routine as longest common subsequence (LCS). The spice of SCS happens in the GetList().</summary>
    public int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var sourceLength = source.Length;
      var targetLength = target.Length;

      var scsg = new int[sourceLength + 1, targetLength + 1];

      for (int si = sourceLength - 1; si >= 0; si--)
        scsg[si, 0] = si;
      for (int ti = targetLength - 1; ti >= 0; ti--)
        scsg[0, ti] = ti;

      for (var si = 0; si < sourceLength; si++)
        for (var ti = 0; ti < targetLength; ti++)
          scsg[si + 1, ti + 1] = EqualityComparer.Equals(source[si], target[ti]) ? scsg[si, ti] + 1 : System.Math.Min(scsg[si, ti + 1], scsg[si + 1, ti]) + 1;

      return scsg;
    }

    public System.Collections.Generic.IList<T> GetSupersequence(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix)
    {
      matrix = GetMatrix(source, target);

      return GetSupersequence(matrix, source, target, source.Length, target.Length);

      System.Collections.Generic.IList<T> GetSupersequence(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int si, int ti)
      {
        if (si == 0) // If the end of the first string is reached, return the second string.
          return target[..ti].ToArray().ToList();
        else if (ti == 0) // If the end of the second string is reached, return the first string.
          return source[..si].ToArray().ToList();

        if (EqualityComparer.Equals(source[si - 1], target[ti - 1])) // If the last character of si and ti matches, include it and recur to find SCS of substring.
        {
          var list = GetSupersequence(matrix, source, target, si - 1, ti - 1);
          list.Add(source[si - 1]);
          return list;
        }
        else
        {
          if (matrix[si - 1, ti] <= matrix[si, ti - 1]) // If the top cell has a value less or equal to that in the left cell, then include the current source element and find SCS of substring less the one added.
          {
            var list = GetSupersequence(matrix, source, target, si - 1, ti);
            list.Add(source[si - 1]);
            return list;
          }
          else // If the left cell has a value greater than that in the top cell, then include the current target element find SCS of substring less the one added.
          {
            var list = GetSupersequence(matrix, source, target, si, ti - 1);
            list.Add(target[ti - 1]);
            return list;
          }
        }
      }
    }

    public int GetEditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var length = source.Length + target.Length;

      return length - 2 * (length - GetMetricLength(source, target));
    }

    public int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetMatrix(source, target)[source.Length, target.Length];

    public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => (double)GetMetricLength(source, target) / (double)System.Math.Max(source.Length, target.Length);

    public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1.0 - GetSimpleMatchingCoefficient(source, target);
  }
}

/*
  var scs = new Flux.Metrical.ShortestCommonSupersequence<char>();

  var a = @"abcbdab";
  var b = @"bdcaba";

  var fm = scs.GetFullMatrix(a, b);
  var l = scs.GetList(a, b); // Now contains: "abdcabdab"
*/
