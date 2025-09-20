namespace Flux
{
  public static class MaximumSumSubarrayAlgorithm
  {
    extension<TNumber>(System.ReadOnlySpan<TNumber> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Find the maximum sum subarray in a span of values.</para>
      /// <see href="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
      /// </summary>
      /// <returns></returns>
      /// <remarks>In computer science, the maximum sum subarray problem, also known as the maximum segment sum problem, is the task of finding a contiguous subarray with the largest sum, within a given one-dimensional array of numbers.</remarks>
      public (int Index, int Count, TNumber Sum) MaximumSumSubarray()
      {
        var bestSum = -TNumber.One;
        var bestStartIndex = -1;
        var bestEndIndex = -1;

        var currentSum = -TNumber.One;
        var currentStartIndex = 0;
        var currentEndIndex = 0;

        for (var index = 0; index < source.Length; index++)
        {
          if (currentSum <= TNumber.Zero) // Start a new sequence at the current element.
          {
            currentStartIndex = currentEndIndex;
            currentSum = source[index];
          }
          else // Extend the existing sequence with the current element.
            currentSum += source[index];

          if (currentSum > bestSum)
          {
            bestSum = currentSum;
            bestStartIndex = currentStartIndex;
            bestEndIndex = currentEndIndex;
          }

          currentEndIndex++;
        }

        return (
          bestStartIndex,
          bestEndIndex - bestStartIndex + 1,
          bestSum
        );
      }
    }
  }
}
