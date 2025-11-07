//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> SelectManyMany<TSource, TKey, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, System.Collections.Generic.IEnumerable<TKey>> keysSelector, System.Func<TSource, System.Collections.Generic.IEnumerable<TValue>> valuesSelector)
//    {
//      foreach (var item in source)
//        foreach (var kvp in keysSelector(item).SelectMany(key => valuesSelector(item), (key, value) => System.Collections.Generic.KeyValuePair.Create(key, value)))
//          yield return kvp;
//    }
//  }
//}
