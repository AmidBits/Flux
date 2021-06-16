using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Flattens a sequence of objects into a sequence of key/value pairs based on the specified keySelector and valuesSelector.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Flatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
      => source.SelectMany(t => valuesSelector(t).Select(v => new System.Collections.Generic.KeyValuePair<TKey, TValue>(keySelector(t), v)));

    /// <summary>Unflattens a sequence of objects into a sequence of based on the specified keySelector and valuesSelector.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.IList<TValue>>> Unflatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector)
      where TKey : System.IEquatable<TKey>
    {
      var list = source.ToList();

      return list.Select(t => keySelector(t)).Distinct().Select(k => new System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.IList<TValue>>(k, list.Where(t => keySelector(t).Equals(k)).Select(t => valueSelector(t)).ToList()));
    }
  }
}
