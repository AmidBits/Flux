namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SequenceBuilder<char> AppendLine(this SequenceBuilder<char> source, System.ReadOnlySpan<char> value)
      => source.Append(value).Append(System.Environment.NewLine.AsSpan());
  }
}
