//namespace Flux
//{
//  public static partial class SpanBuilderExtensionMethods
//  {
//    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
//    public static SpanBuilder<char> TrimLeft(ref this SpanBuilder<char> source, char separator = ' ')
//    {
//      for (var index = 0; index < source.Length; index++)
//        if (source[index] == separator)
//          source.Remove(index, 1);
//        else break;

//      return source;
//    }

//    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
//    public static SpanBuilder<char> TrimRight(ref this SpanBuilder<char> source, char separator = ' ')
//    {
//      for (var index = source.Length - 1; index >= 0; index--)
//        if (source[index] == separator)
//          source.Remove(index, 1);
//        else break;

//      return source;
//    }
//  }
//}
