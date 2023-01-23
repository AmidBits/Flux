namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static string ToString(this SequenceBuilder<System.Text.Rune> source, int startAt, int count)
      => source.AsReadOnlySpan().ToString(startAt, count);
  }
}
