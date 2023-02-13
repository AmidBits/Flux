namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.Collections.Generic.List<System.Text.Rune> ToListRune(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ListRune);

      return list;
    }
  }
}
