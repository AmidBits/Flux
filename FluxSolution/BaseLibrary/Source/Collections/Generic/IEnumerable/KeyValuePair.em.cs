namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Flattens a sequence of objects into a sequence of key/value pairs based on the specified keySelector and valuesSelector.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> Flatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
      => System.Linq.Enumerable.SelectMany(source, t => System.Linq.Enumerable.Select(valuesSelector(t), v => new System.Collections.Generic.KeyValuePair<TKey, TValue>(keySelector(t), v)));

    /// <summary>Unflattens a sequence of objects into a sequence of based on the specified keySelector and valuesSelector.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.List<TValue>>> Unflatten<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TValue> valueSelector)
      where TKey : System.IEquatable<TKey>
    {
      var list = System.Linq.Enumerable.ToList(source);

      return System.Linq.Enumerable.Select(System.Linq.Enumerable.Distinct(System.Linq.Enumerable.Select(list, t => keySelector(t))), k => new System.Collections.Generic.KeyValuePair<TKey, System.Collections.Generic.List<TValue>>(k, System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(System.Linq.Enumerable.Where(list, t => keySelector(t).Equals(k)), t => valueSelector(t)))));
    }
  }
}
