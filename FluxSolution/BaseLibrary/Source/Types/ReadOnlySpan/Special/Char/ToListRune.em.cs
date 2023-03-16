namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> of <see cref="System.Text.Rune"/> from <paramref name="source"/>.</summary>
    public static System.Collections.Generic.List<System.Text.Rune> ToListRune(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      foreach (var rune in source.EnumerateRunes())
        list.Add(rune);

      return list;
    }
  }
}
