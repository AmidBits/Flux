namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    public static bool IsConsonantOf(this System.Globalization.CultureInfo source, char character)
      => source.TwoLetterISOLanguageName switch
      {
        "en" => character switch
        {
          'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'y' or 'z' or
          'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Y' or 'Z' => true,
          _ => false
        },
        "se" => character switch
        {
          'b' or 'c' or 'd' or 'f' or 'g' or 'h' or 'j' or 'k' or 'l' or 'm' or 'n' or 'p' or 'q' or 'r' or 's' or 't' or 'v' or 'w' or 'x' or 'z' or
          'B' or 'C' or 'D' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' or 'M' or 'N' or 'P' or 'Q' or 'R' or 'S' or 'T' or 'V' or 'W' or 'X' or 'Z' => true,
          _ => false
        },
        _ => throw new System.NotImplementedException(nameof(source))
      };

    public static bool IsConsonantOf(this System.Globalization.CultureInfo source, System.Text.Rune rune) => IsConsonantOf(source, (char)rune.Value);
  }
}
