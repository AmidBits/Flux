﻿//using System.Collections.Generic;
//using System.Linq;

//namespace Flux
//{
//  internal static class StringExtensions
//  {
//    public static IEnumerable<char> ToEnumerable(this string @this)
//    {
//#if STRING_IS_ENUMERABLE
//            return @this;
//#else
//      if (@this == null) throw new System.ArgumentNullException(nameof(@this));

//      for (var i = 0; i < @this.Length; ++i)
//      {
//        yield return @this[i];
//      }
//#endif
//    }

//    public static string Join<T>(string separator, IEnumerable<T> values)
//    {
//#if STRING_JOIN_ENUMERABLE
//            return string.Join(separator, values);
//#else
//      return string.Join(separator, values.Select(v => v?.ToString() ?? string.Empty).ToArray());
//#endif
//    }
//  }
//}
