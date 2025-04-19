namespace Flux
{
  public static partial class Strings
  {
    /// <summary>
    /// <para>Gets a list of <see cref="Text.TextElement"/>s from <paramref name="source"/>, optionally in a <paramref name="reversed"/> order.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="reversed"></param>
    /// <returns></returns>
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
