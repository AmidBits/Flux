namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<System.Text.Rune> ToSequenceBuilderOfRune(this SequenceBuilder<char> source)
      => new SequenceBuilder<System.Text.Rune>(source.AsReadOnlySpan().ToSpanRune());
  }
}
