namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Find the sum of subarray within a one-dimensional array of numbers which adds up to the largest sum.</summary>
    /// <remarks>(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }).GetMaximumSumSubarray(out var startIndex, out var count); // Resulting in { 4, -1, 2, 1 } with sum 6 (startIndex = 3 and count = 4).</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
    public static TSelf GetMaximumSumSubarray<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> collection, out int startIndex, out int count)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (collection is null) throw new System.ArgumentNullException(nameof(collection));

      var bestSum = -TSelf.One;
      var bestStart = -1;
      var bestEnd = -1;

      var currentSum = -TSelf.One;
      var currentStart = 0;
      var currentEnd = 0;

      using var e = collection.GetEnumerator();

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
