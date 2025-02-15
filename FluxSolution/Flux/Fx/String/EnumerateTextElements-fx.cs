namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.List<Text.TextElement> GetTextElements(this string source)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      var si = new System.Globalization.StringInfo(source);

      for (var index = 0; index < si.LengthInTextElements; index++)
        list.Add(new Text.TextElement(si.SubstringByTextElements(index, 1)));

      return list;
    }
  }
}
