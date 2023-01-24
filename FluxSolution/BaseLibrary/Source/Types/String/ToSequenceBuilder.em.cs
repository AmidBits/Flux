namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SequenceBuilder<char> ToSequenceBuilder(this string source)
      => new(source.AsSpan());
    public static SequenceBuilder<char> ToSequenceBuilder(this string source, int startIndex, int length)
      => new(source.AsSpan().Slice(startIndex, length));
  }
}
