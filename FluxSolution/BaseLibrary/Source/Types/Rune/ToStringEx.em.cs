namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringEx(this System.Text.Rune source)
      => $"\"{source}\" {System.Text.Rune.GetUnicodeCategory(source).ToUnicodeCategoryMajorMinor()} ({System.Text.Rune.GetUnicodeCategory(source).ToUnicodeCategoryMajor()}, {System.Text.Rune.GetUnicodeCategory(source).ToUnicodeCategoryMinorFriendlyString()}) {source.ToUnicodeUnotationString()} {source.ToUnicodeCsEscapeSequenceString(Unicode.CsEscapeSequenceFormat.Variable)}";
  }
}
