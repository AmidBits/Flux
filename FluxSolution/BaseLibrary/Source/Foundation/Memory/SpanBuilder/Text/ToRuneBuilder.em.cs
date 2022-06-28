//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static SpanBuilder<System.Text.Rune> ToRuneBuilder(ref this SpanBuilder<char> source)
//    {
//      var runes = new SpanBuilder<System.Text.Rune>();

//      for (var index = 0; index < source.Length; index++)
//      {
//        var c1 = source[index];

//        if (char.IsHighSurrogate(c1))
//        {
//          var c2 = source[++index];

//          if (!char.IsLowSurrogate(c2))
//            throw new System.InvalidOperationException(@"Missing low surrogate (required after high surrogate).");

//          runes.Append(new System.Text.Rune(c1, c2));
//        }
//        else if (char.IsLowSurrogate(c1))
//          throw new System.InvalidOperationException(@"Unexpected low surrogate (only allowed after high surrogate).");
//        else
//          runes.Append(new System.Text.Rune(c1));
//      }

//      return runes;
//    }
//  }
//}
