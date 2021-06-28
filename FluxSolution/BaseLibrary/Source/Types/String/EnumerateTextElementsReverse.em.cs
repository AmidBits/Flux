namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<string> EnumerateTextElementsReverse(this string source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var si = new System.Globalization.StringInfo(source);

      for (var index = si.LengthInTextElements - 1; index >= 0; index--)
        yield return si.SubstringByTextElements(index);
    }
  }
}
