namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary></summary>
    /// <exception cref="System.NotImplementedException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, char character)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
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

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => rune.Value switch
        {
          'a' or 'e' or 'i' or 'o' or 'u' or 'y' or
          'A' or 'E' or 'I' or 'O' or 'U' or 'Y' => true,
          _ => false
        },
        "se" => rune.Value switch
        {
          'a' or 'e' or 'i' or 'o' or 'u' or 'y' or '\u00E5' or '\u00E4' or '\u00F6' or
          'A' or 'E' or 'I' or 'O' or 'U' or 'Y' or '\u00C5' or '\u00C4' or '\u00D6' => true,
          _ => false
        },
        _ => throw new System.NotImplementedException(nameof(source))
      };

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, string textElement)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => textElement switch
        {
          "a" or "e" or "i" or "o" or "u" or "y" or
          "A" or "E" or "I" or "O" or "U" or "Y" => true,
          _ => false
        },
        "se" => textElement switch
        {
          "a" or "e" or "i" or "o" or "u" or "y" or "\u00E5" or "\u00E4" or "\u00F6" or
          "A" or "E" or "I" or "O" or "U" or "Y" or "\u00C5" or "\u00C4" or "\u00D6" => true,
          _ => false
        },
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
