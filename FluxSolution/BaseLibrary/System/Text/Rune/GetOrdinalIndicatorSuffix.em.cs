namespace Flux
{
  public static partial class Fx
  {
    public static string GetOrdinalIndicatorSuffix(this System.Text.Rune onesDigit, System.Text.Rune tensDigit)
    {
      if (!System.Text.Rune.IsDigit(tensDigit) || tensDigit != (System.Text.Rune)'1')
      {
        if (onesDigit == (System.Text.Rune)'1') return "st";
        if (onesDigit == (System.Text.Rune)'2') return "nd";
        if (onesDigit == (System.Text.Rune)'3') return "rd";
      }

      if (System.Text.Rune.IsDigit(onesDigit)) return "th";

      return string.Empty;
    }
  }
}
