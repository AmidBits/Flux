namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static System.Collections.Generic.IEnumerable<string> EnumerateTextElements(this SequenceBuilder<System.Text.Rune> source)
    {
      var si = new System.Globalization.StringInfo(source.ToString());

      for (var index = 0; index < si.LengthInTextElements; index++)
        yield return si.SubstringByTextElements(index);
    }
  }
}
