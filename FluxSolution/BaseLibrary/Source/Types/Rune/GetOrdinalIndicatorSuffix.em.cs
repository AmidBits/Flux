namespace Flux
{
  public static partial class ExtensionMethodsRune
  {
    public static string GetOrdinalIndicatorSuffix(this System.Text.Rune onesDigit, bool tensDigitExistsAndIs1)
    {
      if (!tensDigitExistsAndIs1)
      {
        if (onesDigit == (System.Text.Rune)'1')
          return "st";
        if (onesDigit == (System.Text.Rune)'2')
          return "nd";
        if (onesDigit == (System.Text.Rune)'3')
          return "rd";
      }

      return "th";
    }
    public static string GetOrdinalIndicatorSuffix(this System.Text.Rune onesDigit, System.Text.Rune tensDigit)
      => System.Text.Rune.IsDigit(onesDigit) ? GetOrdinalIndicatorSuffix(onesDigit, System.Text.Rune.IsDigit(tensDigit) && tensDigit == (System.Text.Rune)'1') : string.Empty;
  }
}
