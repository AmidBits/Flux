namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the last element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (T item, int index) LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      using var e = source.ThrowOnNull().GetEnumerator();

      var result = (value, -1);

      for (var index = 0; e.MoveNext(); index++)
        if (predicate(e.Current, index))
          result = (e.Current, index);

      return result;
    }
  }
}
