namespace Flux
{
  // https://en.wikipedia.org/wiki/QWERTY

  public static partial class XtensionsQwerty
  {
    public static void GetRelativeFrequencyOfLetters(this System.Globalization.CultureInfo source)
    {
      var table = new System.Collections.Generic.Dictionary<char, double>();
      if (source.Name.StartsWith("en"))
      {
      }

      //return table;
    }
  }

  public static class QwertyProximity
  {
    public static System.Collections.Generic.Dictionary<char, string> English = new System.Collections.Generic.Dictionary<char, string>()
    {
      { 'a', "swqz" },
      { 'b', "nhgv " },
      { 'c', "dfvx " },
      { 'd', "esrcfx" },
      { 'e', "srdw34" },
      { 'f', "trdcgv" },
      { 'g', "thfbyv" },
      { 'h', "nugybj" },
      { 'i', "ouk89j" },
      { 'j', "inhumk" },
      { 'k', "oilmj," },
      { 'l', "opk;,." },
      { 'm', "nkj, " },
      { 'n', "hmbj " },
      { 'o', "ilpk09" },
      { 'p', "ol[;0-" },
      { 'q', "aw12\t" },
      { 'r', "etdf45" },
      { 's', "eadwxz" },
      { 't', "rfgy56" },
      { 'u', "ihyj78" },
      { 'v', "cfgb " },
      { 'w', "easq23" },
      { 'x', "sdcz " },
      { 'y', "thug67" },
      { 'z', "asx" },

      { 'A', "SWQZ" },
      { 'B', "NHGV " },
      { 'C', "DFVX " },
      { 'D', "ESRCFX" },
      { 'E', "SRDW#$" },
      { 'F', "TRDCGV" },
      { 'G', "THFBYV" },
      { 'H', "NUGYBJ" },
      { 'I', "OUK*(J" },
      { 'J', "INHUMK" },
      { 'K', "OILMJ<" },
      { 'L', "OPK:<>" },
      { 'M', "NKJ< " },
      { 'N', "HMBJ " },
      { 'O', "ILPK)(" },
      { 'P', "OL{:)_" },
      { 'Q', "AW!@\t" },
      { 'R', "ETDF$%" },
      { 'S', "EADWXZ" },
      { 'T', "RFGY%^" },
      { 'U', "IHYJ&*" },
      { 'V', "CFGB " },
      { 'W', "EASQ@#" },
      { 'X', "SDCZ " },
      { 'Y', "THUG^&" },
      { 'Z', "ASX" },
    };
  }
}
