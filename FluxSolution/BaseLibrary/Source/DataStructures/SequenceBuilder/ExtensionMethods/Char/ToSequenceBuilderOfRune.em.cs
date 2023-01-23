namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<System.Text.Rune> ToSequenceBuilderOfRune(this SequenceBuilder<char> source)
    {
      var target = new SequenceBuilder<System.Text.Rune>();

      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
        target.Append(rune);

      return target;
    }
  }
}
