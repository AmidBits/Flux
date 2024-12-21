namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>Returns a readonly list with the names and corresponding <see cref="System.Text.Unicode.UnicodeRange"/> objects.</summary>
    private static System.Collections.Generic.Dictionary<System.Text.Unicode.UnicodeRange, string> GetUnicodeRanges()
    {
      var dictionary = new System.Collections.Generic.Dictionary<System.Text.Unicode.UnicodeRange, string>();

      foreach (var pi in Flux.Fx.GetPropertyInfos(typeof(System.Text.Unicode.UnicodeRanges)).Where(pi => pi.Name != nameof(System.Text.Unicode.UnicodeRanges.All) && pi.Name != nameof(System.Text.Unicode.UnicodeRanges.None)))
        if (pi.GetValue(null, null) is System.Text.Unicode.UnicodeRange ur)
          dictionary.Add(ur, pi.Name);

      return dictionary;
    }

    /// <summary>Creates a new sequence with all runes in the <paramref name="unicodeRange"/>.</summary>
    public static System.Collections.Generic.List<System.Text.Rune> GetRunesInUnicodeRange(this System.Text.Unicode.UnicodeRange unicodeRange)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (int codePoint = unicodeRange.FirstCodePoint, length = unicodeRange.Length; length > 0; codePoint++, length--)
        if (System.Text.Rune.IsValid(codePoint))
          list.Add(new System.Text.Rune(codePoint));

      return list;
    }

    /// <summary>Locates the Unicode range and block name of the <paramref name="character"/>.</summary>
    public static System.Collections.Generic.KeyValuePair<System.Text.Unicode.UnicodeRange, string> FindUnicodeRange(this System.Char character)
      => FindUnicodeRange((System.Text.Rune)character);

    /// <summary>Locates the Unicode range and block name of the <paramref name="rune"/>.</summary>
    public static System.Collections.Generic.KeyValuePair<System.Text.Unicode.UnicodeRange, string> FindUnicodeRange(this System.Text.Rune rune)
      => GetUnicodeRanges().FirstOrValue(new(System.Text.Unicode.UnicodeRanges.None, nameof(System.Text.Unicode.UnicodeRanges.None)), (e, i) => rune.Value >= e.Key.FirstCodePoint && rune.Value < (e.Key.FirstCodePoint + e.Key.Length)).Item;
  }
}
