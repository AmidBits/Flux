namespace Flux
{
  public static partial class Chars
  {
    public static string ToVerboseString(this char source)
    {
      var uc = char.GetUnicodeCategory(source);

      return $"{source.UnicodeUnotationEncode()} '{source}' ({uc}, {uc.ToUnicodeCategoryMajorMinor()})";
    }
  }
}
