namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>Removes latin stroke diacriticals from any character with the strokes, i.e. a character without a diacritic is returned in its place. Characters without latin strokes are returned as-is.</summary>
    /// <remarks>These are characters that are not (necessarily) identified in .NET.</remarks>
    public static System.Text.Rune ReplaceDiacriticalLatinStroke(this System.Text.Rune source)
      => source.Value switch
      {
        '\u023A' => (System.Text.Rune)'A', // Latin Capital Letter A with stroke
        '\u0243' => (System.Text.Rune)'B', // Latin Capital Letter B with stroke
        '\u0180' => (System.Text.Rune)'b', // Latin Small Letter B with stroke
        '\u023B' => (System.Text.Rune)'C', // Latin Capital Letter C with stroke
        '\u023C' => (System.Text.Rune)'c', // Latin Small Letter C with stroke
        '\u0110' => (System.Text.Rune)'D', // Latin Capital Letter D with stroke
        '\u0111' => (System.Text.Rune)'d', // Latin Small Letter D with stroke
        '\u0246' => (System.Text.Rune)'E', // Latin Capital Letter E with stroke
        '\u0247' => (System.Text.Rune)'e', // Latin Small Letter E with stroke
        '\u01E4' => (System.Text.Rune)'G', // Latin Capital Letter G with stroke
        '\u01E5' => (System.Text.Rune)'g', // Latin Small Letter G with stroke
        '\u0126' => (System.Text.Rune)'H', // Latin Capital Letter H with stroke
        '\u0127' => (System.Text.Rune)'h', // Latin Small Letter H with stroke
        '\u0197' => (System.Text.Rune)'I', // Latin Capital Letter I with stroke
        '\u0248' => (System.Text.Rune)'J', // Latin Capital Letter J with stroke
        '\u0249' => (System.Text.Rune)'j', // Latin Small Letter J with stroke
        '\u0141' => (System.Text.Rune)'L', // Latin Capital Letter L with stroke
        '\u0142' => (System.Text.Rune)'l', // Latin Small Letter L with stroke
        '\u00D8' => (System.Text.Rune)'O', // Latin Capital letter O with stroke
        '\u01FE' => (System.Text.Rune)'O', // Latin Capital letter O with stroke and acute
        '\u00F8' => (System.Text.Rune)'o', // Latin Small Letter O with stroke
        '\u01FF' => (System.Text.Rune)'o', // Latin Small Letter O with stroke and acute
        '\u024C' => (System.Text.Rune)'R', // Latin Capital letter R with stroke
        '\u024D' => (System.Text.Rune)'r', // Latin Small Letter R with stroke
        '\u0166' => (System.Text.Rune)'T', // Latin Capital Letter T with stroke
        '\u023E' => (System.Text.Rune)'T', // Latin Capital Letter T with diagonal stroke
        '\u0167' => (System.Text.Rune)'t', // Latin Small Letter T with stroke
        '\u024E' => (System.Text.Rune)'Y', // Latin Capital letter Y with stroke
        '\u024F' => (System.Text.Rune)'y', // Latin Small Letter Y with stroke
        '\u01B5' => (System.Text.Rune)'Z', // Latin Capital Letter Z with stroke
        '\u01B6' => (System.Text.Rune)'z', // Latin Small Letter Z with stroke
        _ => source,
      };
  }
}
