namespace Flux
{
  public static partial class RuneEm
  {
    /// <summary>Returns the Unicode block associated with in the specified UTF32 code point.</summary>
    public static UnicodeBlock GetUnicodeBlock(this System.Text.Rune source)
    {
      foreach (var block in System.Enum.GetValues<UnicodeBlock>())
        if (source.Value >= block.GetMinRune().Value && source.Value <= block.GetMaxRune().Value)
          return block;

      throw new System.ArgumentOutOfRangeException(nameof(source));
    }
  }
}
