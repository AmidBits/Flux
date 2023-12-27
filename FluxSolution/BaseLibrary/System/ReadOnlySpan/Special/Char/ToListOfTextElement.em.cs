namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new list of <see cref="Text.TextElement"/> from the <paramref name="source"/>.</summary>
    /// <remarks>A <see cref="System.Collections.Generic.List{T}"/> can be non-allocatingly converted (i.e. casted) to <see cref="System.Span{T}"/>.</remarks>
    public static System.Collections.Generic.List<Text.TextElement> ToListOfTextElement(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      foreach (var textElement in source.EnumerateTextElements())
        list.Add(textElement);

      return list;
    }
  }
}
