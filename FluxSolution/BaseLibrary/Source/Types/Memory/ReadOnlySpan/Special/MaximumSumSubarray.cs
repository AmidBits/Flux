namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>
    /// <para>Find the maximum sum subarray in <paramref name="source"/>, at <paramref name="startIndex"/> and <paramref name="count"/> values.</para>
    /// <see href="https://en.wikipedia.org/wiki/Maximum_subarray_problem"/>
    /// </summary>
    /// <remarks>In computer science, the maximum sum subarray problem, also known as the maximum segment sum problem, is the task of finding a contiguous subarray with the largest sum, within a given one-dimensional array of numbers.</remarks>
    public static TValue MaximumSumSubarray<TValue>(this System.ReadOnlySpan<TValue> source, out int startIndex, out int count)
      where TValue : System.Numerics.INumber<TValue>
    {
      var bestSum = -TValue.One;
      var bestStartIndex = -1;
      var bestEndIndex = -1;

      var currentSum = -TValue.One;
      var currentStartIndex = 0;
      var currentEndIndex = 0;

      for (var index = 0; index < source.Length; index++)
      {
        if (currentSum <= TValue.Zero) // Start a new sequence at the current element.
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

      startIndex = bestStartIndex;
      count = bestEndIndex - bestStartIndex + 1;

      return bestSum;
    }
  }
}
