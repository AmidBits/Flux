namespace Flux
{
  public static partial class Sets
  {
    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.List{T}"/>.</summary>
    public static void AddRange<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> other, out int count)
    {
      count = 0;

      foreach (var item in other)
        if (source.Add(item))
          count++;
    }

    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.List{T}"/>.</summary>
    public static void AddRange<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> other)
      => source.AddRange(other, out var _);

    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.ISet{T}"/> and returns the number of elements that were successfully added.</summary>
    public static void AddSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other, out int count)
    {
      count = 0;

      for (var index = 0; index < other.Length; index++)
        if (source.Add(other[index]))
          count++;
    }

    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.ISet{T}"/> and returns the number of elements that were successfully added.</summary>
    public static void AddSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other) => source.AddSpan(other, out var _);

    public static void RemoveSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other, out int count)
    {
      count = 0;

      for (var index = 0; index < other.Length; index++)
        if (source.Remove(other[index]))
          count++;
    }

    public static void RemoveSpan<T>(this System.Collections.Generic.ISet<T> source, System.ReadOnlySpan<T> other) => source.RemoveSpan(other, out var _);

    /// <summary>Returns the number of unfound (not found) and the number of unique elements. Optionally the function returns early if there are unfound elements. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (int unfoundCount, int uniqueCount) SetCounts<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target, bool returnIfUnfound)
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

    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> SymmetricDifference<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
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
