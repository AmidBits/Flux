namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, char character)
      => source.TwoLetterISOLanguageName switch
      {
        "en" => character switch
        {
          'a' or 'e' or 'i' or 'o' or 'u' or 'y' or
          'A' or 'E' or 'I' or 'O' or 'U' or 'Y' => true,
          _ => false
        },
        "se" => character switch
        {
          'a' or 'e' or 'i' or 'o' or 'u' or 'y' or '\u00E5' or '\u00E4' or '\u00F6' or
          'A' or 'E' or 'I' or 'O' or 'U' or 'Y' or '\u00C5' or '\u00C4' or '\u00D6' => true,
          _ => false
        },
        _ => throw new System.NotImplementedException(nameof(source))
      };

    public static bool IsVowelOf(this System.Globalization.CultureInfo source, System.Text.Rune rune) => IsVowelOf(source, (char)rune.Value);
  }
}
