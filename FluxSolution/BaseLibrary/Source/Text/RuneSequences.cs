namespace Flux.Text
{
  public static class RuneSequences
  {
    /// <summary>The base62 encoding scheme uses 62 characters. The characters consist of the capital letters A-Z, the lower case letters a-z and the numbers 0–9.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Base62"/>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> Base62
      => Loops.RangeLoop<char>.CreateBetween('0', '9').Concat(Loops.RangeLoop<char>.CreateBetween('A', 'Z')).Concat(Loops.RangeLoop<char>.CreateBetween('a', 'z')).Select(c => (System.Text.Rune)c);

    ///// <summary>https://en.wikipedia.org/wiki/Base64</summary>
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> Base64
      => Loops.RangeLoop<char>.CreateBetween('A', 'Z').Concat(Loops.RangeLoop<char>.CreateBetween('a', 'z')).Concat(Loops.RangeLoop<char>.CreateBetween('0', '9')).Concat(new char[] { '+', '/' }).Select(c => (System.Text.Rune)c);

    ///// <summary>The Mayan numeral system was the system to represent numbers and calendar dates in the Maya civilization. It was a vigesimal (base-20) positional numeral system.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Maya_numerals"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Vigesimal"/>
    //public static System.Text.Rune[] MayanNumerals = new System.Text.Rune[] { (System.Text.Rune)0x1D2E0, (System.Text.Rune)0x1D2E1, (System.Text.Rune)0x1D2E2, (System.Text.Rune)0x1D2E3, (System.Text.Rune)0x1D2E4, (System.Text.Rune)0x1D2E5, (System.Text.Rune)0x1D2E6, (System.Text.Rune)0x1D2E7, (System.Text.Rune)0x1D2E8, (System.Text.Rune)0x1D2E9, (System.Text.Rune)0x1D2EA, (System.Text.Rune)0x1D2EB, (System.Text.Rune)0x1D2EC, (System.Text.Rune)0x1D2ED, (System.Text.Rune)0x1D2EE, (System.Text.Rune)0x1D2EF, (System.Text.Rune)0x1D2F0, (System.Text.Rune)0x1D2F1, (System.Text.Rune)0x1D2F2, (System.Text.Rune)0x1D2F3 };
  }
}
