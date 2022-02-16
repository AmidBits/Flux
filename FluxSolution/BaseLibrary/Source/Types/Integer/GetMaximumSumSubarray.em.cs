namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Find the sum of contiguous subarray within a one-dimensional array of numbers which has the largest sum.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
    public static int GetMaximumSumSubarray(this System.Collections.Generic.IList<int> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceCount = source.Count;

      var bestSum = int.MinValue;
      var currentSum = 0;

      for (var index = 0; index < sourceCount; index++)
      {
        var value = source[index];

        currentSum = System.Math.Max(value, currentSum + value);
        bestSum = System.Math.Max(bestSum, currentSum);
      }

      return bestSum;
    }

    /// <summary>Find the sum of contiguous subarray within a one-dimensional array of numbers which has the largest sum, and also return the start index and the number of elements making up the sum.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
    public static int GetMaximumSumSubarray(this System.Collections.Generic.IList<int> source, out int startIndex, out int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceCount = source.Count;

      var bestSum = int.MinValue;
      var currentSum = 0;

      startIndex = -1;
      count = 0;

      for (var index = 0; index < sourceCount; index++)
      {
        var value = source[index];

        if (currentSum <= 0)
        {
          startIndex = index;
          currentSum = value;
        }
        else
          currentSum += value;

        if (currentSum > bestSum)
        {
          bestSum = currentSum;
          count = index + 1 - startIndex;
        }
      }

      return bestSum;
    }
  }
}
