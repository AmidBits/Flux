namespace Flux
{
  public static partial class Fx
  {

    /// <summary>Creates a new list of <see cref="System.Char"/> from the source.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<char> ToListOfChar(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].AsReadOnlyListOfChar);

      return list;
    }

    public static Flux.SpanMaker<char> ToSpanMakerOfChar(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var sb = new Flux.SpanMaker<char>();
      for (var index = 0; index < source.Length; index++)
        sb.Append(1, (System.Collections.Generic.ICollection<char>)source[index].AsReadOnlyListOfChar);
      return sb;
    }
  }
}
