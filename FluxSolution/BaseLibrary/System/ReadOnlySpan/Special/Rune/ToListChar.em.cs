namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> of <see cref="System.Char"/> from the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.List<char> ToListChar(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ToString());

      return list;
    }
  }
}
