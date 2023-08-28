namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsLowerCaseOf_en(char character) => character is >= 'a' and <= 'z';
    public static bool IsLowerCaseOf_se(char character) => IsLowerCaseOf_en(character) || character is '\u00E5' or '\u00E4' or '\u00F6'; // 'å', 'ä', 'ö'

    public static bool IsLowerCaseOf(this System.Globalization.CultureInfo source, char character)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsLowerCaseOf_en(character),
        "se" => IsLowerCaseOf_se(character),
        _ => throw new System.NotImplementedException(nameof(source))
      };

    public static bool IsLowerCaseOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsLowerCaseOf_en((char)rune.Value),
        "se" => IsLowerCaseOf_se((char)rune.Value),
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
