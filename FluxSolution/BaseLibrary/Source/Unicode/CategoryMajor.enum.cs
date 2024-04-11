namespace Flux
{
  public static partial class Unicode
  {
    public static UnicodeCategoryMajor ParseUnicodeCategoryMajor(this char unicodeCategoryMajor)
      => (UnicodeCategoryMajor)System.Enum.Parse(typeof(UnicodeCategoryMajor), unicodeCategoryMajor.ToString(), true);

    public static bool TryParseUnicodeCategoryMajor(this char unicodeCategoryMajor, out UnicodeCategoryMajor result)
    {
      try
      {
        result = ParseUnicodeCategoryMajor(unicodeCategoryMajor);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>Translates the <see cref="UnicodeCategoryMajorMinor"/> enum value (<paramref name="majorMinor"/>) into a <see cref="UnicodeCategoryMajor"/> enum value.</summary>
    /// <example>var allCharactersByCategoryMajorLabel = Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
    public static UnicodeCategoryMajor ToUnicodeCategoryMajor(this UnicodeCategoryMajorMinor majorMinor)
      => ((System.Globalization.UnicodeCategory)majorMinor).ToUnicodeCategoryMajor();
  }

  /// <summary>This is an aggregate derivation of the System.Globalization.UnicodeCategory (or MajorMinorCode) enum value. The values represents the character of the first letter in the major code name, e.g. 'P' for Puncuation.</summary>
  /// <example>var allCharactersByCategoryMajorLabel = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorLabel()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example>
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
