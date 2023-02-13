namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>Contains all English graphemes from the 'main system' and the rest including the minor patterns and oddities.</summary>
    public static readonly string[][][] Graphemes_enUS = new string[][][]
    {
      new string[][] { new string[] { "a", "a.e", "ai", "air", "ar", "are", "au", "aw", "ay", }, new string[] { "aa", "aar", "ach", "ae", "aer", "ah", "aigh", "aire", "ais", "ait", "al", "alf", "anc", "ao", "aoh", "aow", "arr", "arre", "arrh", "as", "at", "augh", "aul", "aur", "awe", "aye", "ayer", "ayor", }, },
      new string[][] { new string[] { "b", "bb", }, new string[] { "bh", "bd", "bp", "bt", "bu", "bv", }, },
      new string[][] { new string[] { "c", "ce", "ch", "ci", "ck", }, new string[] { "cc", "cch", "che", "chs", "ckgu", "cq", "cqu", "ct", "cu", "cz", }, },
      new string[][] { new string[] { "d", "dd", "dg", "dge", }, new string[] { "de", "ddh", "dh", "ddh", "di", "dj", "dne", "dt", }, },
      new string[][] { new string[] { "e", "ea", "ear", "ed", "ee", "e.e", "eer", "er", "ere", "ew", }, new string[] { "eah", "eau", "e'er", "ei", "eigh", "eir", "eo", "e're", "err", "erre", "es", "et", "eu", "eur", "ewe", "ey", "eye", "eyr", "ey're", "ez", }, },
      new string[][] { new string[] { "f", "ff", }, new string[] { "fe", "ffe", "ft" }, },
      new string[][] { new string[] { "g", "ge", "gg", }, new string[] { "gh", "gi", "gl", "gm", "gn", "gne", "gu", "gue", }, },
      new string[][] { new string[] { "h" }, new string[] { "hea", "heir", "ho", "hour", "hu", }, },
      new string[][] { new string[] { "i", "ie", "i.e", "igh", "ir", }, new string[] { "ia", "ier", "ieu", "io", "ire", "irr", "is", "it", }, },
      new string[][] { new string[] { "j", }, new string[] { "jj" }, },
      new string[][] { new string[] { "k", }, new string[] { "ke", "kh", "kk", "kn", }, },
      new string[][] { new string[] { "l", "le", "ll", }, new string[] { "lh", "lle" }, },
      new string[][] { new string[] { "m", "mm", }, new string[] { "mb", "mbe", "me", "mme", "mn", }, },
      new string[][] { new string[] { "n", "ng", "nn", }, new string[] { "nc", "nd", "ne", "ngh", "ngu", "ngue", "nne", "nt", "nw", }, },
      new string[][] { new string[] { "o", "o. e", "oi", "oo", "or", "ore", "ou", "ow", "oy", }, new string[] { "oa", "oar", "oat", "oe", "oer", "oeu", "oh", "oir", "oire", "ois", "ol", "olo", "ooh", "oor", "orp", "orps", "orr", "ort", "os", "ot", "oue", "ough", "oul", "oup", "our", "ou're", "ous", "out", "oux", "owe", }, },
      new string[][] { new string[] { "p", "ph", "pp", }, new string[] { "pb", "pe", "phth", "pn", "ppe", "pph", "ps", "pt", }, },
      new string[][] { new string[] { "q", }, new string[] { "qu", "que" }, },
      new string[][] { new string[] { "r", "rr", }, new string[] { "re", "rh", "rrh" }, },
      new string[][] { new string[] { "s", "se", "sh", "si", "ss", "ssi", }, new string[] { "sc", "sce", "sch", "sci", "sj", "sse", "st", "sth", "sw", }, },
      new string[][] { new string[] { "t", "tch", "th", "ti", "tt", }, new string[] { "te", "the", "ts", "tsch", "tte", "tw", }, },
      new string[][] { new string[] { "u", "ue", "u. e", "ur", }, new string[] { "ua", "ui", "ure", "urr", "ut", "uu", }, },
      new string[][] { new string[] { "v", "ve", }, new string[] { "vv" }, },
      new string[][] { new string[] { "w", "wh", }, new string[] { "wi", "wr", "ww" }, },
      new string[][] { new string[] { "x", }, new string[] { "xe", "xh", "xi" }, },
      new string[][] { new string[] { "y", }, new string[] { "ye", "y.e", "yr", "yre", "yrrh", }, },
      new string[][] { new string[] { "z", "zz", }, new string[] { "ze", "zi" }, },
    };

    /// <summary>Returns all graphemes.</summary>
    /// <see href="https://en.wikipedia.org/wiki/IPA_Extensions"/>
    /// <see href="https://en.wikipedia.org/wiki/English_phonology"/>
    /// <seealso href="https://www.dyslexia-reading-well.com/44-phonemes-in-english.html"/>
    /// <seealso href="https://books.openedition.org/obp/2190"/>
    public static System.Collections.Generic.IList<string> GetGraphemesOf(this System.Globalization.CultureInfo source)
      => (source ?? System.Globalization.CultureInfo.CurrentCulture).TwoLetterISOLanguageName switch
      {
        "en" => Graphemes_enUS.SelectMany(aa => aa.SelectMany(a => a)).ToList(),
        _ => throw new System.NotImplementedException(nameof(source))
      };
  }
}
