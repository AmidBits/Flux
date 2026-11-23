namespace Flux
{
  public static partial class CharExtensions
  {
    extension(System.Char)
    {
      #region ToCsharpUtf16Literal

      public static string ToCsharpUtf16Literal(char character) => $"\\u{(int)character:X4}";

      #endregion
    }

    extension(System.Char character)
    {
      #region ToVerboseString

      public string ToVerboseString()
        => char.IsSurrogate(character)
        ? $"UTF-16 0x{(int)character:X4} {(char.IsHighSurrogate(character) ? "High" : char.IsLowSurrogate(character) ? "Low" : string.Empty)}-{char.GetUnicodeCategory(character)}"
        : $"{System.Text.Rune.ToUnicodeUnotation(new System.Text.Rune(character))} '{character}' {char.GetUnicodeCategory(character)}";

      #endregion

      #region Utf8SequenceLength

      /// <summary>
      /// <para>A char is a UTF‑16 code unit, not a full Unicode character. For UTF‑8 (which is variable-length) encoding, the number of bytes depends on the Unicode code point it represents:</para>
      /// <list type="bullet">
      /// <item><c>0x0000 to 0x007F</c> = 1 byte</item>
      /// <item><c>0x0080 to 0x07FF</c> = 2 bytes</item>
      /// <item><c>0x0800 to 0xFFFF</c> = 3 bytes</item>
      /// <item>* <c>0x10000 to 0x10FFFF</c> = 4 bytes (requires surrogate pairs in UTF-16)</item>
      /// </list>
      /// <para><i>This property is for a single char, which means the last bullet does not apply here, i.e. a single (high or low) surrogate which equals half of a surrogate pair and therefor 2 bytes</i>.</para>
      /// </summary>
      /// <remarks>
      /// <para>A char is always a single UTF‑16 code unit (16 bits). However, a Unicode character may require one or two UTF‑16 code units:</para>
      /// <list type="bullet">
      /// <item>Most characters (Basic Multilingual Plane, U+0000 to U+FFFF) fit in 1 char.</item>
      /// <item>Supplementary characters (U+10000 and above, e.g., many emojis) require 2 char values - a surrogate pair.</item>
      /// </list>
      /// </remarks>
      [System.Diagnostics.Contracts.Pure]
      public int Utf8SequenceLength
        => char.IsSurrogate(character) ? throw new System.InvalidOperationException("Cannot determine UTF-8 sequence length from an isolated surrogate.")
        : character <= '\u007F' ? 1
        : character <= '\u07FF' ? 2
        : 3;

      #endregion
    }
  }
}
