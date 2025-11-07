//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    /// <summary>
//    /// <para>Returns the a tuple with the first element and index from <paramref name="source"/> that satisfies the <paramref name="predicate"/>, otherwise <paramref name="value"/> and index = -1.</para>
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="source"></param>
//    /// <param name="value"></param>
//    /// <param name="predicate">If null then predicate is ignored.</param>
//    /// <returns></returns>
//    public static (T Item, int Index) FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
//    {
//      predicate ??= (e, i) => true;

//      var index = 0;

//      foreach (var element in source)
//        if (predicate(element, index)) return (element, index);
//        else index++;

//      return (value, -1);
//    }

//    public static (T Item, int Index) FirstOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
//      => FirstOrValue(source, value, (e, i) => i == 0);
//  }
//}
