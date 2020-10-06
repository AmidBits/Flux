using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Given an array of positive integers. All numbers occur even number of times except one number which occurs odd number of times. Find the number in O(n) time & constant space.</summary>
    public static int GetOddOccurrence(this System.Collections.Generic.IList<int> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var i = 0; i < source.Count; i++)
      {
        var count = 0;

        for (int j = 0; j < source.Count; j++)
          if (source[i] == source[j])
            count++;

        if (count % 2 != 0)
          return source[i];
      }

      return -1;
    }

    /// <summary>Given an array of integers, and a number sum, find the number of pairs of integers in the array whose sum is equal to sum.</summary>
    public static int GetPairsCount(this System.Collections.Generic.IList<int> source, int sum)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var count = 0;

      for (var i = source.Count - 1; i >= 0; i--)
        for (var j = i - 1; j >= 0; j--)
          if (source[i] + source[j] == sum)
            count++;

      return count;
    }

    /// <summary>Find the sum of contiguous subarray within a one-dimensional array of numbers which has the largest sum.</summary>
    public static int MaxSubArraySum(this System.Collections.Generic.IList<int> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var size = source.Count;

      var max_so_far = int.MinValue;
      var max_ending_here = 0;

      for (var index = 0; index < size; index++)
      {
        max_ending_here += source[index];

        if (max_so_far < max_ending_here)
          max_so_far = max_ending_here;

        if (max_ending_here < 0)
          max_ending_here = 0;
      }

      return max_so_far;
    }

    public static System.Collections.Generic.IEnumerable<int> SequenceFindMissings(this System.Collections.Generic.IEnumerable<int> sequence)
      => sequence.Zip(sequence.Skip(1), (a, b) => Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);

    public static bool IsSequenceBroken(this System.Collections.Generic.IEnumerable<int> sequence)
      => sequence.Zip(sequence.Skip(1), (a, b) => b - a).Any(gap => gap != 1);

    public static System.Collections.Generic.IEnumerable<int> FindMissingIntegers(this System.Collections.Generic.IEnumerable<int> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return source.Zip(source.Skip(1), (a, b) => Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);
    }
  }
}
