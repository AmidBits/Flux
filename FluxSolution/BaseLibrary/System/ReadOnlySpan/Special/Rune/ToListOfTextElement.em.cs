namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new list of <see cref="Text.TextElement"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<Text.TextElement> ToListOfTextElement(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      var stringInfo = new System.Globalization.StringInfo(source.ToListOfChar().AsSpan().ToString());

      for (var index = 0; index < stringInfo.LengthInTextElements; index++)
        list.Add(new Text.TextElement(stringInfo.SubstringByTextElements(index, 1)));

      return list;
    }
  }
}
