namespace Flux
{
  public static partial class ExtensionMethodsChar
  {
    public static string GetOrdinalIndicatorSuffix(this char onesDigit, bool tensDigitExistsAndIs1)
      => ((System.Text.Rune)onesDigit).GetOrdinalIndicatorSuffix(tensDigitExistsAndIs1);

    public static string GetOrdinalIndicatorSuffix(this char onesDigit, char tensDigit)
      => ((System.Text.Rune)onesDigit).GetOrdinalIndicatorSuffix((System.Text.Rune)tensDigit);
  }
}
