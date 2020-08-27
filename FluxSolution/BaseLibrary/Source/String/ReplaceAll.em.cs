//using System.Linq;

//namespace Flux
//{
//  public static partial class XtendString
//  {
//    /// <summary>Replace all characters using the specified replacement selector function. If the replacement selector returns null, no replacement is made.</summary>
//    public static string ReplaceAll(this string source, System.Func<char, string?> replacementSelector)
//    {
//      var output = new System.Text.StringBuilder();

//      foreach (var c in source)
//      {
//        if (replacementSelector(c) is var r && r is null)
//        {
//          output.Append(c);
//        }
//        else
//        {
//          output.Append(r);
//        }
//      }

//      return output.ToString();
//    }
//    /// <summary>Replace all characters using the specified replacement selector function.</summary>
//    public static string ReplaceAll(this string source, System.Func<char, char> replacementSelector)
//    {
//      var buffer = source.ToCharArray();

//      for (var index = 0; index < source.Length; index++)
//      {
//        buffer[index] = replacementSelector(buffer[index]);
//      }

//      return new string(buffer, 0, buffer.Length);
//    }

//    /// <summary>Replace all characters satisfying the predicate with the specified character.</summary>
//    /// <example>"".ReplaceAll(replacement, char.IsWhiteSpace);</example>
//    public static string ReplaceAll(this string source, char replacement, System.Func<char, bool> predicate)
//    {
//      var buffer = source.ToCharArray();

//      for (var index = 0; index < source.Length; index++)
//        if (predicate(source[index]))
//          buffer[index] = replacement;

//      return new string(buffer);
//    }
//    /// <summary>Replace all specified characters with the specified character.</summary>
//    public static string ReplaceAll(this string source, char replacement, params char[] replace)
//      => source.ReplaceAll(replacement, replace.Contains);
//  }
//}
