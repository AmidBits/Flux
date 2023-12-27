namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new list of <see cref="System.Text.Rune"/> from <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<System.Text.Rune> ToListOfRune(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      foreach (var rune in source.EnumerateRunes())
        list.Add(rune);

      return list;
    }
  }
}
