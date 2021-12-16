namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the Unicode block associated with in the specified UTF32 code point.</summary>
    public static Text.UnicodeBlock GetUnicodeBlock(this System.Text.Rune source)
    {
      foreach (var block in System.Enum.GetValues<Text.UnicodeBlock>())
        if (source.Value >= GetMinRune(block).Value && source.Value <= GetMaxRune(block).Value)
          return block;

      throw new System.ArgumentOutOfRangeException(nameof(source));
    }
  }
}
