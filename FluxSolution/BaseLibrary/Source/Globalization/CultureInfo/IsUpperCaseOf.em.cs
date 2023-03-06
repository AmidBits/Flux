namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsUpperCaseOf_en(char character)
      => character is >= 'A' and <= 'Z';
    public static bool IsUpperCaseOf_se(char character)
      => IsUpperCaseOf_en(character) || character is '\u00C5' or '\u00C4' or '\u00D6'; // 'Å', 'Ä', 'Ö'

    public static bool IsUpperCaseOf(this System.Globalization.CultureInfo source, char character)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsUpperCaseOf_en(character),
        "se" => IsUpperCaseOf_se(character),
        _ => throw new System.NotImplementedException(nameof(source))
      };

    public static bool IsUpperCaseOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsUpperCaseOf_en((char)rune.Value),
        "se" => IsUpperCaseOf_se((char)rune.Value),
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
