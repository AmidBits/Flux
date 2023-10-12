namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the last element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (T item, int index) LastOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var result = (value, -1);

      var index = 0;

      foreach (var item in source.ThrowOnNull())
      {
        if (predicate(item, index))
          result = (item, index);

        index++;
      }

      return result;
    }
  }
}
