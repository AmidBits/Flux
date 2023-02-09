namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringEx(this System.Text.Rune source)
    {
      var uc = System.Text.Rune.GetUnicodeCategory(source);

      return $"\"{source}\" {uc.ToUnicodeCategoryMajorMinor()} ({uc.ToUnicodeCategoryMajor()}, {uc.ToUnicodeCategoryMinorFriendlyString()}) {source.ToUnicodeUnotationString()} {source.ToUnicodeCsEscapeSequenceString(Unicode.CsEscapeSequenceFormat.Variable)}";
    }
  }
}
