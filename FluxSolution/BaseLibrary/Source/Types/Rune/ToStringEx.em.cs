namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringEx(this System.Text.Rune source)
      => $"{System.Text.Rune.GetUnicodeCategory(source)} {source.ToUnicodeUnotationString()}";
  }
}
