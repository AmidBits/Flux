namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Run-length encodes a sequence by converting consecutive instances of the same element into a <c>KeyValuePair{T,int}</c> representing the item and its occurrence count. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> RunLengthEncode<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var previous = e.Current;

        var count = 1;

        while (e.MoveNext())
        {
          if (equalityComparer.Equals(previous, e.Current))
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
    /// <summary>Run-length encodes a sequence by converting consecutive instances of the same element into a <c>KeyValuePair{T,int}</c> representing the item and its occurrence count. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> RunLengthEncode<T>(this System.Collections.Generic.IEnumerable<T> source)
      => RunLengthEncode(source, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
