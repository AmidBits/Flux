//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    #region SingleOrValue

//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="source"></param>
//    /// <param name="value"></param>
//    /// <param name="predicate"></param>
//    /// <returns></returns>
//    /// <exception cref="System.InvalidOperationException"></exception>
//    public static (T Item, int Index) SingleOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Func<T, int, bool>? predicate = null)
//    {
//      predicate ??= (e, i) => true;

//      var item = value;
//      var index = -1;

//      foreach (var element in source)
//      {
//        if (predicate(element, index))
//        {
//          index++;
//          item = element;

//          if (index > 0) throw new System.InvalidOperationException("The sequence has more than one element.");
//        }
//      }

//      return (item, index);
//    }

//    #endregion
//  }
//}
