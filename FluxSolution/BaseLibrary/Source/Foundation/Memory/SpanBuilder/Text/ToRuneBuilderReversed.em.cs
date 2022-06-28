//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    public static SpanBuilder<System.Text.Rune> ToRuneBuilderReversed(ref this SpanBuilder<char> source)
//    {
//      var runes = new SpanBuilder<System.Text.Rune>();

//      for (var index = source.Length - 1; index >= 0; index--)
//      {
//        var rc2 = source[index];

//        if (char.IsLowSurrogate(rc2))
//        {
//          var rc1 = source[--index];

//          if (!char.IsHighSurrogate(rc1))
//            throw new System.InvalidOperationException(@"Missing high surrogate (required before low surrogate).");

//          runes.Append(new System.Text.Rune(rc1, rc2));
//        }
//        else if (char.IsHighSurrogate(rc2))
//          throw new System.InvalidOperationException(@"Unexpected high surrogate (only allowed before low surrogate).");
//        else
//          runes.Append(new System.Text.Rune(rc2));
//      }

//      return runes;
//    }
//  }
//}
