namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Run-length encodes <paramref name="source"/> by converting consecutive instances of the same element into a <c>KeyValuePair{T,int}</c> representing the item and its occurrence count. Uses the specified <paramref name="equalityComparer"/> (default if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> RunLengthEncode<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var previous = e.Current;

        var count = 1;

        while (e.MoveNext())
        {
          if (equalityComparer.Equals(previous, e.Current))
            count++;
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
