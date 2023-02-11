namespace Flux
{
  public static partial class ExtensionMethodsCultureInfo
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ipaCode"></param>
    /// <returns></returns>
    /// <see href="https://en.wikipedia.org/wiki/IPA_Extensions"/>
    /// <see href="https://en.wikipedia.org/wiki/English_phonology"/>
    /// <seealso href="https://www.dyslexia-reading-well.com/44-phonemes-in-english.html"/>
    /// // https://books.openedition.org/obp/2190
    public static string[] GetGraphemesOfIpa(this System.Globalization.CultureInfo source, string ipaCode)
      // ADD CHECK FOR en-US!!!
      => ipaCode switch
      {
        // Consonants:
        "\u0062" => new string[] { "b", "bb" }, // b
        "\u0064" => new string[] { "d", "dd", "ed" }, // d
        "\u0066" => new string[] { "f", "ff", "ph", "gh", "lf", "ft" }, // f
        "\u0068" => new string[] { "h", "wh" }, // h
        "\u006A" => new string[] { "y", "i", "j" }, // j
        "\u006B" => new string[] { "k", "c", "ch", "cc", "lk", "qu", "ck", "x" }, // k
        "\u006C" => new string[] { "l", "ll" }, // l
        "\u006D" => new string[] { "m", "mm", "mb", "mn", "lm" }, // m
        "\u006E" => new string[] { "n", "nn", "kn", "gn", "pn", "mn" }, // n
        "\u0070" => new string[] { "p", "pp" }, // p
        "\u0073" => new string[] { "s", "ss", "c", "sc", "ps", "st", "ce", "se" }, // s
        "\u0074" => new string[] { "t", "tt", "th", "ed" }, // t
        "\u0076" => new string[] { "v", "f", "ph", "ve" }, // v
        "\u0077" => new string[] { "w", "wh", "u", "o" }, // w
        "\u007A" => new string[] { "z", "zz", "s", "ss", "x", "ze", "se" }, // z
        "\u00f0" => new string[] { "th" },
        "\u014B" => new string[] { "ng", "n", "ngue" },
        "\u0261" => new string[] { "g", "gg", "gh", "gu", "gue" }, // g
        "\u0279" => new string[] { "r", "rr", "wr", "rh" },
        "\u0283" => new string[] { "sh", "ce", "s", "ci", "si", "ch", "sci", "ti" },
        "\u0292" => new string[] { "s", "si", "z" },
        "\u02A4" => new string[] { "j", "ge", "g", "dge", "di", "gg" },
        "\u02A7" => new string[] { "ch", "tch", "tu", "te" },
        "\u03B8" => new string[] { "th" },
        // Vowels:
        "\u0065" => new string[] { "a", "ai", "eigh", "aigh", "ay", "er", "et", "ei", "au", "ea", "ey" },
        "\u0069" => new string[] { "e", "ee", "ea", "y", "ey", "oe", "ie", "i", "ei", "eo", "ay" },
        "\u006F\u031E" => new string[] { "o", "oa", "oe", "ow", "ough", "eau", "oo", "ew" }, // o+omega
        "\u00E6" => new string[] { "a", "ai", "au" },
        "\u0250" => new string[] { "u", "o", "oo", "ou" },

        "\u0254" => new string[] { "a", "ho", "au", "aw", "ough" },
        "\u025B" => new string[] { "e", "ea", "u", "ie", "ai", "a", "eo", "ei", "ae" },
        "\u026A" => new string[] { "i", "e", "o", "u", "ui", "y", "ie" },
        "\u0289" => new string[] { "o", "oo", "ew", "ue", "oe", "ough", "ui", "oew", "ou" }, // u+colon
        "\u028A" => new string[] { "o", "oo", "u", "ou" }, // [flipped-omega]
        "\u0061\u028A" => new string[] { "ow", "ou", "ough" }, // a+[flipped-omega]

        // VOWELS ARE NOT COMPLETED YET!
        //'\u0259' => new string[] { "a, er, i, ar, our, ur" },
        //'\u0' => new string[] { "air, are, ear, ere, eir, ayer" },
        //'\u0251' => new string[] { "a" },
        //'\u0' => new string[] { "ir, er, ur, ear, or, our, yr" },
        //'\u0' => new string[] { "aw, a, or, oor, ore, oar, our, augh, ar, ough, au" },
        //'\u0' => new string[] { "ear, eer, ere, ier" },
        //'\u0' => new string[] { "ure, our" },
        _ => System.Array.Empty<string>()
      };

    public static string[] GetGraphemesOfIpa(this System.Globalization.CultureInfo source, System.Text.Rune ipaRune) => GetGraphemesOfIpa(source, ipaRune.ToString());
  }
}
