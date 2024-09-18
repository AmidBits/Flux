namespace Flux
{
  public static partial class Fx
  {
    public static string GetOrdinalIndicatorSuffix(this System.Text.Rune onesDigit, System.Text.Rune tensDigit = default!)
      => ((char)onesDigit.Value).GetOrdinalIndicatorSuffix((char)tensDigit.Value);
  }
}
