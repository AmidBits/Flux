namespace Flux
{
  public static partial class Runes
  {
    public static string ToVerboseString(this System.Text.Rune source)
    {
      var uc = System.Text.Rune.GetUnicodeCategory(source);

      return $"{source.UnicodeUnotationEncode()} '{source}' ({uc}, {uc.ToUnicodeCategoryMajorMinor()})";
    }
  }
}
