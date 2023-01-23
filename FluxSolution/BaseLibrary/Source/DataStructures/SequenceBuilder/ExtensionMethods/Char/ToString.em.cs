namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static string ToString(this SequenceBuilder<char> source, int startAt, int count)
      => source.AsReadOnlySpan().ToString(startAt, count);
  }
}
