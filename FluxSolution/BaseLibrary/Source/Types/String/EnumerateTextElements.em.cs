using Flux.Text;

namespace Flux
{
  public static partial class StringEm
  {
    public static System.Collections.Generic.IEnumerable<TextElement> EnumerateTextElements(this string source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var si = new System.Globalization.StringInfo(source);

      for (var index = 0; index < si.LengthInTextElements; index++)
        yield return new TextElement(si.SubstringByTextElements(index, 1), index);
    }
  }
}
