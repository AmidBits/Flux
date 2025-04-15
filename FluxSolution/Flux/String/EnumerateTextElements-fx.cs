namespace Flux
{
  public static partial class Strings
  {
    public static System.Collections.Generic.List<Text.TextElement> GetTextElements(this string source, bool reversed = false)
    {
      var list = new System.Collections.Generic.List<Text.TextElement>();

      var si = new System.Globalization.StringInfo(source);

      if (reversed)
        for (var index = si.LengthInTextElements - 1; index >= 0; index--)
          list.Add(new Text.TextElement(si.SubstringByTextElements(index, 1)));
      else
        for (var index = 0; index < si.LengthInTextElements; index++)
          list.Add(new Text.TextElement(si.SubstringByTextElements(index, 1)));

      return list;
    }
  }
}
