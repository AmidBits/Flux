namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsLowerCaseOf(this System.Globalization.CultureInfo source, char character) => source.IsLowerCaseOf((System.Text.Rune)character);

    public static bool IsLowerCaseOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => rune.Value >= 'a' && rune.Value <= 'z',
        "se" => (rune.Value >= 'a' && rune.Value <= 'z') || rune.Value == '\u00E5' || rune.Value == '\u00E4' || rune.Value == '\u00F6', // 'å', 'ä', 'ö'
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
