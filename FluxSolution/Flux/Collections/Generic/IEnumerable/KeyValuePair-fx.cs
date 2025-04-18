namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>Flattens a sequence of objects into a sequence of key/value pairs based on the specified keySelector and valuesSelector.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Flatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
      => source.SelectMany(e => valuesSelector(e).Select(v => new System.Collections.Generic.KeyValuePair<TKey, TValue>(keySelector(e), v)));

    /// <summary>Unflattens a sequence of objects into a sequence of based on the specified keySelector and valuesSelector.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.List<TValue>>> Unflatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector)
      where TKey : System.IEquatable<TKey>
    {
      var list = source.ToList();

      return list.Select(t => keySelector(t)).Distinct().Select(k => new System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.List<TValue>>(k, list.Where(t => keySelector(t).Equals(k)).Select(t => valueSelector(t)).ToList()));
    }
  }
}
