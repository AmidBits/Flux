namespace Flux
{
  public static partial class ExtensionMethodsString
  {
    public static System.Collections.Generic.IEnumerable<Text.TextElement> EnumerateTextElementsReverse(this string source)
    {
      var si = new System.Globalization.StringInfo(source);

      for (var index = si.LengthInTextElements - 1; index >= 0; index--)
        yield return new Text.TextElement(si.SubstringByTextElements(index, 1));
    }
  }
}
