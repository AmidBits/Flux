//namespace Flux
//{
//  public static partial class SystemCollectionsGenericIEnumerableEm
//  {
//    /// <summary>Compute both the minimum and maximum element in a single pass, and return them as a 2-tuple. Uses the specified comparer.</summary>
//    public static bool GetNearestGreaterAndLessOrEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue referenceValue, out int indexOfLtMax, out int indexOfEq, out int indexOfGtMin)
//      where TValue : struct, System.IComparable<TValue>
//    {
//      using var e = source.GetEnumerator();

//      var ltCurrent = new TValue?();
//      var gtCurrent = new TValue?();

//      indexOfLtMax = -1;
//      indexOfEq = -1;
//      indexOfGtMin = -1;

//      var index = 0;

//      while (e.MoveNext())
//      {
//        var currentValue = valueSelector(e.Current);

//        switch (currentValue.CompareTo(referenceValue))
//        {
//          case int lo when lo < 0:
//            if (!ltCurrent.HasValue || currentValue.CompareTo(ltCurrent.Value) > 0)
//            {
//              indexOfLtMax = index;
//              ltCurrent = currentValue;
//            }
//            break;
//          case int hi when hi > 0:
//            if (!gtCurrent.HasValue || currentValue.CompareTo(gtCurrent.Value) < 0)
//            {
//              indexOfGtMin = index;
//              gtCurrent = currentValue;
//            }
//            break;
//          default:
//            indexOfEq = index;
//            break;
//        }

//        index++;
//      }

//      return indexOfLtMax != -1 || indexOfEq != -1 || indexOfGtMin != -1;
//    }
//    public static bool GetNearestGreaterAndLessOrEqual<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue referenceValue, System.Collections.Generic.IComparer<TValue> comparer)
//       where TValue : struct
//    {
//      using var e = source.GetEnumerator();

//      if (e.MoveNext())
//      {
//        var ltSource = e.Current;


//        var ltCurrent = new TValue?();
//        var gtCurrent = new TValue?();

//        var index = 0;

//        while (e.MoveNext())
//        {
//          var currentValue = valueSelector(e.Current);

//          switch (currentValue.CompareTo(referenceValue))
//          {
//            case int lo when lo < 0:
//              if (!ltCurrent.HasValue || currentValue.CompareTo(ltCurrent.Value) > 0)
//              {
//                indexOfLtMax = index;
//                ltCurrent = currentValue;
//              }
//              break;
//            case int hi when hi > 0:
//              if (!gtCurrent.HasValue || currentValue.CompareTo(gtCurrent.Value) < 0)
//              {
//                indexOfGtMin = index;
//                gtCurrent = currentValue;
//              }
//              break;
//            default:
//              indexOfEq = index;
//              break;
//          }

//          index++;
//        }

//        return indexOfLtMax != -1 || indexOfEq != -1 || indexOfGtMin != -1;
//      }
//    }
//  }
//}
