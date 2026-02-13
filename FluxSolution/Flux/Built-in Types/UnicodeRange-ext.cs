namespace Flux
{
  public static partial class Unicode
  {
    extension(System.Text.Unicode.UnicodeRange)
    {
      /// <summary>
      /// <para>Returns a list with names and a corresponding <see cref="System.Text.Unicode.UnicodeRange"/>.</para>
      /// <para>This list does not include non-unicode defined ranges, e.g. <see cref="System.Text.Unicode.UnicodeRanges.All"/> and <see cref="System.Text.Unicode.UnicodeRanges.None"/></para>
      /// </summary>
      public static System.Collections.Generic.IDictionary<string, System.Text.Unicode.UnicodeRange> GetUnicodeRanges()
      {
        var dictionary = new System.Collections.Generic.SortedDictionary<string, System.Text.Unicode.UnicodeRange>();

        foreach (var kvp in typeof(System.Text.Unicode.UnicodeRanges).GetMemberDictionary(null).Where(kvp => kvp.Key.Name != nameof(System.Text.Unicode.UnicodeRanges.All) && kvp.Key.Name != nameof(System.Text.Unicode.UnicodeRanges.None)))
          if (kvp.Value is System.Text.Unicode.UnicodeRange ur)
            dictionary.Add(kvp.Key.Name, ur);

        return dictionary;
      }
    }

    extension(System.Text.Unicode.UnicodeRange unicodeRange)
    {
      /// <summary>
      /// <para>Creates a new list with all runes in a specified <see cref="System.Text.Unicode.UnicodeRange"/>.</para>
      /// </summary>
      public System.Collections.Generic.List<System.Text.Rune> GetRunes()
      {
        var list = new System.Collections.Generic.List<System.Text.Rune>();

        for (var codePoint = unicodeRange.FirstCodePoint; list.Count < unicodeRange.Length; codePoint++)
          if (System.Text.Rune.IsValid(codePoint))
            list.Add(new System.Text.Rune(codePoint));

        return list;
      }
    }
  }
}
