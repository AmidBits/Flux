namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SequenceBuilder<char> ToSequenceBuilder(this string source)
      => new(source);
    public static SequenceBuilder<char> ToSequenceBuilder(this string source, int startIndex, int length)
      => new(source.Substring(startIndex, length));
  }
}
