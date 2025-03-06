namespace Flux
{
  public static partial class Em
  {
    //    /// <summary>Given an array of positive integers. All numbers occur even number of times except one number which occurs odd number of times. Find the number in O(n) time & constant space.</summary>
    //    public static int GetOddOccurrence(System.Collections.Generic.IList<int> source)
    //    {
    //      if (source is null) throw new System.ArgumentNullException(nameof(source));

    //      var sourceCount = source.Count;

    //      for (var i = 0; i < sourceCount; i++)
    //      {
    //        var sourceValue = source[i];

    //        var count = 0;

    //        for (var j = 0; j < sourceCount; j++)
    //          if (sourceValue == source[j])
    //            count++;

    //        if (count % 2 != 0)
    //          return sourceValue;
    //      }

    //      return -1;
    //    }

    //    public static System.Collections.Generic.IEnumerable<int> SequenceFindMissings(System.Collections.Generic.IEnumerable<int> sequence)
    //      => sequence.Zip(sequence.Skip(1), (a, b) => System.Linq.Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);

    //    public static bool IsSequenceBroken(System.Collections.Generic.IEnumerable<int> sequence)
    //      => sequence.Zip(sequence.Skip(1), (a, b) => b - a).Any(gap => gap != 1);

    //    public static System.Collections.Generic.IEnumerable<int> FindMissingIntegers(System.Collections.Generic.IEnumerable<int> source)
    //    {
    //      if (source is null) throw new System.ArgumentNullException(nameof(source));

    //      return source.Zip(source.Skip(1), (a, b) => System.Linq.Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);
    //    }
    public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Count)> GetMissingRanges<TInteger>(this System.Collections.Generic.IEnumerable<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.PartitionTuple2(false, (a, b, i) => (Number: a + TInteger.One, Count: (b - a) - TInteger.One)).Where(t2 => t2.Count > TInteger.One);
    }
  }
}
