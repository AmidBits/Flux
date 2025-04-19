namespace Flux
{
  public static partial class RuneEnumerators
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes(this System.IO.TextReader source) => new Text.RuneEnumerator(source);
  }
}
