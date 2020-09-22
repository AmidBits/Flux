namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns whether the sequence has at least the specified count (number of elements) matching the predicate.</summary>
    public static bool CountAtLeast<T>(this System.Collections.Generic.IEnumerable<T> source, int count, System.Func<T, int, bool> predicate)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      switch (source)
      {
        case null:
          throw new System.ArgumentNullException(nameof(source));
        case System.Collections.Generic.ICollection<T> ict:
          return ict.Count >= count;
        case System.Collections.ICollection ic:
          return ic.Count >= count;
        default:
          using (var e = source.GetEnumerator())
          {
            int counter = 0, index = 0;
            while (counter < count && e.MoveNext())
              if (predicate(e.Current, index++))
                counter++;
            return counter >= count;
          }
      }
    }
    /// <summary>Returns whether the sequence has at least the specified count (number of elements).</summary>
    public static bool CountAtLeast<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
      => CountAtLeast(source, count, (e, i) => true);

    /// <summary>Returns whether the sequence has at most the specified count (number of elements) matching the predicate.</summary>
    public static bool CountAtMost<T>(this System.Collections.Generic.IEnumerable<T> source, int count, System.Func<T, int, bool> predicate)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      switch (source)
      {
        case null:
          throw new System.ArgumentNullException(nameof(source));
        case System.Collections.Generic.ICollection<T> ict:
          return ict.Count <= count;
        case System.Collections.ICollection ic:
          return ic.Count <= count;
        default:
          using (var e = source.GetEnumerator())
          {
            int counter = 0, index = 0;
            while (counter <= count && e.MoveNext())
              if (predicate(e.Current, index++))
                counter++;
            return counter <= count;
          }
      }
    }
    /// <summary>Returns whether the sequence has at most the specified count (number of elements).</summary>
    public static bool CountAtMost<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
      => CountAtMost(source, count, (e, i) => true);
  }
}
