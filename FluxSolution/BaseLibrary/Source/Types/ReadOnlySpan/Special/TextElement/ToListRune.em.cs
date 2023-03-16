namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> of <see cref="System.Text.Rune"/> from the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.List<System.Text.Rune> ToListRune(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ToReadOnlyListRune());

      return list;
    }
  }
}
