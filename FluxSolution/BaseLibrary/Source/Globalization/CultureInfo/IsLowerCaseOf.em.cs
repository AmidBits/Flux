namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsLowerCaseOf(this System.Globalization.CultureInfo source, char character)
      => source.TwoLetterISOLanguageName switch
      {
        "en" => character >= 'a' && character <= 'z',
        "se" => (character >= 'a' && character <= 'z') || character == '\u00E5' || character == '\u00E4' || character == '\u00F6', // 'å', 'ä', 'ö'
        _ => throw new System.NotImplementedException(nameof(source))
      };

    public static bool IsLowerCaseOf(this System.Globalization.CultureInfo source, System.Text.Rune rune) => IsLowerCaseOf(source, (char)rune.Value);
  }
}
