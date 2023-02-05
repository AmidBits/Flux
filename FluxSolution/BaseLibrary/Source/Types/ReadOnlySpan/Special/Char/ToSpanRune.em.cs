namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Text.StringBuilder"/> from the source.</summary>
    public static System.ReadOnlySpan<System.Text.Rune> ToSpanRune(this System.ReadOnlySpan<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      foreach (var rune in source.EnumerateRunes())
        list.Add(rune);

      return list.AsReadOnlySpan();
    }
  }
}
