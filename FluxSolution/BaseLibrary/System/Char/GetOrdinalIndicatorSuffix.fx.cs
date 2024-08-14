namespace Flux
{
  public static partial class Fx
  {
    public static string GetOrdinalIndicatorSuffix(this char onesDigit, char tensDigit)
    {
      if (!System.Char.IsDigit(tensDigit) || tensDigit != '1')
      {
        if (onesDigit == '1') return "st";
        if (onesDigit == '2') return "nd";
        if (onesDigit == '3') return "rd";
      }

      if (System.Char.IsDigit(onesDigit)) return "th";

      return string.Empty;
    }
  }
}
