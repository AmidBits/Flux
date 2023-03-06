namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</summary>
    /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
    public static bool IsBasicLatinLetterY(this char character) => character is 'y' or 'Y';

    /// <summary>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</summary>
    /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
    public static bool IsBasicLatinLetterY(this System.Text.Rune rune) => (char)rune.Value is 'y' or 'Y';
  }
}
