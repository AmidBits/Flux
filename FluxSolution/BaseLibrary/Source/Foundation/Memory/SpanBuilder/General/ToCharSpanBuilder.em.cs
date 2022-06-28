//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static SpanBuilder<char> ToCharSpanBuilder(this ref SpanBuilder<System.Text.Rune> source)
//    {
//      var target = new SpanBuilder<char>();

//      for (var index = 0; index < source.Length; index++)
//        target.Append(source[index].ToString());

//      return target;
//    }
//  }
//}
