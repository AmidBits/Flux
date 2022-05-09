namespace Flux
{
  public static partial class CharEm
  {
    /// <summary>Replaces a character with diacritical latin stroke with the closest 'plain' character, i.e. a character without a diacritic is returned in its place. Characters without latin strokes are returned as-is.</summary>
    /// <remarks>These are characters that are not (necessarily) identified in .NET.</remarks>
    public static char ReplaceDiacriticalLatinStroke(this char source)
      => source switch
      {
        '\u023A' => 'A', // Latin Capital Letter A with stroke
        '\u0243' => 'B', // Latin Capital Letter B with stroke
        '\u0180' => 'b', // Latin Small Letter B with stroke
        '\u023B' => 'C', // Latin Capital Letter C with stroke
        '\u023C' => 'c', // Latin Small Letter C with stroke
        '\u0110' => 'D', // Latin Capital Letter D with stroke
        '\u0111' => 'd', // Latin Small Letter D with stroke
        '\u0246' => 'E', // Latin Capital Letter E with stroke
        '\u0247' => 'e', // Latin Small Letter E with stroke
        '\u01E4' => 'G', // Latin Capital Letter G with stroke
        '\u01E5' => 'g', // Latin Small Letter G with stroke
        '\u0126' => 'H', // Latin Capital Letter H with stroke
        '\u0127' => 'h', // Latin Small Letter H with stroke
        '\u0197' => 'I', // Latin Capital Letter I with stroke
        '\u0248' => 'J', // Latin Capital Letter J with stroke
        '\u0249' => 'j', // Latin Small Letter J with stroke
        '\u0141' => 'L', // Latin Capital Letter L with stroke
        '\u0142' => 'l', // Latin Small Letter L with stroke
        '\u00D8' => 'O', // Latin Capital letter O with stroke
        '\u01FE' => 'O', // Latin Capital letter O with stroke and acute
        '\u00F8' => 'o', // Latin Small Letter O with stroke
        '\u01FF' => 'o', // Latin Small Letter O with stroke and acute
        '\u024C' => 'R', // Latin Capital letter R with stroke
        '\u024D' => 'r', // Latin Small Letter R with stroke
        '\u0166' => 'T', // Latin Capital Letter T with stroke
        '\u023E' => 'T', // Latin Capital Letter T with diagonal stroke
        '\u0167' => 't', // Latin Small Letter T with stroke
        '\u024E' => 'Y', // Latin Capital letter Y with stroke
        '\u024F' => 'y', // Latin Small Letter Y with stroke
        '\u01B5' => 'Z', // Latin Capital Letter Z with stroke
        '\u01B6' => 'z', // Latin Small Letter Z with stroke
        _ => source,
      };
  }
}
