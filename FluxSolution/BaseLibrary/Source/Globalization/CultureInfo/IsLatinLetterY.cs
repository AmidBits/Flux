namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</summary>
    /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
    public static bool IsLatinLetterY(this char character) => character == 'Y' || character == 'y';

    /// <summary>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</summary>
    /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
    public static bool IsLatinLetterY(this System.Text.Rune rune) => rune == (System.Text.Rune)'Y' || rune == (System.Text.Rune)'y';
  }
}
