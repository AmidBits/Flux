namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsVowelOf_en(char character) => char.IsLetter(character) && character is 'a' or 'e' or 'i' or 'o' or 'u' or 'y' or 'A' or 'E' or 'I' or 'O' or 'U' or 'Y';
    public static bool IsVowelOf_se(char character) => IsVowelOf_en(character) || character is '\u00E5' or '\u00E4' or '\u00F6' or '\u00C5' or '\u00C4' or '\u00D6';

    /// <summary></summary>
    /// <exception cref="System.NotImplementedException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, char character)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsVowelOf_en(character),
        "se" => IsVowelOf_se(character),
        _ => throw new System.NotImplementedException(nameof(source))
      };

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsVowelOf_en((char)rune.Value),
        "se" => IsVowelOf_se((char)rune.Value),
        _ => throw new System.NotImplementedException(nameof(source))
      };

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsVowelOf(this System.Globalization.CultureInfo source, string textElement)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => textElement is not null && textElement.Length == 1 && IsVowelOf_en(textElement[0]),
        "se" => textElement is not null && textElement.Length == 1 && IsVowelOf_se(textElement[0]),
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
