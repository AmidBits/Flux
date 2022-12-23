namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.ReadOnlySpan<System.Text.Rune> ToRunes(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      foreach (var rune in source.EnumerateRunes())
        list.Add(rune);

      return list.AsSpan();
    }
  }
}
