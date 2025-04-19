namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<System.Text.Rune> ToListOfRune(this System.Text.StringBuilder source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      foreach (var chunk in source.GetChunks())
        foreach (var rune in chunk.Span.EnumerateRunes())
          list.Add(rune);

      return list;
    }
  }
}
