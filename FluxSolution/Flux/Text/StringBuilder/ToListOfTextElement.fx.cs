namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Creates a new list of <see cref="BaseLibrary.Source.Text.TextElement"/> from <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<Text.TextElement> ToListOfTextElement(this System.Text.StringBuilder source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      foreach (var chunk in source.GetChunks())
        foreach (var textElement in chunk.Span.EnumerateTextElements())
          list.Add(textElement);

      return list;
    }
  }
}
