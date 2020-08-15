namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Run-length encodes a sequence by converting consecutive instances of the same element into a <c>KeyValuePair{T,int}</c> representing the item and its occurrence count. This overload uses a custom equality comparer to identify equivalent items.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> RunLengthEncode<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var e = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

      if (e.MoveNext())
      {
        var previous = e.Current;

        var count = 1;

        while (e.MoveNext())
        {
          if (comparer.Equals(previous, e.Current))
          {
            count++;
          }
          else
          {
            yield return new System.Collections.Generic.KeyValuePair<T, int>(previous, count);

            previous = e.Current;

            count = 1;
          }
        }

        yield return new System.Collections.Generic.KeyValuePair<T, int>(previous, count);
      }
    }
  }
}
