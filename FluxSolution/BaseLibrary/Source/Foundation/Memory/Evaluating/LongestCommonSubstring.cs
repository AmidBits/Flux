namespace Flux.Metrical
{
  /// <summary>Finding the longest consecutive sequence of elements common to two or more sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
  /// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
  public sealed class LongestCommonSubstring<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public LongestCommonSubstring(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public LongestCommonSubstring()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    [System.Diagnostics.Contracts.Pure]
    private int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int length, out int sourceMaxIndex, out int targetMaxIndex)
    {
      var sourceLength = source.Length;
      var targetLength = target.Length;

      var lcsg = new int[sourceLength + 1, targetLength + 1];

      length = 0;

      sourceMaxIndex = 0;
      targetMaxIndex = 0;

      for (var si = 0; si <= sourceLength; si++)
      {
        for (var ti = 0; ti <= targetLength; ti++)
        {
          if (si > 0 && ti > 0 && EqualityComparer.Equals(source[si - 1], target[ti - 1]))
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
    [System.Diagnostics.Contracts.Pure]
    public int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetMatrix(source, target, out var _, out var _, out var _);

    [System.Diagnostics.Contracts.Pure]
    public T[] GetSubstring(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int[,] matrix)
    {
      matrix = GetMatrix(source, target, out var length, out var sourceIndex, out var targetIndex);

      var lcs = new T[length];

      if (length > 0)
      {
        while (matrix[sourceIndex, targetIndex] != 0)
        {
          lcs.Insert(0, source[sourceIndex - 1]); // Can also use target[targetIndex - 1].

          sourceIndex--;
          targetIndex--;
        }
      }

      return lcs;
    }

    [System.Diagnostics.Contracts.Pure]
    public int GetLengthMeasure(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var maxLength = 0;

      var v1 = new int[target.Length + 1];
      var v0 = new int[target.Length + 1];

      for (var i = source.Length - 1; i >= 0; i--)
      {
        (v0, v1) = (v1, v0);
        
        for (var j = target.Length - 1; j >= 0; j--)
        {
          if (EqualityComparer.Equals(source[i], target[j]))
          {
            v0[j] = v1[j + 1] + 1;

            maxLength = System.Math.Max(maxLength, v0[j]);
          }
          else
          {
            v0[j] = 0;
          }
        }
      }

      return maxLength;
    }
  }
}