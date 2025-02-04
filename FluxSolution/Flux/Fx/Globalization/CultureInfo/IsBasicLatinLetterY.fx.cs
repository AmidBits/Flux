namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</para>
    /// </summary>
    /// <param name="source">The <see cref="System.Char"/> to check.</param>
    /// <returns></returns>
    /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
    public static bool IsBasicLatinLetterY(this char source) => source is 'y' or 'Y';

    /// <summary>
    /// <para>Indicates whether the character is the letter 'Y' or 'y', i.e. ignore case.</para>
    /// </summary>
    /// <param name="source">The <see cref="System.Text.Rune"/> to check.</param>
    /// <returns></returns>
    /// <remarks>Provided for consistent check against consonants and vowels in English.</remarks>
    public static bool IsBasicLatinLetterY(this System.Text.Rune source) => source.Value is 'y' or 'Y';
  }
}
