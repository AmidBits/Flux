namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringEx(this char source)
      => $"\'{source}\' {char.GetUnicodeCategory(source)} {source.ToUnicodeUnotationString()} {source.ToUnicodeCsEscapeSequenceString(Unicode.CsEscapeSequenceFormat.Variable)}";
  }
}
