//namespace Flux
//{
//  // https://en.wikipedia.org/wiki/QWERTY

//  //public static partial class ExtensionMethods
//  //{
//  //  public static System.ReadOnlySpan<char> AdjacentQwertyKeys(this char source)
//  //    => QwertyProximity.English.ContainsKey(source) ? QwertyProximity.English[source] : string.Empty;

//  //  public static void GetRelativeFrequencyOfLetters(this System.Globalization.CultureInfo source)
//  //  {
//  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

//  //    var table = new System.Collections.Generic.Dictionary<char, double>();

//  //    if (source.Name.StartsWith("en", System.StringComparison.Ordinal))
//  //    {
//  //    }

//  //    //return table;
//  //  }
//  //}

//  //public static class User32Interop
//  //{
//  //  [System.Runtime.InteropServices.DllImport("user32.dll")]
//  //  static extern short VkKeyScan(char c);

//  //  public static char ToAscii(Keys key, Keys modifiers)
//  //  {
//  //    var outputBuilder = new System.Text.StringBuilder(2);
//  //    int result = ToAscii((uint)key, 0, GetKeyState(modifiers), outputBuilder, 0);
//  //    if (result == 1)
//  //      return outputBuilder[0];
//  //    else
//  //      throw new System.Exception("Invalid key");
//  //  }

//  //  private const byte HighBit = 0x80;
//  //  private static byte[] GetKeyState(Keys modifiers)
//  //  {
//  //    var keyState = new byte[256];
//  //    foreach (Keys key in System.Enum.GetValues(typeof(Keys)))
//  //    {
//  //      if ((modifiers & key) == key)
//  //      {
//  //        keyState[(int)key] = HighBit;
//  //      }
//  //    }
//  //    return keyState;
//  //  }

//  //  [System.Runtime.InteropServices.DllImport("user32.dll")]
//  //  private static extern int ToAscii(uint uVirtKey, uint uScanCode, byte[] lpKeyState, [System.Runtime.InteropServices.Out] System.Text.StringBuilder lpChar, uint uFlags);
//  //}

//  public static class QwertyProximity
//  {
//    //public static void Distance(char source, char target)
//    //{

//    //  var v = new System.Collections.Generic.List<char>();

//    //  var l = InternalDistance(source, English[source].ToCharArray(), target);

//    //  System.Collections.Generic.KeyValuePair<System.Collections.Generic.List<char>, int> InternalDistance(char s, char[] n, char t)
//    //  {
//    //    if (n.Contains(t))
//    //      return new System.Collections.Generic.KeyValuePair<System.Collections.Generic.List<char>, int>(new System.Collections.Generic.List<char>() { s, t }, 1);

//    //    v.Add(s);
//    //    v.AddRange(n);

//    //    foreach (var c in n.SelectMany(c => English[c]).Where(c => !v.Contains(c)))
//    //    {
//    //      return InternalDistance(s, .ToArray(), t);
//    //    }
//    //  }
//    //}

//    public static readonly System.Collections.Generic.Dictionary<char, string> English = new()
//    {
//      { 'a', "swqz" },
//      { 'b', "nhgv " },
//      { 'c', "dfvx " },
//      { 'd', "esrcfx" },
//      { 'e', "srdw34" },
//      { 'f', "trdcgv" },
//      { 'g', "thfbyv" },
//      { 'h', "nugybj" },
//      { 'i', "ouk89j" },
//      { 'j', "inhumk" },
//      { 'k', "oilmj," },
//      { 'l', "opk;,." },
//      { 'm', "nkj, " },
//      { 'n', "hmbj " },
//      { 'o', "ilpk09" },
//      { 'p', "ol[;0-" },
//      { 'q', "aw12\t" },
//      { 'r', "etdf45" },
//      { 's', "eadwxz" },
//      { 't', "rfgy56" },
//      { 'u', "ihyj78" },
//      { 'v', "cfgb " },
//      { 'w', "easq23" },
//      { 'x', "sdcz " },
//      { 'y', "thug67" },
//      { 'z', "asx" },
//      { '`', "1\t" },
//      { '1', "2`\tq" },
//      { '2', "31qw" },
//      { '3', "42we" },
//      { '4', "53er" },
//      { '5', "64rt" },
//      { '6', "75ty" },
//      { '7', "86yu" },
//      { '8', "97ui" },
//      { '9', "08io" },
//      { '0', "-9op" },
//      { '-', "=0p[" },
//      { '=', "\b-[]" },
//      { '\\', "]\r" },

//      { 'A', "SWQZ" },
//      { 'B', "NHGV " },
//      { 'C', "DFVX " },
//      { 'D', "ESRCFX" },
//      { 'E', "SRDW#$" },
//      { 'F', "TRDCGV" },
//      { 'G', "THFBYV" },
//      { 'H', "NUGYBJ" },
//      { 'I', "OUK*(J" },
//      { 'J', "INHUMK" },
//      { 'K', "OILMJ<" },
//      { 'L', "OPK:<>" },
//      { 'M', "NKJ< " },
//      { 'N', "HMBJ " },
//      { 'O', "ILPK)(" },
//      { 'P', "OL{:)_" },
//      { 'Q', "AW!@\t" },
//      { 'R', "ETDF$%" },
//      { 'S', "EADWXZ" },
//      { 'T', "RFGY%^" },
//      { 'U', "IHYJ&*" },
//      { 'V', "CFGB " },
//      { 'W', "EASQ@#" },
//      { 'X', "SDCZ " },
//      { 'Y', "THUG^&" },
//      { 'Z', "ASX" },
//    };
//  }
//}
