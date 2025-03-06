namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringEx(this char source)
    {
      var uc = char.GetUnicodeCategory(source);

      return $"{source.UnicodeUnotationEncode()} '{source}' ({uc}, {uc.ToUnicodeCategoryMajorMinor()})";
    }
  }
}
