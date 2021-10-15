//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static bool EqualTo(this System.Collections.IList source, System.Collections.IList target)
//    {
//      if (System.Object.ReferenceEquals(source, target))
//        return true;

//      if (source is null || target is null)
//        return source is null && target is null;

//      if (source.Count != target.Count)
//        return false;

//      for (var index = source.Count - 1; index >= 0; index--)
//      {
//        var sourceValue = source[index];
//        var targetValue = target[index];

//        if (System.Object.ReferenceEquals(sourceValue, targetValue))
//          continue;

//        if (!sourceValue.EqualsEx(targetValue))
//          return false;
//      }

//      return true;
//    }
//  }
//}
