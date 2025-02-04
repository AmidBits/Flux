namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<Text.TextElement> EnumerateTextElements(this string source)
    {
      var si = new System.Globalization.StringInfo(source);

      for (var index = 0; index < si.LengthInTextElements; index++)
        yield return new Text.TextElement(si.SubstringByTextElements(index, 1));
    }
  }
}
