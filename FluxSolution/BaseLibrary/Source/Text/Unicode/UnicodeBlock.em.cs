namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the first UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointFirst(this Text.UnicodeBlock block)
      => (int)((long)block >> 32 & 0x7FFFFFFF);
    /// <summary>Returns the last UTF32 code point in the specified Unicode block.</summary>
    public static int GetCodePointLast(this Text.UnicodeBlock block)
      => (int)((long)block & 0x7FFFFFFF);
  }
}
