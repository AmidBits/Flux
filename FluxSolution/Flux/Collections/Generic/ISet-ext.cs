namespace Flux
{
  public static partial class LongestConsecutiveSequence
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
  }

  public static class ISets
  {
    extension<T>(System.Collections.Generic.ISet<T> source)
    {
      /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public (int unfoundCount, int uniqueCount) SetCounts(System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound)
      {
        var unfoundCount = 0;

        var hsUnique = new System.Collections.Generic.HashSet<T>();

        using var e = target.ThrowOnNull().GetEnumerator();

        while (e.MoveNext())
        {
          if (e.Current is var current && source.Contains(current))
            hsUnique.Add(current);
          else
          {
            unfoundCount++;

            if (returnIfUnfound)
              break;
          }
        }

        return (unfoundCount, hsUnique.Count);
      }

      /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.List{T}"/>.</summary>
      public void AddRange(System.Collections.Generic.IEnumerable<T> other, out int count)
      {
        count = 0;

        foreach (var item in other)
          if (source.Add(item))
            count++;
      }

      /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.List{T}"/>.</summary>
      public void AddRange(System.Collections.Generic.IEnumerable<T> other)
        => source.AddRange(other, out var _);

      /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.ISet{T}"/> and returns the number of elements that were successfully added.</summary>
      public void AddSpan(System.ReadOnlySpan<T> other, out int count)
      {
        count = 0;

        for (var index = 0; index < other.Length; index++)
          if (source.Add(other[index]))
            count++;
      }

      /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.ISet{T}"/> and returns the number of elements that were successfully added.</summary>
      public void AddSpan(System.ReadOnlySpan<T> other) => source.AddSpan(other, out var _);

      public void RemoveSpan(System.ReadOnlySpan<T> other, out int count)
      {
        count = 0;

        for (var index = 0; index < other.Length; index++)
          if (source.Remove(other[index]))
            count++;
      }

      public void RemoveSpan(System.ReadOnlySpan<T> other) => source.RemoveSpan(other, out var _);

      ///// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
      ///// <exception cref="System.ArgumentNullException"/>
      //public static (int unfoundCount, int uniqueCount) SetCounts(System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound)
      //{
      //  var unfoundCount = 0;

      //  var hsUnique = new System.Collections.Generic.HashSet<T>();

      //  using var e = target.ThrowOnNull().GetEnumerator();

      //  while (e.MoveNext())
      //  {
      //    if (e.Current is var current && source.Contains(current))
      //      hsUnique.Add(current);
      //    else
      //    {
      //      unfoundCount++;

      //      if (returnIfUnfound)
      //        break;
      //    }
      //  }

      //  return (unfoundCount, hsUnique.Count);
      //}

      /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.IEnumerable<T> SymmetricDifference(System.Collections.Generic.IEnumerable<T> target)
      {
        if (ReferenceEquals(source, target))
          return []; // A symmetric difference of a set with itself is an empty set.

        if (!source.Any())
          return target; // If source is empty, target is the result.

        var ths = new System.Collections.Generic.HashSet<T>(target);

        if (ths.Count == 0)
          return source; // If target is empty, source is the result.

        var ihs = new System.Collections.Generic.HashSet<T>(source.Intersect(ths));

        return source.Except(ihs).Concat(ths.Except(ihs));
      }
    }
  }
}