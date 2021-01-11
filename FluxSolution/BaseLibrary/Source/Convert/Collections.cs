//namespace Flux
//{
//  /// <summary></summary>
//  public static partial class Convert
//  {
//    public static System.Collections.Generic.IEnumerable<object[]> ToTypedObjects<T>(this System.Collections.Generic.IEnumerable<T[]> source, System.Func<T, int, object> typeConverter)
//    {
//      if (source is null) throw new System.ArgumentNullException(nameof(source));
//      if (typeConverter is null) throw new System.ArgumentNullException(nameof(typeConverter));

//      foreach (var sa in source)
//      {
//        var ta = new object[sa.Length];
//        for (var index = 0; index < sa.Length; index++)
//          ta[index] = typeConverter(sa[index], index);
//        yield return ta;
//      }
//    }
//  }
//}
