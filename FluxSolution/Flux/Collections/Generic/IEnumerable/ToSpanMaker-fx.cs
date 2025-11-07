//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Collections.Generic.IEnumerable<T> source)
//    {
//      var sb = new SpanBuilder<T>();
//      foreach (var item in source)
//        sb.Append(item);
//      return sb;
//    }

//    //public static SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.Collections.Generic.IEnumerable<char> source)
//    //{
//    //  var sm = new SpanMaker<System.Text.Rune>();

//    //  using var e = source.GetEnumerator();

//    //  while (e.MoveNext())
//    //  {
//    //    var c0 = e.Current;

//    //    if (char.IsHighSurrogate(c0))
//    //    {
//    //      if (!(e.MoveNext() && e.Current is var c1 && char.IsLowSurrogate(c1)))
//    //        throw new System.InvalidOperationException("High surrogate without a following low surrogate.");

//    //      sm.Append(new System.Text.Rune(c0, c1));
//    //    }
//    //    else if (char.IsLowSurrogate(c0))
//    //      throw new System.InvalidOperationException("Low surrogate without a preceding high surrogate.");
//    //    else
//    //      sm.Append(new System.Text.Rune(c0));
//    //  }

//    //  return sm;
//    //}
//  }
//}
