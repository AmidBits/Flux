namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SequenceBuilder<char> ToSequenceBuilder(this System.Text.StringBuilder source) => new(source.ToString().AsSpan());
  }
}
