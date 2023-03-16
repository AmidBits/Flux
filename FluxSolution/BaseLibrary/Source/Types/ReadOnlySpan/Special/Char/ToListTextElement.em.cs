namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> of <see cref="Text.TextElement"/> from the <paramref name="source"/>.</summary>
    public static System.Collections.Generic.List<Text.TextElement> ToListTextElement(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      foreach (var textElement in source.EnumerateTextElements())
        list.Add(textElement);

      return list;
    }
  }
}
