namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringEx(this System.Text.Rune source)
    {
      var uc = System.Text.Rune.GetUnicodeCategory(source);

      return $"{source.UnicodeUnotationEncode()} '{source}' ({uc}, {uc.ToUnicodeCategoryMajorMinor()})";
    }
  }
}
