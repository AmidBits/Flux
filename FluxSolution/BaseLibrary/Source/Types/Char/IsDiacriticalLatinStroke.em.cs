namespace Flux
{
  public static partial class SystemCharEm
  {
    /// <summary>Determines whether the character is a latin diacritical stroke.</summary>
    public static bool IsDiacriticalLatinStroke(this char source)
      => source switch
      {
        '\u00D8' => true, // Latin Capital letter O with stroke
        '\u00F8' => true, // Latin Small Letter O with stroke
        '\u0110' => true, // Latin Capital Letter D with stroke
        '\u0111' => true, // Latin Small Letter D with stroke
        '\u0126' => true, // Latin Capital Letter H with stroke
        '\u0127' => true, // Latin Small Letter H with stroke
        '\u0141' => true, // Latin Capital Letter L with stroke
        '\u0142' => true, // Latin Small Letter L with stroke
        '\u0166' => true, // Latin Capital Letter T with stroke
        '\u0167' => true, // Latin Small Letter T with stroke
        '\u0180' => true, // Latin Small Letter B with stroke
        '\u01B5' => true, // Latin Capital Letter Z with stroke
        '\u01B6' => true, // Latin Small Letter Z with stroke
        '\u0197' => true, // Latin Capital Letter I with stroke
        '\u01E4' => true, // Latin Capital Letter G with stroke
        '\u01E5' => true, // Latin Small Letter G with stroke
        '\u01FE' => true, // Latin Capital letter O with stroke and acute
        '\u01FF' => true, // Latin Small Letter O with stroke and acute
        '\u023A' => true, // Latin Capital Letter A with stroke
        '\u023B' => true, // Latin Capital Letter C with stroke
        '\u023C' => true, // Latin Small Letter C with stroke
        '\u023E' => true, // Latin Capital Letter T with diagonal stroke
        '\u0243' => true, // Latin Capital Letter B with stroke
        '\u0246' => true, // Latin Capital Letter E with stroke
        '\u0247' => true, // Latin Small Letter E with stroke
        '\u0248' => true, // Latin Capital Letter J with stroke
        '\u0249' => true, // Latin Small Letter J with stroke
        '\u024C' => true, // Latin Capital letter R with stroke
        '\u024D' => true, // Latin Small Letter R with stroke
        '\u024E' => true, // Latin Capital letter Y with stroke
        '\u024F' => true, // Latin Small Letter Y with stroke
        _ => false,
      };
  }
}
