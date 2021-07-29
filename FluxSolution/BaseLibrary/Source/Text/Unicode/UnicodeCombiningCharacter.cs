using System.Linq;

namespace Flux.Text
{
  /// <summary>The functionality of this class relates to U+xxxxx style formatting.</summary>
  public static class UnicodeCombiningCharacter
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> CombiningDiacricitalMarks()
      => UnicodeBlock.CombiningDiacriticalMarks.GetCodePoints();

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> CombiningDiacricitalMarksExtended()
    {
      for (var i = 0x1AB0; i <= 0x1AC0; i++)
        yield return (System.Text.Rune)i;
    }

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> CombiningDiacricitalMarksSupplement()
      => UnicodeBlock.CombiningDiacriticalMarksSupplement.GetCodePoints();

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> CombiningDiacricitalMarksForSymbols()
    {
      for (var i = 0x20D0; i <= 0x20F0; i++)
        yield return (System.Text.Rune)i;
    }

    public static System.Collections.Generic.IEnumerable<System.Text.Rune> CombiningHalfMarks()
      => UnicodeBlock.CombiningHalfMarks.GetCodePoints();
  }
}
