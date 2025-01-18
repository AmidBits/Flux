namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new list of <see cref="System.Char"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> ToListOfChar(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ToString());

      return list;
    }

    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sm = new Flux.SpanMaker<char>();
      for (var index = 0; index < source.Length; index++)
        sm = sm.Append(1, source[index].ToString());
      return sm;
    }
  }
}
