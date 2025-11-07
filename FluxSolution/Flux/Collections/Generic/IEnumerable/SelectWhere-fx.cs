//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    /// <summary>
//    /// <para>Yields a new sequence of elements from <paramref name="source"/> based on <paramref name="selector"/> and <paramref name="predicate"/>.</para>
//    /// </summary>
//    /// <typeparam name="TSource"></typeparam>
//    /// <typeparam name="TResult"></typeparam>
//    /// <param name="source"></param>
//    /// <param name="selector"></param>
//    /// <param name="predicate"></param>
//    /// <returns></returns>
//    public static System.Collections.Generic.IEnumerable<TResult> SelectWhere<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TResult> selector, System.Func<TSource, int, TResult, bool> predicate)
//    {
//      System.ArgumentNullException.ThrowIfNull(predicate);
//      System.ArgumentNullException.ThrowIfNull(selector);

//      var index = 0;

//      foreach (var item in source)
//        if (selector(item, index) is var select && predicate(item, index++, select))
//          yield return select;
//    }
//  }
//}
