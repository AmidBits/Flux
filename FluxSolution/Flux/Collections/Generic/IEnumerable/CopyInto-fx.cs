//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    #region CopyInto

//    public static int CopyInto<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IList<T> target, int index, int count)
//    {
//      using var e = source.GetEnumerator();

//      var c = 0;

//      for (var i = index; e.MoveNext() && c < count; c++, i++)
//        target[i] = e.Current;

//      return c;
//    }

//    public static int CopyInto<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IList<T> target)
//      => source.CopyInto(target, 0, target.Count);

//    #endregion
//  }
//}
