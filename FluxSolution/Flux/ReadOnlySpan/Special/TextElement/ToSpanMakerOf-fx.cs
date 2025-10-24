//namespace Flux
//{
//  public static partial class ReadOnlySpans
//  {
//    /// <summary>
//    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <returns></returns>
//    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<Text.TextElement> source)
//    {
//      var sm = new Flux.SpanMaker<char>();
//      for (var index = 0; index < source.Length; index++)
//        sm = sm.Append(1, (System.Collections.Generic.ICollection<char>)source[index].AsReadOnlyListOfChar);
//      return sm;
//    }

//    /// <summary>
//    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <returns></returns>
//    public static Flux.SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.ReadOnlySpan<Text.TextElement> source)
//    {
//      var sm = new Flux.SpanMaker<System.Text.Rune>();
//      for (var index = 0; index < source.Length; index++)
//        sm = sm.Append(1, source[index].AsReadOnlySpan().ToListOfRune());
//      return sm;
//    }

//    /// <summary>
//    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <returns></returns>
//    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<string> source)
//    {
//      var sm = new Flux.SpanMaker<char>();
//      for (var index = 0; index < source.Length; index++)
//        sm = sm.Append(1, source[index].AsSpan().ToListOfRune());
//      return sm;
//    }

//    /// <summary>
//    /// <para>Creates a new <see cref="SpanMaker{T}"/> from the <paramref name="source"/> as content.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <returns></returns>
//    public static Flux.SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this System.ReadOnlySpan<string> source)
//    {
//      var sm = new Flux.SpanMaker<System.Text.Rune>();
//      for (var index = 0; index < source.Length; index++)
//        sm = sm.Append(1, source[index].AsSpan().ToListOfRune());
//      return sm;
//    }
//  }
//}
