namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static System.Collections.Generic.IEnumerable<string> EnumerateTextElementsReverse(this SequenceBuilder<System.Text.Rune> source)
    {
      var si = new System.Globalization.StringInfo(source.ToString());

      for (var index = si.LengthInTextElements - 1; index >= 0; index--)
        yield return si.SubstringByTextElements(index);
    }
  }
}
