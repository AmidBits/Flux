namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.ReadOnlySpan<Text.TextElement> ToSpanTextElement(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      foreach (var textElement in source.EnumerateTextElements())
        list.Add(textElement);

      return list.AsReadOnlySpan();
    }
  }
}
