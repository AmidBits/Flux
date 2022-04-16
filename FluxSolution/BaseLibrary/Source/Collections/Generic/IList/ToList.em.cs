namespace Flux
{
  public static partial class IListEm
  {
    /// <summary>Creates a new list from the specified array that is satisfying the specified predicate.</summary>
    public static System.Collections.Generic.List<TResult> ToList<TValue, TResult>(this System.Collections.Generic.IList<TValue> source, System.Func<TValue, bool> predicate, System.Func<TValue, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var target = new System.Collections.Generic.List<TResult>();
      foreach (var item in source)
        if (predicate(item))
          target.Add(resultSelector(item));
      return target;
    }

    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.Collections.Generic.IList<T> source, int offset, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (offset < 0 || offset >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new System.Collections.Generic.List<T>(count);
      while (count-- > 0)
        target.Add(source[offset++]);
      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.Collections.Generic.IList<T> source, int offset)
      => ToList(source, offset, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - offset);
  }
}
