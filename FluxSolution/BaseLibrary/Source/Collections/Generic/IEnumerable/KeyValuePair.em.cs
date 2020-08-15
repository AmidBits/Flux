using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Flattens a sequence of objects into a sequence of key/value pairs based on the specified keySelector and valuesSelector.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Flatten<T, TKey, TValue>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, TKey> keySelector, System.Func<T, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
      => source.SelectMany(t => valuesSelector(t).Select(v => new System.Collections.Generic.KeyValuePair<TKey, TValue>(keySelector(t), v)));

    /// <summary>Unflattens a sequence of objects into a sequence of key/value pairs based on the specified keySelector and valuesSelector.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.IList<TValue>>> Unflatten<T, TKey, TValue>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, TKey> keySelector, System.Func<T, TValue> valueSelector)
      where TKey : System.IEquatable<TKey>
    {
      var list = source.ToList();

      return list.Select(t => keySelector(t)).Distinct().Select(k => new System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.IList<TValue>>(k, list.Where(t => keySelector(t).Equals(k)).Select(t => valueSelector(t)).ToList()));
    }
  }
}
