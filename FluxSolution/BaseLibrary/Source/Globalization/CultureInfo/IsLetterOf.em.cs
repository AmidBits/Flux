namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsLetterOf(this System.Globalization.CultureInfo source, char character) => source.IsLowerCaseOf(character) || source.IsUpperCaseOf(character);

    public static bool IsLetterOf(this System.Globalization.CultureInfo source, System.Text.Rune rune) => IsLetterOf(source, rune);
  }
}
