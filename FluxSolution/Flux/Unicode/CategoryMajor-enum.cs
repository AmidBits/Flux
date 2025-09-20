namespace Flux
{
  /// <summary>
  /// <para>This is an aggregate derivation of the System.Globalization.UnicodeCategory (or MajorMinorCode) enum value. The values represents the character of the first letter in the major code name, e.g. 'P' for Puncuation.</para>
  /// <code><example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example></code>
  /// </summary>
  public enum UnicodeCategoryMajor
  {
    Letter = 'L',
    Mark = 'M',
    Number = 'N',
    Punctuation = 'P',
    Symbol = 'S',
    Separator = 'Z',
    Other = 'C',
  }
}
