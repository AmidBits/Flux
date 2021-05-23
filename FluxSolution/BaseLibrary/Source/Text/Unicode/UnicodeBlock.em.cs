using System.Linq;

namespace Flux.Text
{
  public static partial class Unicodex
  {
    /// <summary>Yields a sequence of Unicode blocks with their respective first and last code points.</summary>
    public static System.Collections.Generic.IReadOnlyList<(UnicodeBlock block, int firstCodePoint, int lastCodePoint)> GetBlockRanges()
      => ((long[])System.Enum.GetValues(typeof(UnicodeBlock))).Select(v => ((UnicodeBlock)v, (int)(v >> 32 & 0x7FFFFFFF), (int)(v & 0x7FFFFFFF))).ToList();

    /// <summary>Returns the first UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointFirst(this UnicodeBlock block)
      => (int)((long)block >> 32 & 0x7FFFFFFF);
    /// <summary>Returns the last UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointLast(this UnicodeBlock block)
      => (int)((long)block & 0x7FFFFFFF);

    /// <summary>Returns the Unicode block associated with in the specified UTF32 code point.</summary>
    public static UnicodeBlock GetBlock(int utf32)
    {
      foreach (var (block, firstCodePoint, lastCodePoint) in GetBlockRanges())
        if (utf32 >= firstCodePoint && utf32 <= lastCodePoint)
          return block;

      throw new System.ArgumentOutOfRangeException(nameof(utf32));
    }
    /// <summary>Returns the Unicode block associated with in the specified Rune.</summary>
    public static UnicodeBlock GetBlock(this System.Text.Rune rune)
      => GetBlock(rune.Value);
  }
}
