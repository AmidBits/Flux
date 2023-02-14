namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsUpperCaseOf(this System.Globalization.CultureInfo source, char character) => source.IsUpperCaseOf((System.Text.Rune)character);

    public static bool IsUpperCaseOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => rune.Value >= 'A' && rune.Value <= 'Z',
        "se" => (rune.Value >= 'A' && rune.Value <= 'Z') || rune.Value == '\u00C5' || rune.Value == '\u00C4' || rune.Value == '\u00D6', // 'Å', 'Ä', 'Ö'
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
