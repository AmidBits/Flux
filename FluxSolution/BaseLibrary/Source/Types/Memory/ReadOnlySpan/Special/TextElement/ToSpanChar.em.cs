namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.ReadOnlySpan<char> ToSpanChar(this System.ReadOnlySpan<Text.TextElement> source)
    {
      var list = new System.Collections.Generic.List<char>();

      for (var index = 0; index < source.Length; index++)
        list.AddRange(source[index].ListChar);

      return list.AsReadOnlySpan();
    }
  }
}
