namespace Flux
{
  /// <summary>
  /// <para>This is a directly correlated enum of System.Globalization.UnicodeCategory to ease translation to the abbreviated two character code for the major and minor parts of the System.Globalization.UnicodeCategory enum values.</para>
  /// <code><example>var allCharactersByCategoryMajorMinorCode = Flux.Unicode.GetUnicodeCategoryCharacters().GroupBy(kv => kv.Key.ToCategoryMajorMinorCode()).ToDictionary(g => g.Key, g => g.SelectMany(kv => kv.Value).ToList());</example></code>
  /// </summary>
  public enum UnicodeCategoryMajorMinor
  {
    Ll = System.Globalization.UnicodeCategory.LowercaseLetter,
    Lm = System.Globalization.UnicodeCategory.ModifierLetter,
    Lo = System.Globalization.UnicodeCategory.OtherLetter,
    Lt = System.Globalization.UnicodeCategory.TitlecaseLetter,
    Lu = System.Globalization.UnicodeCategory.UppercaseLetter,

    Mc = System.Globalization.UnicodeCategory.SpacingCombiningMark,
    Me = System.Globalization.UnicodeCategory.EnclosingMark,
    Mn = System.Globalization.UnicodeCategory.NonSpacingMark,

    Nd = System.Globalization.UnicodeCategory.DecimalDigitNumber,
    Nl = System.Globalization.UnicodeCategory.LetterNumber,
    No = System.Globalization.UnicodeCategory.OtherNumber,

    Pc = System.Globalization.UnicodeCategory.ConnectorPunctuation,
    Pd = System.Globalization.UnicodeCategory.DashPunctuation,
    Pe = System.Globalization.UnicodeCategory.ClosePunctuation,
    Pf = System.Globalization.UnicodeCategory.FinalQuotePunctuation,
    Pi = System.Globalization.UnicodeCategory.InitialQuotePunctuation,
    Po = System.Globalization.UnicodeCategory.OtherPunctuation,
    Ps = System.Globalization.UnicodeCategory.OpenPunctuation,

    Sc = System.Globalization.UnicodeCategory.CurrencySymbol,
    Sk = System.Globalization.UnicodeCategory.ModifierSymbol,
    Sm = System.Globalization.UnicodeCategory.MathSymbol,
    So = System.Globalization.UnicodeCategory.OtherSymbol,

    Zl = System.Globalization.UnicodeCategory.LineSeparator,
    Zp = System.Globalization.UnicodeCategory.ParagraphSeparator,
    Zs = System.Globalization.UnicodeCategory.SpaceSeparator,

    Cc = System.Globalization.UnicodeCategory.Control,
    Cf = System.Globalization.UnicodeCategory.Format,
    Cn = System.Globalization.UnicodeCategory.OtherNotAssigned,
    Co = System.Globalization.UnicodeCategory.PrivateUse,
    Cs = System.Globalization.UnicodeCategory.Surrogate,
  }
}
