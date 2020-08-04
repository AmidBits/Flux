namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns whether the sequence has at most the specified count (number of elements) matching the predicate.</summary>
    public static bool CountAtMost<T>(this System.Collections.Generic.IEnumerable<T> source, int count, System.Func<T, int, bool>? predicate = null)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      switch (source)
      {
        case null: throw new System.ArgumentNullException(nameof(source));
        case System.Collections.Generic.ICollection<T> ict: return ict.Count <= count;
        case System.Collections.ICollection ic: return ic.Count <= count;
        default:
          using (var e = source.GetEnumerator())
          {
            int counter = 0, index = 0;
            while (counter <= count && e.MoveNext())
              if (predicate?.Invoke(e.Current, index++) ?? true)
                counter++;
            return counter <= count;
          }
      }
    }
  }
}
