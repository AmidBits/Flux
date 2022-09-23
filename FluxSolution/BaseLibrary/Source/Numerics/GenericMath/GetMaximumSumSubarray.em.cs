#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Find the sum of contiguous subarray within a one-dimensional array of numbers which has the largest sum.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
    public static TSelf GetMaximumSumSubarray<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source, out int startIndex, out int count)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var bestSum = -TSelf.One;
      var bestStart = -1;
      var bestEnd = -1;

      var currentSum = -TSelf.One;
      var currentStart = 0;
      var currentEnd = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        if (currentSum <= TSelf.Zero) // Start a new sequence at the current element.
        {
          currentStart = currentEnd;
          currentSum = e.Current;
        }
        else // Extend the existing sequence with the current element.
          currentSum += e.Current;

        if (currentSum > bestSum) // the +1 is to make 'best_end' match Python's slice convention (endpoint excluded).
        {
          bestSum = currentSum;
          bestStart = currentStart;
          bestEnd = currentEnd + 1;
        }

        currentEnd++;
      }

      startIndex = bestStart;
      count = bestEnd - bestStart;

      return bestSum;
    }
  }
}
#endif
