namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    /// <summary>Returns the first element in the sequence that satisfies the predicate, or if none is found, the specified value.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static (T item, int index) FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var index = 0;

      foreach (var item in source.ThrowOnNull())
      {
        if (predicate(item, index))
          return (item, index);

        index++;
      }

      return (value, -1);
    }
  }
}
