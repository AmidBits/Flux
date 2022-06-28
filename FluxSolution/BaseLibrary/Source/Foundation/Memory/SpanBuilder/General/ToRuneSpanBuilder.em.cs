//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static SpanBuilder<System.Text.Rune> ToRuneSpanBuilder(this ref SpanBuilder<char> source)
//    {
//      var target = new SpanBuilder<System.Text.Rune>();

//      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
//        target.Append(rune);

//      return target;
//    }
//  }
//}
