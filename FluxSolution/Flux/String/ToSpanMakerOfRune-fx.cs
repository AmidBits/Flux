namespace Flux
{
  public static partial class Strings
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
    public static SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this string source)
    {
      var sm = new SpanMaker<System.Text.Rune>(source.Length);
      if (!string.IsNullOrEmpty(source))
        foreach (var rune in source.EnumerateRunes())
          sm = sm.Append(rune);
      return sm;
    }
  }
}
