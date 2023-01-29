namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsUpperCaseOf(this System.Globalization.CultureInfo source, char character)
      => source.TwoLetterISOLanguageName switch
      {
        "en" => character >= 'A' && character <= 'Z',
        "se" => (character >= 'A' && character <= 'Z') || character == '\u00C5' || character == '\u00C4' || character == '\u00D6', // 'Å', 'Ä', 'Ö'
        _ => throw new System.NotImplementedException(nameof(source))
      };

    public static bool IsUpperCaseOf(this System.Globalization.CultureInfo source, System.Text.Rune rune) => IsUpperCaseOf(source, (char)rune.Value);
  }
}
