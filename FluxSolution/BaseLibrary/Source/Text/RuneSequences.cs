namespace Flux.Text
{
  public static class RuneSequences
  {
    /// <summary>ASCII</summary>
    /// <see cref="https://en.wikipedia.org/wiki/ASCII"/>
    public static ReadOnlySpan<System.Text.Rune> Ascii => System.Linq.Enumerable.Range(0, 127).Select(i => (System.Text.Rune)i).ToArray();

    /// <summary>The base62 encoding scheme uses 62 characters. The characters consist of the capital letters A-Z, the lower case letters a-z and the numbers 0–9.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Base62"/>
    public static ReadOnlySpan<System.Text.Rune> Base62 => (ReadOnlySpan<System.Text.Rune>)new System.Text.Rune[] { (System.Text.Rune)'0', (System.Text.Rune)'1', (System.Text.Rune)'2', (System.Text.Rune)'3', (System.Text.Rune)'4', (System.Text.Rune)'5', (System.Text.Rune)'6', (System.Text.Rune)'7', (System.Text.Rune)'8', (System.Text.Rune)'9', (System.Text.Rune)'A', (System.Text.Rune)'B', (System.Text.Rune)'C', (System.Text.Rune)'D', (System.Text.Rune)'E', (System.Text.Rune)'F', (System.Text.Rune)'G', (System.Text.Rune)'H', (System.Text.Rune)'I', (System.Text.Rune)'J', (System.Text.Rune)'K', (System.Text.Rune)'L', (System.Text.Rune)'M', (System.Text.Rune)'N', (System.Text.Rune)'O', (System.Text.Rune)'P', (System.Text.Rune)'Q', (System.Text.Rune)'R', (System.Text.Rune)'S', (System.Text.Rune)'T', (System.Text.Rune)'U', (System.Text.Rune)'V', (System.Text.Rune)'W', (System.Text.Rune)'X', (System.Text.Rune)'Y', (System.Text.Rune)'Z', (System.Text.Rune)'a', (System.Text.Rune)'b', (System.Text.Rune)'c', (System.Text.Rune)'d', (System.Text.Rune)'e', (System.Text.Rune)'f', (System.Text.Rune)'g', (System.Text.Rune)'h', (System.Text.Rune)'i', (System.Text.Rune)'j', (System.Text.Rune)'k', (System.Text.Rune)'l', (System.Text.Rune)'m', (System.Text.Rune)'n', (System.Text.Rune)'o', (System.Text.Rune)'p', (System.Text.Rune)'q', (System.Text.Rune)'r', (System.Text.Rune)'s', (System.Text.Rune)'t', (System.Text.Rune)'u', (System.Text.Rune)'v', (System.Text.Rune)'w', (System.Text.Rune)'x', (System.Text.Rune)'y', (System.Text.Rune)'z' };

    /// <summary>https://en.wikipedia.org/wiki/Base64</summary>
    public static ReadOnlySpan<System.Text.Rune> Base64 => (ReadOnlySpan<System.Text.Rune>)new System.Text.Rune[] { (System.Text.Rune)'A', (System.Text.Rune)'B', (System.Text.Rune)'C', (System.Text.Rune)'D', (System.Text.Rune)'E', (System.Text.Rune)'F', (System.Text.Rune)'G', (System.Text.Rune)'H', (System.Text.Rune)'I', (System.Text.Rune)'J', (System.Text.Rune)'K', (System.Text.Rune)'L', (System.Text.Rune)'M', (System.Text.Rune)'N', (System.Text.Rune)'O', (System.Text.Rune)'P', (System.Text.Rune)'Q', (System.Text.Rune)'R', (System.Text.Rune)'S', (System.Text.Rune)'T', (System.Text.Rune)'U', (System.Text.Rune)'V', (System.Text.Rune)'W', (System.Text.Rune)'X', (System.Text.Rune)'Y', (System.Text.Rune)'Z', (System.Text.Rune)'a', (System.Text.Rune)'b', (System.Text.Rune)'c', (System.Text.Rune)'d', (System.Text.Rune)'e', (System.Text.Rune)'f', (System.Text.Rune)'g', (System.Text.Rune)'h', (System.Text.Rune)'i', (System.Text.Rune)'j', (System.Text.Rune)'k', (System.Text.Rune)'l', (System.Text.Rune)'m', (System.Text.Rune)'n', (System.Text.Rune)'o', (System.Text.Rune)'p', (System.Text.Rune)'q', (System.Text.Rune)'r', (System.Text.Rune)'s', (System.Text.Rune)'t', (System.Text.Rune)'u', (System.Text.Rune)'v', (System.Text.Rune)'w', (System.Text.Rune)'x', (System.Text.Rune)'y', (System.Text.Rune)'z', (System.Text.Rune)'0', (System.Text.Rune)'1', (System.Text.Rune)'2', (System.Text.Rune)'3', (System.Text.Rune)'4', (System.Text.Rune)'5', (System.Text.Rune)'6', (System.Text.Rune)'7', (System.Text.Rune)'8', (System.Text.Rune)'9', (System.Text.Rune)'+', (System.Text.Rune)'/' };

    /// <summary>The Mayan numeral system was the system to represent numbers and calendar dates in the Maya civilization. It was a vigesimal (base-20) positional numeral system.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Maya_numerals"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Vigesimal"/>
    public static ReadOnlySpan<System.Text.Rune> MayanNumerals => (ReadOnlySpan<System.Text.Rune>)new System.Text.Rune[] { (System.Text.Rune)0x1D2E0, (System.Text.Rune)0x1D2E1, (System.Text.Rune)0x1D2E2, (System.Text.Rune)0x1D2E3, (System.Text.Rune)0x1D2E4, (System.Text.Rune)0x1D2E5, (System.Text.Rune)0x1D2E6, (System.Text.Rune)0x1D2E7, (System.Text.Rune)0x1D2E8, (System.Text.Rune)0x1D2E9, (System.Text.Rune)0x1D2EA, (System.Text.Rune)0x1D2EB, (System.Text.Rune)0x1D2EC, (System.Text.Rune)0x1D2ED, (System.Text.Rune)0x1D2EE, (System.Text.Rune)0x1D2EF, (System.Text.Rune)0x1D2F0, (System.Text.Rune)0x1D2F1, (System.Text.Rune)0x1D2F2, (System.Text.Rune)0x1D2F3 };

    /// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
    public static ReadOnlySpan<System.Text.Rune> PrintableAscii => Ascii.Slice(32, 95);

    /// <summary>Decimal unicode subscript</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    public static ReadOnlySpan<System.Text.Rune> Subscript0Through9 => (ReadOnlySpan<System.Text.Rune>)new System.Text.Rune[] { (System.Text.Rune)0x2080, (System.Text.Rune)0x2081, (System.Text.Rune)0x2082, (System.Text.Rune)0x2083, (System.Text.Rune)0x2084, (System.Text.Rune)0x2085, (System.Text.Rune)0x2086, (System.Text.Rune)0x2087, (System.Text.Rune)0x2088, (System.Text.Rune)0x2089 };
    /// <summary>Decimal unicode superscript</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Subscript_and_superscript"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Decimal"/>
    public static ReadOnlySpan<System.Text.Rune> Superscript0Through9 => (ReadOnlySpan<System.Text.Rune>)new System.Text.Rune[] { (System.Text.Rune)0x2070, (System.Text.Rune)0x00B9, (System.Text.Rune)0x00B2, (System.Text.Rune)0x00B3, (System.Text.Rune)0x2074, (System.Text.Rune)0x2075, (System.Text.Rune)0x2076, (System.Text.Rune)0x2077, (System.Text.Rune)0x2078, (System.Text.Rune)0x2079 };
  }
}
