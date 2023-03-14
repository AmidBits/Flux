namespace Flux
{
  public static partial class ExtensionMethodsChar
  {
    public static string GetOrdinalIndicatorSuffix(this char onesDigit, char tensDigit) => ((System.Text.Rune)onesDigit).GetOrdinalIndicatorSuffix((System.Text.Rune)tensDigit);
  }
}
