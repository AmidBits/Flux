namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<char> InsertLine(this SequenceBuilder<char> source, int index, System.ReadOnlySpan<char> value)
      => source.Insert(index, value).Insert(index + value.Length, System.Environment.NewLine.AsSpan());
  }
}
