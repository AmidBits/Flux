namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new list from the specified array that is satisfying the specified predicate.</summary>
    public static System.Collections.Generic.List<TResult> ToList<TValue, TResult>(this System.ReadOnlySpan<TValue> source, System.Func<TValue, int, bool> predicate, System.Func<TValue, int, TResult> resultSelector)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var target = new System.Collections.Generic.List<TResult>();

      for (var index = 0; index < source.Length; index++)
        if (source[index] is var item && predicate(item, index))
          target.Add(resultSelector(item, index));

      return target;
    }

    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, int offset, int count)
    {
      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new System.Collections.Generic.List<T>(count);

      while (count-- > 0)
        target.Add(source[offset++]);

      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, int offset)
      => ToList(source, offset, source.Length - offset);
    /// <summary>Creates a new list from the readonlyspan.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source)
      => ToList(source, 0, source.Length);
  }
}
