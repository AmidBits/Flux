namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsConsonantOf_en(char character)
      => char.IsLetter(character) && character is 'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'y' or 'z' or 'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Y' or 'Z';
    public static bool IsConsonantOf_se(char character)
      => char.IsLetter(character) && character is 'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' or 'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z';

    /// <summary></summary>
    /// <exception cref="System.NotImplementedException"/>
    public static bool IsConsonantOf(this System.Globalization.CultureInfo source, char character)
      => (source ??= System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsConsonantOf_en(character),
        "se" => IsConsonantOf_se(character),
        _ => throw new System.NotImplementedException(nameof(source))
      };

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsConsonantOf(this System.Globalization.CultureInfo source, System.Text.Rune rune)
      => (source ??= System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => IsConsonantOf_en((char)rune.Value),
        "se" => IsConsonantOf_se((char)rune.Value),
        _ => throw new System.NotImplementedException(nameof(source))
      };

    /// <summary></summary>
    /// <exception cref="System.OverflowException"/>
    public static bool IsConsonantOf(this System.Globalization.CultureInfo source, string textElement)
      => (source ??= System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => textElement is not null && textElement.Length == 1 && IsConsonantOf_en(textElement[0]),
        "se" => textElement is not null && textElement.Length == 1 && IsConsonantOf_se(textElement[0]),
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
