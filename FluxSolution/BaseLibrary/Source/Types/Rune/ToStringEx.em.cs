namespace Flux
{
  public static partial class Unicode
  {
    public static string ToStringEx(this System.Text.Rune source)
      => $"{System.Text.Rune.GetUnicodeCategory(source)} {source.ToUnotationString()}";
  }
}
