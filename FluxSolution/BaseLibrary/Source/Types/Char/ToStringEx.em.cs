namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringEx(this char source)
    {
      var uc = char.GetUnicodeCategory(source);

      return $"\'{source}\' {uc.ToUnicodeCategoryMajorMinor()}  ({uc.ToUnicodeCategoryMajor()}, {uc.ToUnicodeCategoryMinorFriendlyString()}) {source.ToUnicodeUnotationString()} {source.ToUnicodeCsEscapeSequenceString(Unicode.CsEscapeSequenceFormat.Variable)}";
    }
  }
}
