namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>Returns the Unicode block associated with in the specified UTF32 code point.</summary>
    public static UnicodeBlock GetUnicodeBlock(this System.Text.Rune source)
    {
      foreach (var block in System.Enum.GetValues<UnicodeBlock>())
        if (source.Value >= block.GetMinValue() && source.Value <= block.GetMaxValue())
          return block;

      throw new System.ArgumentOutOfRangeException(nameof(source));
    }
  }
}
