//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para>Creates a new sequence of "rows", i.e. arrays containing fields. The first row contains field names, the second row contains field types and then come the rows of field values.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <param name="valueSelector"></param>
//    /// <returns></returns>
//    public static System.Collections.Generic.IEnumerable<object[]> GetData<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<object?, int, object> valueSelector)
//      where TSource : notnull
//    {
//      var e = source.GetEnumerator();

//      if (e.MoveNext())
//      {
//        yield return Fx.GetPropertyInfos(e.Current).Select(pi => pi.Name).ToArray();
//        yield return Fx.GetPropertyInfos(e.Current).Select(pi => pi.PropertyType).ToArray();

//        do
//        {
//          yield return Fx.GetPropertyInfos(e.Current).Select((pi, i) => valueSelector(pi.GetValue(e.Current), i)).ToArray();
//        }
//        while (e.MoveNext());
//      }
//    }
//  }
//}
