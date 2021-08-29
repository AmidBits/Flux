namespace Flux.Metrical
{
  /// <summary>Finding the shortest common supersequence (SCS) of two sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Shortest_common_supersequence_problem"/> 
  /// <seealso cref="http://rosettacode.org/wiki/Shortest_common_supersequence#C"/>
  public class ShortestCommonSupersequence<T>
    : AMetrical<T>, /*IFullMatrix<T>,*/ IMetricDistance<T>, IMetricLength<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
  {
    public ShortestCommonSupersequence()
      : base(System.Collections.Generic.EqualityComparer<T>.Default)
    { }
    public ShortestCommonSupersequence(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }

    /// <summary>This is the same routine as longest common subsequence (LCS). The spice of SCS happens in the GetList().</summary>
    public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var scsg = new int[source.Length + 1, target.Length + 1];

      for (int i = source.Length - 1; i >= 0; i--)
        scsg[i, 0] = 0;
      for (int j = target.Length - 1; j >= 0; j--)
        scsg[0, j] = 0;

      for (var i = 0; i < source.Length; i++)
        for (var j = 0; j < target.Length; j++)
          scsg[i + 1, j + 1] = EqualityComparer.Equals(source[i], target[j]) ? scsg[i, j] + 1 : System.Math.Max(scsg[i + 1, j], scsg[i, j + 1]);

      return scsg;
    }

    /// <summary>Returns the items comprising the shortest common super-sequence.</summary>
    public System.Collections.Generic.List<T> GetList(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var lcsl = Backtrack(GetFullMatrix(source, target), source, target, source.Length, target.Length);

      var si = 0;
      var ti = 0;
      var li = 0;

      while (li < lcsl.Count)
      {
        if (si < source.Length && !EqualityComparer.Equals(source[si], lcsl[li]))
        {
          lcsl.Insert(li++, source[si++]);
          continue;
        }
        else if (ti < target.Length && !EqualityComparer.Equals(target[ti], lcsl[li]))
        {
          lcsl.Insert(li++, target[ti++]);
          continue;
        }
        else
        {
          si++;
          ti++;
          li++;
        }
      }

      while (si < source.Length)
        lcsl.Insert(li++, source[si++]);
      while (ti < target.Length)
        lcsl.Insert(li++, target[ti++]);

      return lcsl;
    }

    public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => source.Length + target.Length - 2 * GetMetricLength(source, target);

    public int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetList(source, target).Count;

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
  var l = scs.GetList(a, b);
*/
