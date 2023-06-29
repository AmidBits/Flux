namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (T item, int index) FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowOnNull().GetEnumerator();

      for (var index = 0; e.MoveNext(); index++)
        if (predicate(e.Current, index))
          return (e.Current, index);

      return (value, -1);
    }
  }
}
