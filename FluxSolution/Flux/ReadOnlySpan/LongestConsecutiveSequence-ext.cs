namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.Collections.Generic.ISet<T> source)
      where T : System.Numerics.IBinaryInteger<T>
    {
      /// <summary>
      /// <para>Finds the longest consecutive sequence.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public int LongestConsecutiveSequenceLength()
      {
        /*
  for num in num_set:
  if num - 1 not in num_set:
  current_num = num
  current_streak = 1

  while current_num + 1 in num_set:
    current_num += 1
    current_streak += 1

  longest_streak = max(longest_streak, current_streak)
         */

        var longest_streak = 1;

        foreach (var num in source)
        {
          if (!source.Contains(num - T.One))
          {
            var current_num = num;
            var current_streak = 1;

            while (source.Contains(current_num + T.One))
            {
              current_num++;
              current_streak++;
            }

            longest_streak = int.Max(longest_streak, current_streak);
          }
        }

        return longest_streak;
      }
    }

    extension<TInteger>(System.ReadOnlySpan<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Finds the longest consecutive sequence.</para>
      /// </summary>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int LongestConsecutiveSequenceLength(System.Collections.Generic.IEqualityComparer<TInteger>? equalityComparer = null)
        => source.ToHashSet(equalityComparer).LongestConsecutiveSequenceLength();
    }
  }
}
